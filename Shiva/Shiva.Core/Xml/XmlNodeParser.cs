using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Shiva.Xml
{
    /// <summary>
    /// Xml Node parser
    /// </summary>
    public abstract class XmlNodeParser
    {
        /// <summary>
        /// Writes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            this.WriteStartElement(writer);
            this.WriteChildren(writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Updates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        public void Update(XmlReader reader, XmlWriter writer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            this.WriteStartElement(writer);
            XmlParserTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
            if (reader.EOF || reader.NodeType == XmlNodeType.EndElement)
                this.WriteChildren(writer);
            else
                this.UpdateChildren(reader, writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected abstract void WriteStartElement(XmlWriter writer);

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected abstract void WriteChildren(XmlWriter writer);

        /// <summary>
        /// Updates the children.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        protected abstract void UpdateChildren(XmlReader reader, XmlWriter writer);
    }
}
