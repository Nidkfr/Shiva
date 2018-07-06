using Shiva.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// Ressource node parser
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlNodeParser" />
    public sealed class RessourceNodeXmlParser : XmlNodeParser
    {
        private readonly IRessource _ressource;

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceNodeXmlParser"/> class.
        /// </summary>
        /// <param name="ressource">The ressource.</param>
        public RessourceNodeXmlParser(IRessource ressource)
        {
            this._ressource = ressource??throw new ArgumentNullException(nameof(ressource));
        }
        /// <summary>
        /// Updates the children.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void UpdateChildren(XmlReader reader, XmlWriter writer)
        {
           
        }

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteChildren(XmlWriter writer)
        {

        }

        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void WriteStartElement(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
            writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_ID, XD.NAMESPACE);
            writer.WriteValue(this._ressource.Id);
            writer.WriteEndAttribute();
            writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_TYPE, XD.NAMESPACE);
            writer.WriteValue(this._ressource.GetType().FullName);
            writer.WriteEndAttribute();
        }
    }
}
