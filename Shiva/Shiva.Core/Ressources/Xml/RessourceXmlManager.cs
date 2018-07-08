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
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;
using Shiva.Xml;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// Xml ressource Manager
    /// </summary>
    public class RessourceXmlManager : RessourceManagerBase, IDisposable
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
        /// Initializes a new instance of the <see cref="RessourceXmlManager"/> class.
        /// </summary>
        /// <param name="logmanager">The logmanager.</param>
        public RessourceXmlManager(ILogManager logmanager = null) : base(logmanager)
        {
            this.Logger = logmanager?.CreateLogger<RessourceXmlManager>() ?? new NoLogger();
        }

        /// <summary>
        /// Intializes the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="source">The data XML.</param>
        public void Initialize(CultureInfo culture, StreamSource source)
        {
            this.IsInitialized = false;
            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Initialize Ressource mananger to culture {culture}");

            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
            this._streamSource = source ?? throw new ArgumentNullException(nameof(source));

            this.ValidateXml();

            this.IsInitialized = true;
        }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public override CultureInfo Culture => this._culture;

        /// <summary>
        /// Finalizes an instance of the <see cref="RessourceXmlManager" /> class.
        /// </summary>
        ~RessourceXmlManager()
        {
            this.Dispose();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            if (this.Logger.DebugIsEnabled)
                this.Logger.Debug("Ressource Manager is disposed");
            this._streamSource?.Dispose();
        }

        ///// <summary>
        ///// Gets the group ressources.
        ///// </summary>
        ///// <typeparam name="TRessource">The type of the ressource.</typeparam>
        ///// <param name="groupRessourceId">The group ressource identifier.</param>
        ///// <returns></returns>
        //public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId)
        //{
        //    this._CheckInit();
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Gets the group ressources.
        ///// </summary>
        ///// <typeparam name="TRessource">The type of the ressource.</typeparam>
        ///// <param name="groupNamespaceRessource">The group namespace ressource.</param>
        ///// <returns></returns>
        //public override IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource)
        //{
        //    this._CheckInit();
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Determines whether [contains ressource internal] [the specified ressource identifier].
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns>
        ///   <c>true</c> if [contains ressource internal] [the specified ressource identifier]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool ContainsRessourceInternal<TRessource>(Identity ressourceID)
        {
            this._CheckInit();
            
            return false;
        }

        /// <summary>
        /// Gets all groups internal.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Identity> GetAllGroupsInternal()
        {
            this._CheckInit();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the ressource internal.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        protected override TRessource GetRessourceInternal<TRessource>(Identity ressourceID)
        {
            this._CheckInit();
            return this._GetRessource<TRessource>(ressourceID, typeof(TRessource));                
        }

        private TRessource _GetRessource<TRessource>(Identity ressourceID, Type type) where TRessource : IRessource, new()
        {
            this._streamSource.GetStream().Seek(0,SeekOrigin.Begin);
            using (var reader = XmlReader.Create(this._streamSource.GetStream()))
            {
                if(XmlParserTool.MoveToElement(reader,XD.ELEMENT_RESSOURCE,x=>x[XD.ATTRIBUTE_ID] == ressourceID && x[XD.ATTRIBUTE_TYPE] == type.FullName))
                {
                    if (XmlParserTool.MoveToElement(reader, XD.ELEMENT_VALUE, x => x[XD.ATTRIBUTE_LANG] == this.Culture.TwoLetterISOLanguageName))
                    {
                        var ressource = new TRessource();
                        ressource.UnSerialize(reader, ressourceID, this.Culture);
                        return ressource;
                    }
                }
            }
            return default(TRessource);
        }

        /// <summary>
        /// Validates the XML.
        /// </summary>
        public void ValidateXml()
        {
            this._validationXml.Clear();
            var ressource = this.GetType().Assembly.GetManifestResourceNames();
            var schema = XmlSchema.Read(this.GetType().Assembly.GetManifestResourceStream(XD.RESSOURCE_SCHEMA), this._Xml_ValidationEventHandler);

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

        /// <summary>
        /// Flushes the internal.
        /// </summary>
        /// <param name="editInformation">The edit information.</param>
        protected override void FlushInternal(RessourcesEditInfo editInformation)
        {

            var parser = new RessourceXmlParser(editInformation);
            parser.Update(this._streamSource.GetStream(), this._streamSource.GetSaveStream());

            this._streamSource.Flush();
            this.ValidateXml();
        }



    }
}
