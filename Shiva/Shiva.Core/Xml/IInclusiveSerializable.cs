using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shiva.Xml
{
    /// <summary>
    /// Serialize inside Xml
    /// </summary>
    public interface IInclusiveSerializable
    {
        /// <summary>
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="ctx">xml context</param>
        void Serialize(XmlWriter writer, XmlContext ctx);

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ctx">xml context</param>
        void UnSerialize(XmlReader reader, XmlContext ctx);
    }
}
