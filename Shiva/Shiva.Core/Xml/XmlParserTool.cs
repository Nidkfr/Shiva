using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;

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


        /// <summary>
        /// Moves to element.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="hieachicElementExpression">The hieachic element expression.</param>
        /// <param name="elementSelector">The element selector.</param>
        /// <returns></returns>
        public static bool MoveToElement(XmlReader reader, string hieachicElementExpression, Predicate<IDictionary<string,string>> elementSelector = null)
        {            
            var path = new Stack<string>();
            var pathExpressionLevel = hieachicElementExpression.Split('\\').Length;
            
            do
            {
                if(reader.NodeType == XmlNodeType.Element)
                {
                    path.Push(reader.LocalName);
                    var currentPath = string.Join("\\", path.ToArray().Take(pathExpressionLevel));
                    if (currentPath == hieachicElementExpression)
                    {
                        var attrList = new Dictionary<string, string>();
                        if (elementSelector != null)
                        {
                            if (reader.MoveToFirstAttribute())
                            {
                                do
                                {
                                    attrList.Add(reader.LocalName, reader.ReadContentAsString());
                                }
                                while (reader.MoveToNextAttribute());
                            }
                            if (elementSelector.Invoke(attrList))
                            {
                                reader.MoveToContent();
                                return true;
                            }
                        }
                        else
                        {
                            reader.MoveToContent();
                            return true;
                        }
                    }
                }
            }
            while (reader.Read());
            return false;
        }
    }
}
