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
    /// <seealso cref="Shiva.Xml.XmlParser" />
    internal class RessourceXmlParser : XmlParser
    {
        public readonly RessourcesEditInfo _editInfo;
        public RessourceXmlParser(RessourcesEditInfo info)
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
            if(reader.LocalName == XD.ELEMENT_RESSOURCES)
            {
                var node = new RessourcesNodeXmlParser(this._editInfo);
                node.Update(reader, writer);
            }
        }

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteChildren(XmlWriter writer)
        {
            var node = new RessourcesNodeXmlParser(this._editInfo);
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
