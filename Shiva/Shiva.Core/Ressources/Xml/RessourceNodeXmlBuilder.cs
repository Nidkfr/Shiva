using Shiva.Xml;
using System;
using System.Xml;
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// Ressource node parser
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlNodeBuilder" />
    ///
     sealed class RessourceNodeXmlBuilder : XmlNodeBuilder
    {
        #region Private Fields

        private readonly IRessource _ressource;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceNodeXmlBuilder" /> class.
        /// </summary>
        /// <param name="ressource">
        /// The ressource.
        /// </param>
        public RessourceNodeXmlBuilder(IRessource ressource)
        {
            this._ressource = ressource;
        }

        #endregion Public Constructors

        #region Protected Methods

        /// <summary>
        /// Updates the children.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void UpdateChildren(XmlReader reader, XmlWriter writer)
        {
            while (!reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_RESSOURCE)
                    break;

                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == XD.ELEMENT_VALUE)
                {
                    var langattr = reader.GetAttribute(XD.ATTRIBUTE_LANG);
                    if (langattr == this._ressource.Culture.TwoLetterISOLanguageName)
                    {
                        reader.Skip();
                    }
                    else
                    {
                        writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_VALUE, XD.NAMESPACE);
                        writer.WriteAttributes(reader, true);
                        XmlBuilderTool.WriteToEndElement(reader, writer, XD.ELEMENT_VALUE);
                        writer.WriteEndElement();
                    }
                }
                else
                    XmlBuilderTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
            }

            this.WriteChildren(writer);
        }

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void WriteChildren(XmlWriter writer)
        {            
            this._ressource.Serialize(writer,new XmlContext (XD.NAMESPACE,XD.PREFIX));            
        }

        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        protected override void WriteStartElement(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
            writer.WriteStartAttribute(XD.ATTRIBUTE_ID);
            writer.WriteValue(this._ressource.Id);
            writer.WriteEndAttribute();
            writer.WriteStartAttribute(XD.ATTRIBUTE_TYPE);
            writer.WriteValue(this._ressource.GetType().FullName);
            writer.WriteEndAttribute();
        }

        #endregion Protected Methods
    }
}