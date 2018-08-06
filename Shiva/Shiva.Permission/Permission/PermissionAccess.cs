using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Identities;

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

        

        
    }
}
