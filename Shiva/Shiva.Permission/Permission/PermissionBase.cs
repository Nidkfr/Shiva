using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Shiva.Core.Identities;
using Shiva.Xml;

namespace Shiva.Permission
{
    /// <summary>
    /// Permission Base
    /// </summary>
    /// <seealso cref="Shiva.Permission.IPermission" />
    public abstract class PermissionBase : IPermission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"></see> class.
        /// </summary>
        /// <returns></returns>
        protected PermissionBase()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionBase"/> class.
        /// </summary>
        /// <param name="idPermission">The identifier permission.</param>
        protected PermissionBase(Identity idPermission)
        {
            this.Id = idPermission ?? throw new ArgumentNullException(nameof(idPermission));
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id
        {
            get;
            protected set;
        }

        

        /// <summary>
        /// Gets a value indicating whether this instance is empty ressource.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty ressource; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmptyPermission =>this.Id == null;

        /// <summary>
        /// Serializes the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="ctx">The CTX.</param>
        public abstract void Serialize(XmlWriter writer, XmlContext ctx);

        /// <summary>
        /// Uns the serialize.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ctx">The CTX.</param>
        public abstract void UnSerialize(XmlReader reader, XmlContext ctx);
    }
}
