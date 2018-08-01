using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Xml;
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;
using System.Linq;


namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// Ressources Node Builder
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlNodeBuilder" />
    public class RessourcesNodeXmlBuilder : XmlNodeBuilder
    {
        private readonly RessourcesEditInfo _editInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editInfo"></param>
        public RessourcesNodeXmlBuilder(RessourcesEditInfo editInfo)
        {
            this._editInfo = editInfo ?? throw new ArgumentNullException(nameof(editInfo));
        }
        /// <summary>
        /// Updates the children.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
        protected override void UpdateChildren(XmlReader reader, XmlWriter writer)
        {
            while (!reader.EOF)
            {

                if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_RESSOURCES)
                    break;

                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == XD.ELEMENT_RESSOURCE)
                {
                    var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                    var typeattr = reader.GetAttribute(XD.ATTRIBUTE_TYPE);
                    var type = Type.GetType(typeattr, false, true);
                    if (type != null)
                        if (this._editInfo.RemovedRessources[type].ToList().Contains(idattr))
                            XmlBuilderTool.ReadToEndOfElement(reader, XD.ELEMENT_RESSOURCE);
                        else
                        {
                            var ressource = this._editInfo.AddedRessources.FirstOrDefault(x => x.Id == idattr && type == x.GetType());
                            if (ressource != null)
                            {
                                var ressourceParser = new RessourceNodeXmlBuilder(ressource);
                                ressourceParser.Update(reader, writer);
                                this._editInfo.AddedRessources.Remove(ressource);                                
                            }
                            else
                            {
                                writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
                                reader.MoveToElement();
                                writer.WriteAttributes(reader, true);
                                XmlBuilderTool.WriteToEndElement(reader, writer, XD.ELEMENT_RESSOURCE);
                                writer.WriteEndElement();
                            }
                                
                        }
                    else
                        XmlBuilderTool.ReadToEndOfElement(reader, XD.ELEMENT_RESSOURCE);
                }
                else
                    XmlBuilderTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
            }

            this.WriteChildren(writer);
        }

        /// <summary>
        /// Writes the children.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteChildren(XmlWriter writer)
        {
            foreach (var ressource in this._editInfo.AddedRessources)
            {
                var parser = new RessourceNodeXmlBuilder(ressource);
                parser.Write(writer);
            }
        }

        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteStartElement(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCES, XD.NAMESPACE);
        }
    }
}
