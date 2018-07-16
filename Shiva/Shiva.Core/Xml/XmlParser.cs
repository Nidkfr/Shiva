using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Shiva.Xml
{
    /// <summary>
    /// Xml Parser base
    /// </summary>
    public abstract class XmlParser
    {
        /// <summary>
        /// Writes Xml on the specified stream destination.
        /// </summary>
        /// <param name="streamDestination">The stream destination.</param>
        public void Write(Stream streamDestination)
        {
            using (var writer = XmlWriter.Create(streamDestination,new XmlWriterSettings { Indent=true }))
            {
                writer.WriteStartDocument();
                this.WriteStartRoot(writer);
                
                this.WriteChildren(writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Updates the specified stream origin On stream destination.
        /// </summary>
        /// <param name="streamOrigin">The stream origin.</param>
        /// <param name="streamDestination">The stream destination.</param>
        public void Update(Stream streamOrigin,Stream streamDestination)
        {
            streamOrigin.Seek(0, SeekOrigin.Begin);
            using (var reader = XmlReader.Create(streamOrigin))
            {
                using (var writer = XmlWriter.Create(streamDestination, new XmlWriterSettings { Indent = true, }))
                {
                    writer.WriteStartDocument();
                    XmlParserTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
                    this.WriteStartRoot(writer);
                    XmlParserTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
                    if (reader.EOF || reader.NodeType == XmlNodeType.EndElement)
                        this.WriteChildren(writer);
                    else
                    {
                        this.UpdateChildren(reader, writer);
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
        }

        /// <summary>
        /// Writes the start root.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected abstract void WriteStartRoot(XmlWriter writer);

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
