using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Identities;
using System.Linq;

namespace Shiva.Permission
{
    /// <summary>
    /// Role
    /// </summary>
    /// <seealso cref="Shiva.Permission.RoleBase" />
    public sealed class Role : RoleBase
    {
        private readonly IdentifiableList<IPermission> _permissions = new IdentifiableList<IPermission>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Role(Identity id) : base(id)
        {
        }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<IPermission> GetPermissions()
        {
            return this._permissions.ToList();
        }

        /// <summary>
        /// Sets the permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        public void SetPermission(IPermission permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            this._permissions.Add(permission);
        }

        /// <summary>
        /// Removes the permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        public void RemovePermission(IPermission permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            this._permissions.Remove(permission);
        }

        /// <summary>
        /// Removes the permission.
        /// </summary>
        /// <param name="idPermission">The identifier permission.</param>
        /// <exception cref="ArgumentNullException">idPermission</exception>
        public void RemovePermission(Identity idPermission)
        {
            if (idPermission == null)
                throw new ArgumentNullException(nameof(idPermission));

            this._permissions.Remove(idPermission);
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <typeparam name="TPermission">The type of the permission.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override TPermission GetPermission<TPermission>(Identity id)
        {
            return (TPermission)this.GetPermissions().FirstOrDefault(x => x.Id == id && x.GetType() == typeof(TPermission));
        }
    }
}
