using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Xml;
using Shiva.Core.Identities;
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;
using System.Linq;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlNodeBuilder" />
    public class GroupNodeXmlBuilder : XmlNodeBuilder
    {
        private readonly IGroupInformation _group;
        private readonly IList<Identity> _attachedRessource;
        private readonly IEnumerable<Identity> _detachedRessource;

        /// <summary>
        /// s this instance.
        /// </summary>
        /// <returns></returns>
        public GroupNodeXmlBuilder(IGroupInformation group, IEnumerable<Identity> atachedRessource, IEnumerable<Identity> detachedRessource)
        {
            this._group = group ?? throw new ArgumentNullException(nameof(group));
            this._attachedRessource = atachedRessource?.ToList() ?? throw new ArgumentNullException(nameof(atachedRessource));
            this._detachedRessource = detachedRessource ?? throw new ArgumentNullException(nameof(detachedRessource));
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

                if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_GROUP)
                    break;

                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == XD.ELEMENT_RESSOURCE)
                {
                    var ressourceId = reader.GetAttribute(XD.ATTRIBUTE_ID);
                    if (this._detachedRessource.Contains((Identity)ressourceId))
                    {
                        XmlBuilderTool.ReadToEndOfElement(reader, XD.ELEMENT_RESSOURCE);
                    }
                    else
                    {
                        writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
                        writer.WriteAttributes(reader, true);
                        XmlBuilderTool.WriteToEndElement(reader, writer, XD.ELEMENT_RESSOURCE);
                        writer.WriteEndElement();
                        if (this._attachedRessource.Contains((Identity)ressourceId))
                            this._attachedRessource.Remove(ressourceId);

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
        /// <param name="writer">The writer.</param>
        protected override void WriteChildren(XmlWriter writer)
        {
            foreach (var ressourceId in this._attachedRessource)
            {
                writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
                writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_ID, XD.NAMESPACE);
                writer.WriteValue(ressourceId);
                writer.WriteEndAttribute();
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteStartElement(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_GROUP, XD.NAMESPACE);
            writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_ID, XD.NAMESPACE);
            writer.WriteValue(this._group.Id);
            writer.WriteEndAttribute();
            writer.WriteStartAttribute(XD.PREFIX, XD.ATTRIBUTE_TYPE, XD.NAMESPACE);
            writer.WriteValue(this._group.RessourceTargetType.FullName);
            writer.WriteEndAttribute();

        }
    }
}
