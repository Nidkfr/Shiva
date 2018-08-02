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
          //code is manage in GroupsNodeXmlBuilder
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
                writer.WriteStartAttribute(XD.ATTRIBUTE_ID);
                writer.WriteValue(ressourceId);
                writer.WriteEndAttribute();
                writer.WriteStartAttribute(XD.ATTRIBUTE_TYPE);
                writer.WriteValue(this._group.RessourceTargetType.FullName);
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
            writer.WriteStartAttribute(XD.ATTRIBUTE_ID);
            writer.WriteValue(this._group.Id);
            writer.WriteEndAttribute();
        }
    }
}
