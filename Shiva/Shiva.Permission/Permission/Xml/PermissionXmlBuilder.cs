using Shiva.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XD = Shiva.Permission.Xml.PermissionXmlDefinition;

namespace Shiva.Permission.Xml
{
    /// <summary>
    /// Xml builder of permission file
    /// </summary>
    public sealed class PermissionXmlBuilder : XmlBuilder
    {
        /// <summary>
        /// Updates the children.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="writer">The writer.</param>
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
        /// Writes the start root.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteStartRoot(XmlWriter writer)
        {
            writer.WriteStartElement(XD.PREFIX, XD.ELEMENT_ROOT, XD.NAMESPACE);
        }
    }
}
