using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Core.Identities;
using Shiva.Xml;

namespace Shiva.Permission
{
    /// <summary>
    /// Simple implementation of permission
    /// </summary>
    /// <seealso cref="Shiva.Permission.IPermission" />
    public sealed class PermissionAccess : PermissionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAccess"/> class.
        /// </summary>
        /// <returns></returns>
        public PermissionAccess()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAccess"/> class.
        /// </summary>
        /// <param name="idPermission">The identifier permission.</param>
        public PermissionAccess(Identity idPermission) : base(idPermission)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PermissionAccess"/> is acces.
        /// </summary>
        /// <value>
        ///   <c>true</c> if acces; otherwise, <c>false</c>.
        /// </value>
        public bool Acces { get; set; }

        /// <summary>
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="ctx">The CTX.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Serialize(XmlWriter writer, XmlContext ctx)
        {
            writer.WriteStartElement("Acces");
            writer.WriteAttributeString("id", this.Id);
            writer.WriteAttributeString("value",this.Acces.ToString());            
            writer.WriteEndElement();
        }

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ctx">The CTX.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void UnSerialize(XmlReader reader, XmlContext ctx)
        {
            if (reader.ReadToFollowing("Acces"))
            {
                if (bool.TryParse(reader.GetAttribute("value"), out var val))
                {
                    this.Acces = val;
                }

                this.Id = reader.GetAttribute("id");
            }
            else
                throw new InvalidOperationException("Invalid Element for unserialize PermissionAccess.");
        }
    }
}
