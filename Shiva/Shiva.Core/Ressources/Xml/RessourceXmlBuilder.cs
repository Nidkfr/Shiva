using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Xml;
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// XmlParser
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlBuilder" />
    internal class RessourceXmlBuilder : XmlBuilder
    {
        public readonly RessourcesEditInfo _editInfo;

        private bool RessourceCheck { get; set; }

        private bool GroupsCheck { get; set; }

        public RessourceXmlBuilder(RessourcesEditInfo info)
        {
            this._editInfo = info ?? throw new ArgumentNullException(nameof(info));
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
                if (reader.LocalName == XD.ELEMENT_RESSOURCES)
                {
                    var node = new RessourcesNodeXmlBuilder(this._editInfo);
                    node.Update(reader, writer);
                    this.RessourceCheck = true;
                }

                if (reader.LocalName == XD.ELEMENT_GROUPS)
                {
                    var node = new GroupsNodeXmlBuilder(this._editInfo);
                    node.Update(reader, writer);
                    this.GroupsCheck = true;
                }
                XmlBuilderTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
            }
            while (!reader.EOF);
            
            this.WriteChildren(writer);
        }

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteChildren(XmlWriter writer)
        {
            if (!this.RessourceCheck)
            {
                var node = new RessourcesNodeXmlBuilder(this._editInfo);
                node.Write(writer);
            }

            if (!this.GroupsCheck)
            {
                var gnode = new GroupsNodeXmlBuilder(this._editInfo);
                gnode.Write(writer);
            }
        }

        /// <summary>
        /// Writes the start root.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteStartRoot(XmlWriter writer)
        {
            this.RessourceCheck = false;
            this.GroupsCheck = false;
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_ROOT, XD.NAMESPACE);
            writer.WriteAttributeString("xmlns", XD.PREFIX, null, XD.NAMESPACE);
        }
    }
}
