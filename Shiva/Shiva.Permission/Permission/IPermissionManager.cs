using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shiva.Core.Identities;

namespace Shiva.Permission
{
    /// <summary>
    /// Permission Manager
    /// </summary>
    public interface IPermissionManager
    {
        /// <summary>
        /// Sets the role.
        /// </summary>
        /// <param name="role">The role.</param>
        void SetRole(RoleBase role);

        /// <summary>
        /// Sets the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancelToken">Cancel token</param>
        /// <returns></returns>
        Task SetRoleAsync(RoleBase role, CancellationToken? cancelToken = null);

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="id">The identifier.</param>
        TRole GetRole<TRole>(Identity id) where TRole:RoleBase;

        /// <summary>
        /// Gets the role asynchronous.
        /// </summary>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns></returns>
        Task<TRole> GetRoleAsync<TRole>(Identity id, CancellationToken? cancelToken = null) where TRole : RoleBase;
    }
}
