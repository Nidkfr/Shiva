using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Xml;
using Shiva.Core.Identities;

namespace Shiva.Permission
{
    /// <summary>
    /// Permission for Data
    /// </summary>
    /// <seealso cref="Shiva.Permission.PermissionBase" />
    public sealed class PermissionData : PermissionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionData"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public PermissionData(Identity id) : base(id)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionData"/> class.
        /// </summary>
        /// <returns></returns>
        public PermissionData()
        {

        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public PermissionDataEnum Mode { get; set; }

        /// <summary>
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="ctx">The CTX.</param>
        public override void Serialize(XmlWriter writer, XmlContext ctx)
        {
            writer.WriteStartElement("Data");
            writer.WriteAttributeString("id", this.Id);
            writer.WriteAttributeString("mode", this.Mode.ToString());
            writer.WriteEndElement();
        }

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ctx">The CTX.</param>
        public override void UnSerialize(XmlReader reader, XmlContext ctx)
        {
            if (reader.ReadToFollowing("Data"))
            {
                if (Enum.TryParse<PermissionDataEnum>(reader.GetAttribute("mode"), out var mode))
                {
                    this.Mode = mode;
                }
                else
                    this.Mode = PermissionDataEnum.HIDDEN;

                this.Id = reader.GetAttribute("id");
            }
            else
                new InvalidOperationException("Invalid Element for unserialize PermissionData");
        }
    }

    /// <summary>
    /// Permission Data Mode
    /// </summary>
    public enum PermissionDataEnum : byte
    {
        /// <summary>
        /// The hidden Mode
        /// </summary>
        HIDDEN = 0,

        /// <summary>
        /// The visible Mode
        /// </summary>
        VISIBLE = 1,

        /// <summary>
        /// The editable Mode
        /// </summary>
        EDITABLE = 2,
    }
}
