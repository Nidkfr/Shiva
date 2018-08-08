using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Xml;
using XD = Shiva.Permission.Xml.PermissionXmlDefinition;

namespace Shiva.Permission.Xml
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlNodeBuilder" />
    public sealed class RoleNodeXmlBuilder : XmlNodeBuilder
    {
        private readonly RoleBase _role;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleNodeXmlBuilder"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        public RoleNodeXmlBuilder(RoleBase role)
        {
            this._role = role;
        }

        /// <summary>
        /// Updates the children.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        protected override void UpdateChildren(XmlReader reader, XmlWriter writer)
        {
            
        }

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteChildren(XmlWriter writer)
        {
            switch (this._role)
            {
                case Role role:
                    writer.WriteStartElement(XD.PREFIX,XD.ELEMENT_PERMISSIONS,XD.NAMESPACE);
                    writer.WriteEndElement();
                    break;
            }
        }


        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteStartElement(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_ROLE, XD.NAMESPACE);
            writer.WriteAttributeString(XD.ATTRIBUTE_ID, this._role.Id);
        }
    }
}
