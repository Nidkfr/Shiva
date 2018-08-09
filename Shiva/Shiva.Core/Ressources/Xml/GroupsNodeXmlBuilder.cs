using Shiva.Core.Identities;
using Shiva.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using XD = Shiva.Ressources.Xml.RessourceXmlDefinitions;

namespace Shiva.Ressources.Xml
{
    /// <summary>
    /// Group node parser
    /// </summary>
    /// <seealso cref="Shiva.Xml.XmlNodeBuilder" />
    ///
    sealed class GroupsNodeXmlBuilder : XmlNodeBuilder
    {
        #region Private Fields

        private readonly RessourcesEditInfo _ressourceInfo;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsNodeXmlBuilder" /> class.
        /// </summary>
        /// <param name="ressourcesInfo">
        /// The ressources information.
        /// </param>
        public GroupsNodeXmlBuilder(RessourcesEditInfo ressourcesInfo)
        {
            this._ressourceInfo = ressourcesInfo ?? throw new ArgumentNullException(nameof(ressourcesInfo));
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
                if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == XD.ELEMENT_GROUPS)
                    break;

                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == XD.ELEMENT_GROUP)
                {
                    var idattr = reader.GetAttribute(XD.ATTRIBUTE_ID);
                    if (this._ressourceInfo.RemovedGroups.Contains((Identity)idattr))
                        reader.Skip();
                    else
                    {
                        if (this._ressourceInfo.AddedGroups.Any(x => x.Key.Id == idattr))
                        {
                            var grps = this._ressourceInfo.AddedGroups.Where(x => x.Key.Id == idattr).ToList();

                            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_GROUP, XD.NAMESPACE);
                            writer.WriteStartAttribute(XD.ATTRIBUTE_ID);
                            writer.WriteValue(idattr);
                            writer.WriteEndAttribute();

                            foreach (var grp in grps)
                            {
                                var subtreeReader = reader.ReadSubtree();
                                var detachedRessources = this._ressourceInfo.DetachedRessourceGroups.ContainsKey(grp.Key) ? this._ressourceInfo.DetachedRessourceGroups[grp.Key] : new List<Identity>();
                                var attachedRessource = grp.Value.ToList();
                                while (subtreeReader.ReadToFollowing(XD.ELEMENT_RESSOURCE, XD.NAMESPACE))
                                {
                                    var resIdAttr = subtreeReader.GetAttribute(XD.ATTRIBUTE_ID);
                                    if (detachedRessources.Contains((Identity)resIdAttr))
                                        continue;

                                    writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
                                    writer.WriteAttributes(subtreeReader, true);
                                    writer.WriteEndElement();
                                    attachedRessource.Remove(resIdAttr);
                                }
                                foreach (var ressourceId in attachedRessource)
                                {
                                    writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_RESSOURCE, XD.NAMESPACE);
                                    writer.WriteStartAttribute(XD.ATTRIBUTE_ID);
                                    writer.WriteValue(ressourceId);
                                    writer.WriteEndAttribute();
                                    writer.WriteStartAttribute(XD.ATTRIBUTE_TYPE);
                                    writer.WriteValue(grp.Key.RessourceTargetType.FullName);
                                    writer.WriteEndAttribute();
                                    writer.WriteEndElement();
                                }
                                this._ressourceInfo.AddedGroups.Remove(grp);
                            }
                            writer.WriteEndElement();
                        }
                        else
                        {
                            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_GROUP, XD.NAMESPACE);
                            writer.WriteAttributes(reader, true);
                            XmlBuilderTool.WriteToEndElement(reader, writer, XD.ELEMENT_GROUP);
                            writer.WriteEndElement();
                        }
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
            foreach (var grp in this._ressourceInfo.AddedGroups)
            {
                var node = new GroupNodeXmlBuilder(grp.Key, grp.Value, new List<Identity>());
                node.Write(writer);
            }
        }

        /// <summary>
        /// Writes the start element.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void WriteStartElement(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_GROUPS, XD.NAMESPACE);
        }

        #endregion Protected Methods
    }
}