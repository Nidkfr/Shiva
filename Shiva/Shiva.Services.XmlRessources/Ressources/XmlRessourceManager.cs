using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Shiva.Core.Identities;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Shiva.Core.Services;
using Shiva.Core.IO;

namespace Shiva.Ressources
{
    /// <summary>
    /// Xml ressource Manager
    /// </summary>
    public class XmlRessourceManager : RessourceManagerBase, IDisposable
    {
        private CultureInfo _culture;
        private StreamSource _streamSource;
        private readonly IList<string> _validationXml = new List<string>();

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; private set; }

        private ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRessourceManager"/> class.
        /// </summary>
        /// <param name="logmanager">The logmanager.</param>
        public XmlRessourceManager(ILogManager logmanager = null) : base(logmanager)
        {
            this.Logger = logmanager?.CreateLogger<XmlRessourceManager>() ?? new NoLogger();
        }

        /// <summary>
        /// Intializes the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="dataXml">The data XML.</param>
        public void Initialize(CultureInfo culture, StreamSource source)
        {
            this.IsInitialized = false;
            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Initialize Ressource mananger to culture {culture}");

            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
            this._streamSource = source ?? throw new ArgumentNullException(nameof(source));

            this._ValidateXml();

            this.IsInitialized = true;
        }

        public override CultureInfo Culture => this._culture;

        ~XmlRessourceManager()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (this.Logger.DebugIsEnabled)
                this.Logger.Debug("Ressource Manager is disposed");
            this._streamSource?.Dispose();
        }

        public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId)
        {
            this._CheckInit();
            throw new NotImplementedException();
        }

        public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource)
        {
            this._CheckInit();
            throw new NotImplementedException();
        }

        protected override bool ContainsRessourceInternal<TRessource>(Identity ressourceID)
        {
            this._CheckInit();
            var elt = this._GetRessource(ressourceID, typeof(TRessource));
            if (elt == null) return false;

            if (elt.Elements(ELEMENT_VALUE).Any(x => x.Attribute(ATTRIBUTE_LANG).Value == this.Culture.TwoLetterISOLanguageName))
                return true;
            return false;
        }

        protected override IEnumerable<Identity> GetAllGroupsInternal()
        {
            this._CheckInit();
            throw new NotImplementedException();
        }

        protected override TRessource GetRessourceInternal<TRessource>(Identity ressourceID)
        {
            this._CheckInit();
            throw new NotImplementedException();
        }

        private XElement _GetRessource(Identity id, Type ressource)
        {
            var validParent = false;

            var stream = this._streamSource.GetStream();
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = XmlReader.Create(stream))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == ELEMENT_RESSOURCES)
                        {
                            validParent = true;
                        }

                        if (reader.Name == ELEMENT_RESSOURCE)
                        {
                            if (id == reader.GetAttribute("id") && ressource.FullName == reader.GetAttribute("type"))
                            {
                                return (XElement)XElement.ReadFrom(reader);
                            }
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == ELEMENT_RESSOURCES)
                        {
                            if (validParent) return null;
                        }
                    }
                }
            }
            return null;
        }
        private void _ValidateXml()
        {
            this._validationXml.Clear();
            var ressource = this.GetType().Assembly.GetManifestResourceNames();
            var schema = XmlSchema.Read(this.GetType().Assembly.GetManifestResourceStream(RESSOURCE_SCHEMA), this._Xml_ValidationEventHandler);

            if (this._validationXml.Count == 0)
            {
                var xsdSet = new XmlSchemaSet();
                xsdSet.ValidationEventHandler += this._Xml_ValidationEventHandler;
                xsdSet.Add(schema);

                var settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema
                };
                settings.Schemas.Add(xsdSet);
                settings.ValidationEventHandler += new ValidationEventHandler(this._Xml_ValidationEventHandler);

                using (var reader = XmlReader.Create(this._streamSource.GetStream(), settings))
                    while (reader.Read())
                    {
                    }
                if (this._validationXml.Count > 0)
                    throw new XmlSchemaValidationException($"Ressource Xml stream validation fail :\n\r {string.Join("\n\r", this._validationXml)}");
            }
            else
                throw new XmlSchemaValidationException($"Ressource Xml schema is not valid :\n\r {string.Join("\n\r", this._validationXml)}");

        }

        private void _Xml_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            this._validationXml.Add(e.Message);
        }

        private void _CheckInit()
        {
            if (!this.IsInitialized)
                throw new InvalidOperationException("XmlRessourceManager is not initialized");
        }

        protected override void FlushInternal(RessourcesEditInfo editInformation)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
            };

            using (var writer = XmlWriter.Create(this._streamSource.GetSaveStream(), settings))
            {
                var stream = this._streamSource.GetStream();
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = XmlReader.Create(stream))
                {
                    //start
                    writer.WriteStartDocument();
                    //root
                    writer.WriteStartElement(PREFIX, ELEMENT_ROOT, NAMESPACE);

                    //Ressources
                    writer.WriteStartElement(PREFIX, ELEMENT_RESSOURCES, NAMESPACE);
                    //end Ressource
                    writer.WriteEndElement();
                    //End root
                    writer.WriteEndElement();
                    //end Document
                    writer.WriteEndDocument();
                }
            }
        }

        private const string ELEMENT_RESSOURCE = "Ressource";
        private const string ELEMENT_RESSOURCES = "Ressources";
        private const string ELEMENT_VALUE = "Value";
        private const string ATTRIBUTE_ID = "id";
        private const string ATTRIBUTE_TYPE = "type";
        private const string ATTRIBUTE_LANG = "lang";
        private const string RESSOURCE_SCHEMA = "Shiva.Ressources.XmlRessource.xsd";
        private const string ELEMENT_ROOT = "RessourcesDefinitions";
        private const string NAMESPACE = "http://shiva.org/XmlRessource.xsd";
        private const string PREFIX = "xr";
    }
}
