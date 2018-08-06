using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Identities;

namespace Shiva.Permission
{
    /// <summary>
    /// Permission Base
    /// </summary>
    /// <seealso cref="Shiva.Permission.IPermission" />
    public abstract class PermissionBase : IPermission
    {
        private readonly Identity _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionBase"/> class.
        /// </summary>
        /// <param name="idPermission">The identifier permission.</param>
        protected PermissionBase(Identity idPermission)
        {
            this._id = idPermission ?? throw new ArgumentNullException(nameof(idPermission));
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id => this._id;
    }
}
