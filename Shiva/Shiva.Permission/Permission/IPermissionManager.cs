using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    }
}
