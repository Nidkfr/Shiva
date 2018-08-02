﻿using System;
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

            using (var reader = XmlReader.Create(this._streamSource.GetStream(), this._getReadingPermformenceSettings()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.LocalName == XD.ELEMENT_RESSOURCE)
                        {
                            var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                            if (idattr == ressourceID)
                            {
                                var typeattr = reader.GetAttribute(XD.ATTRIBUTE_TYPE);
                                if (typeattr == typeof(TRessource).FullName)
                                {
                                    var subreader = reader.ReadSubtree();
                                    while (subreader.ReadToFollowing(XD.ELEMENT_VALUE, XD.NAMESPACE))
                                    {
                                        var lang = subreader.GetAttribute(XD.ATTRIBUTE_LANG);
                                        if (lang == this.Culture.TwoLetterISOLanguageName)
                                        {
                                            return true;
                                        }
                                    }
                                    return false;

                                }
                                else continue;

                            }
                            else
                                continue;
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_RESSOURCES)
                        break;
                }
                return false;
            }
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

            using (var reader = XmlReader.Create(this._streamSource.GetStream(), this._getReadingPermformenceSettings()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.LocalName == XD.ELEMENT_RESSOURCE)
                        {
                            var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                            if (idattr == ressourceID)
                            {
                                var typeattr = reader.GetAttribute(XD.ATTRIBUTE_TYPE);
                                if (typeattr == typeof(TRessource).FullName)
                                {
                                    var subreader = reader.ReadSubtree();
                                    while (subreader.ReadToFollowing(XD.ELEMENT_VALUE, XD.NAMESPACE))
                                    {
                                        var lang = subreader.GetAttribute(XD.ATTRIBUTE_LANG);
                                        if (lang == this.Culture.TwoLetterISOLanguageName)
                                        {
                                            var ressource = new TRessource();
                                            ressource.UnSerialize(subreader, ressourceID, this.Culture);
                                            return ressource;
                                        }
                                    }
                                    return default;

                                }
                            }
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_RESSOURCES)
                        break;
                }
            }
            return default;
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
                    ValidationType = ValidationType.Schema,
                    IgnoreComments = true,
                    IgnoreProcessingInstructions = true,
                    IgnoreWhitespace = true,
                };
                settings.Schemas.Add(xsdSet);
                settings.ValidationEventHandler += new ValidationEventHandler(this._Xml_ValidationEventHandler);
                var stream = this._streamSource.GetStream();
                using (var reader = XmlReader.Create(stream, settings))
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

            var parser = new RessourceXmlBuilder(editInformation);
            parser.Update(this._streamSource.GetStream(), this._streamSource.GetSaveStream());

            this._streamSource.Flush();
            this.ValidateXml();
        }


        private XmlReaderSettings _getReadingPermformenceSettings()
        {
            return new XmlReaderSettings
            {
                CheckCharacters = false,
                ConformanceLevel = ConformanceLevel.Document,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
                ValidationFlags = XmlSchemaValidationFlags.None,
                ValidationType = ValidationType.None,
            };
        }

        /// <summary>
        /// Gets the ressource from group internal.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <returns></returns>
        protected override IEnumerable<TRessource> GetRessourceFromGroupInternal<TRessource>(Identity groupRessourceId)
        {
            this._CheckInit();
            var ressourceIds = new List<Identity>();
            //check ressource id
            using (var reader = XmlReader.Create(this._streamSource.GetStream(), this._getReadingPermformenceSettings()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.LocalName == XD.ELEMENT_GROUP)
                        {
                            var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                            if (idattr == groupRessourceId)
                            {
                                var subreader = reader.ReadSubtree();
                                while (subreader.ReadToFollowing(XD.ELEMENT_RESSOURCE, XD.NAMESPACE))
                                {
                                    var typeAttr = subreader.GetAttribute(XD.ATTRIBUTE_TYPE);
                                    if(typeAttr == typeof(TRessource).FullName)
                                       ressourceIds.Add(subreader.GetAttribute(XD.ATTRIBUTE_ID));
                                }
                                break;
                            }
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_GROUPS)
                        break;
                }
            }
            //check ressource
            var ressources = new IdentifiableList<TRessource>();
            using (var reader = XmlReader.Create(this._streamSource.GetStream(), this._getReadingPermformenceSettings()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.LocalName == XD.ELEMENT_RESSOURCE)
                        {
                            var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                            if (ressourceIds.Contains(idattr))
                            {
                                var typeattr = reader.GetAttribute(XD.ATTRIBUTE_TYPE);
                                if (typeattr == typeof(TRessource).FullName)
                                {
                                    var subreader = reader.ReadSubtree();
                                    while (subreader.ReadToFollowing(XD.ELEMENT_VALUE, XD.NAMESPACE))
                                    {
                                        var lang = subreader.GetAttribute(XD.ATTRIBUTE_LANG);
                                        if (lang == this.Culture.TwoLetterISOLanguageName)
                                        {
                                            var ressource = new TRessource();
                                            ressource.UnSerialize(subreader, idattr, this.Culture);
                                            ressources.Add(ressource);
                                            ressourceIds.Remove(idattr);
                                        }
                                    }

                                    if (ressourceIds.Count == 0)
                                        break;
                                }
                            }
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_RESSOURCES)
                        break;
                }
            }
            return ressources;
        }

        /// <summary>
        /// Gets the group ressources.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupNamespaceRessource">The group namespace ressource.</param>
        /// <param name="getChildNamespace">get child namespace</param>
        /// <returns></returns>
        public override IRessourceGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource, bool getChildNamespace)
        {
            this._CheckInit();
            var ressources = new IdentifiableList<TRessource>();
            using (var reader = XmlReader.Create(this._streamSource.GetStream(), this._getReadingPermformenceSettings()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.LocalName == XD.ELEMENT_RESSOURCE)
                        {
                            var idattr = new Identity(reader.GetAttribute(XD.ATTRIBUTE_ID));
                            var test = getChildNamespace ?
                                groupNamespaceRessource.InNamespace(idattr.Namespace) : idattr.Namespace == groupNamespaceRessource;
                            if (test)
                            {
                                var typeattr = reader.GetAttribute(XD.ATTRIBUTE_TYPE);
                                if (typeattr == typeof(TRessource).FullName)
                                {
                                    var subreader = reader.ReadSubtree();
                                    while (subreader.ReadToFollowing(XD.ELEMENT_VALUE, XD.NAMESPACE))
                                    {
                                        var lang = subreader.GetAttribute(XD.ATTRIBUTE_LANG);
                                        if (lang == this.Culture.TwoLetterISOLanguageName)
                                        {
                                            var ressource = new TRessource();
                                            ressource.UnSerialize(subreader, idattr, this.Culture);
                                            ressources.Add(ressource);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_RESSOURCES)
                        break;
                }
            }
            if (ressources.Count > 0)
                return new RessourceGroup<TRessource>(groupNamespaceRessource.ToString(), this.Culture, ressources);
            else
                return default;

        }

        /// <summary>
        /// Gets all groups internal.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IGroupInformation> GetAllGroupsInternal()
        {
            this._CheckInit();
            var result = new List<IGroupInformation>();
            using (var reader = XmlReader.Create(this._streamSource.GetStream(), this._getReadingPermformenceSettings()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.LocalName == XD.ELEMENT_GROUP)
                        {
                            var idAttr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                            var types = new List<Type>();
                            var subreader = reader.ReadSubtree();
                            while (subreader.ReadToFollowing(XD.ELEMENT_RESSOURCE, XD.NAMESPACE))
                            {
                                var typeAttr = subreader.GetAttribute(XD.ATTRIBUTE_TYPE);
                                var type = Type.GetType(typeAttr);
                                types.Add(type);
                            }
                            foreach (var ty in types.Distinct())
                            {
                                result.Add(new RessourceGroupInformation(idAttr, ty));
                            }
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_GROUPS)
                        break;
                }
            }
            return result;

        }
    }
}
