using Shiva.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;
using System.Linq;
using Shiva.Core.Identities;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// Group node parser
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlNodeBuilder" />
    public class GroupsNodeXmlBuilder : XmlNodeBuilder
    {
        private readonly RessourcesEditInfo _ressourceInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsNodeXmlBuilder"/> class.
        /// </summary>
        /// <param name="ressourcesInfo">The ressources information.</param>
        public GroupsNodeXmlBuilder(RessourcesEditInfo ressourcesInfo)
        {
            this._ressourceInfo = ressourcesInfo ?? throw new ArgumentNullException(nameof(ressourcesInfo));
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
                if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_GROUPS)
                    break;

                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == XD.ELEMENT_GROUP)
                {
                    var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                    var typeattr = reader.GetAttribute(XD.ATTRIBUTE_TYPE);
                    var type = Type.GetType(typeattr, false, true);
                    var grpInfo = new RessourceGroupInformation(idattr, type);
                    if (type != null)
                        if (this._ressourceInfo.RemovedGroups.Contains(grpInfo))
                            XmlBuilderTool.ReadToEndOfElement(reader, XD.ELEMENT_GROUP);
                        else
                        {
                            var group = this._ressourceInfo.AddedGroups.FirstOrDefault(x => x.Key == grpInfo);
                            if (group.Key != null)
                            {
                                var detachedRessource = this._ressourceInfo.DetachedRessourceGroups.ContainsKey(grpInfo) ? this._ressourceInfo.DetachedRessourceGroups[grpInfo] : new List<Identity>();
                                var node = new GroupNodeXmlBuilder(group.Key, group.Value, detachedRessource);
                                node.Update(reader, writer);
                                this._ressourceInfo.AddedGroups.Remove(grpInfo);
                            }
                            else
                            {
                                writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_GROUP, XD.NAMESPACE);
                                reader.MoveToElement();
                                writer.WriteAttributes(reader, true);
                                XmlBuilderTool.WriteToEndElement(reader, writer, XD.ELEMENT_GROUP);
                                writer.WriteEndElement();
                            }

                        }
                    else
                        XmlBuilderTool.ReadToEndOfElement(reader, XD.ELEMENT_GROUP);
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
            foreach (var grp in this._ressourceInfo.AddedGroups)
            {
                var node = new GroupNodeXmlBuilder(grp.Key, grp.Value,new List<Identity>());
                node.Write(writer);
            }
        }

        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteStartElement(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_GROUPS, XD.NAMESPACE);
        }
    }
}
