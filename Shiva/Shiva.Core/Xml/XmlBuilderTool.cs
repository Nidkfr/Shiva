﻿using System;
using System.Xml;

namespace Shiva.Xml
{
    /// <summary>
    /// Xml Parser Tool
    /// </summary>
    public static class XmlBuilderTool
    {
        #region Public Methods

        /// <summary>
        /// Reads and write to next start or end element.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
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

        /// <summary>
        /// Writes to end element.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="elementName">
        /// Name of the element.
        /// </param>
        public static void WriteToEndElement(XmlReader reader, XmlWriter writer, string elementName)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            while (reader.Read())
            {
                if (reader.EOF)
                    break;

                if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == elementName)
                    break;

                switch (reader.NodeType)
                {
                    case XmlNodeType.Attribute: writer.WriteAttributes(reader, true); break;
                    case XmlNodeType.CDATA: writer.WriteCData(reader.Value); break;
                    case XmlNodeType.Comment: writer.WriteComment(reader.Value); break;
                    case XmlNodeType.Element:
                        writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                        writer.WriteAttributes(reader, true);
                        if (reader.IsEmptyElement)
                            writer.WriteEndElement();
                        break;

                    case XmlNodeType.EndElement: writer.WriteEndElement(); break;
                    case XmlNodeType.Text: writer.WriteString(reader.Value); break;
                    case XmlNodeType.Whitespace: writer.WriteWhitespace(reader.Value); break;
                    case XmlNodeType.ProcessingInstruction: writer.WriteProcessingInstruction(reader.Name, reader.Value); break;
                    case XmlNodeType.SignificantWhitespace: writer.WriteWhitespace(reader.Value); break;
                }
            }
        }

        #endregion Public Methods
    }
}