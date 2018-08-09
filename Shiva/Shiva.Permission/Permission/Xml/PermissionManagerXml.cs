using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Services;
using Shiva.Core.IO;
using System.Xml.Schema;
using System.Xml;
using XD = Shiva.Permission.Xml.PermissionXmlDefinition;
using Shiva.Core.Identities;

namespace Shiva.Permission.Xml
{
    /// <summary>
    /// Permission Manager Base
    /// </summary>
    /// <seealso cref="Shiva.Permission.PermissionManagerBase" />
    public sealed class PermissionManagerXml : PermissionManagerBase, IDisposable
    {
        private readonly IList<string> _validationXml = new List<string>();
        private StreamSource _source = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionManagerXml"/> class.
        /// </summary>
        /// <param name="logmanager">The logmanager.</param>
        public PermissionManagerXml(ILogManager logmanager) : base(logmanager)
        {

        }

        /// <summary>
        /// Initializes with the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        public void Initialize(FileSource source)
        {
            if (this.Logger.InfoIsEnabled)
                this.Logger.Info("Initialization of permission manager xml");

            this._source = source;
            this._validateXml();
            this.IsInitialized = true;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized
        {
            get;
            private set;

        }

        private void _CheckInit()
        {
            if (!this.IsInitialized)
                throw new InvalidOperationException("Permission Manager Xml is not Initialized");
        }

        /// <summary>
        /// Validates the XML.
        /// </summary>
        private void _validateXml()
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
                var stream = this._source.GetStream();
                using (var reader = XmlReader.Create(stream, settings))
                    while (reader.Read()) ;

                if (this._validationXml.Count > 0)
                    throw new XmlSchemaValidationException($"Permission Xml stream validation fail :\n\r {string.Join("\n\r", this._validationXml)}");
            }
            else
                throw new XmlSchemaValidationException($"Permission Xml schema is not valid :\n\r {string.Join("\n\r", this._validationXml)}");
        }

        private void _Xml_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            this._validationXml.Add(e.Message);
            if (this.Logger.ErrorIsEnabled)
                this.Logger.Error($"Xml Validation Fail : {e.Message}");
        }

        /// <summary>
        /// Sets the role internal.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void SetRoleInternal(RoleBase role)
        {
            var builder = new PermissionXmlBuilder(role);
            builder.Update(this._source.GetStream(), this._source.GetSaveStream());

            this._source.Flush();
            this._validateXml();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this._source?.Dispose();
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="roleId">The identifier.</param>
        /// <returns></returns>        
        public override TRole GetRole<TRole>(Identity roleId)
        {
            this._CheckInit();
            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            using (var reader = XmlReader.Create(this._source.GetStream(), this._getReadingPermformenceSettings()))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.LocalName == XD.ELEMENT_ROLE)
                        {
                            var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                            if (idattr == roleId)
                            {
                                if (typeof(TRole) == typeof(Role))
                                    return this._getRole(reader, idattr) as TRole;
                               
                            }
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_ROLES)
                        break;
                }
            }
            return default;

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

        private Role _getRole(XmlReader reader,Identity roleId)
        {
            var role = new Role(roleId);            
            var permissionsTypeSubReader = reader.ReadSubtree();           
            while (permissionsTypeSubReader.Read())
            {
                if (permissionsTypeSubReader.IsStartElement())
                {
                    if (permissionsTypeSubReader.LocalName == XD.ELEMENT_PERMISSIONS)
                    {
                        var type = Type.GetType(permissionsTypeSubReader.GetAttribute(XD.ATTRIBUTE_TYPE), false, true);
                        if (type != null)
                        {
                            var ctor = type.GetConstructor(new Type[] { });
                            var permissionsSubReader = permissionsTypeSubReader.ReadSubtree();
                            while (permissionsSubReader.Read())
                            {
                                if (permissionsSubReader.IsStartElement())
                                {
                                    var permission = ctor.Invoke(null) as IPermission;
                                    permission.UnSerialize(permissionsSubReader, new Shiva.Xml.XmlContext(XD.NAMESPACE, XD.PREFIX));
                                    role.SetPermission(permission);
                                }
                            }
                        }
                        permissionsTypeSubReader.Skip();
                    }
                }
            }
            return role;
        }
    }
}
