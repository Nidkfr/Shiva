using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shiva.Xml
{
    /// <summary>
    /// Xml Parser Tool
    /// </summary>
    public static class XmlParserTool
    {
        /// <summary>
        /// Reads and write to next start or end element.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        public static void ReadAndWriteToNextStartOrEndElement(XmlReader reader, XmlWriter writer)
        {
            if (!reader.EOF)
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Comment:
                            writer.WriteComment(reader.Value);
                            break;
                        case XmlNodeType.CDATA:
                            writer.WriteCData(reader.Value);
                            break;
                        case XmlNodeType.Text:
                            writer.WriteValue(reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                        case XmlNodeType.Element: return;
                    }
                }
        }
    }
}
