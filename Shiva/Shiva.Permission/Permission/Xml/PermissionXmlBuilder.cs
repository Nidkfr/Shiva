using Shiva.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XD = Shiva.Permission.Xml.PermissionXmlDefinition;

namespace Shiva.Permission.Xml
{
    /// <summary>
    /// Xml builder of permission file
    /// </summary>
    public sealed class PermissionXmlBuilder : XmlBuilder
    {
        private readonly RoleBase _role;        
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionXmlBuilder"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        public PermissionXmlBuilder(RoleBase role)
        {
            this._role = role ?? throw new ArgumentNullException(nameof(role));
        }

        /// <summary>
        /// Updates the children.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        protected override void UpdateChildren(XmlReader reader, XmlWriter writer)
        {            
            do
            {
                if (reader.LocalName == XD.ELEMENT_ROLES)
                {
                    var subreader = reader.ReadSubtree();

                    while(subreader.EOF)
                    {
                        XmlBuilderTool.ReadAndWriteToNextStartOrEndElement(subreader, writer);
                        if(subreader.LocalName ==  XD.ELEMENT_ROLE && this._role != null)
                        {
                            var attrId = subreader.GetAttribute(XD.ATTRIBUTE_ID);
                            if (attrId == this._role.Id)
                                XmlBuilderTool.ReadToEndOfElement(subreader, XD.ELEMENT_ROLE);
                        }
                    }
                    if(this._role !=null)
                    {
                        var node = new RoleNodeXmlBuilder(this._role);
                        node.Write(writer);
                    }
                }                
                XmlBuilderTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
            }
            while (!reader.EOF);            
        }

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteChildren(XmlWriter writer)
        {
            var node = new RoleNodeXmlBuilder(this._role);
            node.Write(writer);
        }

        /// <summary>
        /// Writes the start root.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteStartRoot(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_ROOT, XD.NAMESPACE);
        }
    }
}
