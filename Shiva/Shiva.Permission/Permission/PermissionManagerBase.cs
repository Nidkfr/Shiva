using Shiva.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shiva.Permission
{
    /// <summary>
    /// Permission Manager Base
    /// </summary>
    /// <seealso cref="Shiva.Permission.IPermissionManager" />
    public abstract class PermissionManagerBase : IPermissionManager
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionManagerBase"/> class.
        /// </summary>
        /// <param name="logmanager">The logmanager.</param>
        protected PermissionManagerBase(ILogManager logmanager)
        {
            this._logger = logmanager?.CreateLogger(this.GetType().FullName) ?? new NoLogger();
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger => this._logger;

        /// <summary>
        /// Sets the role. it's override previous version
        /// </summary>
        /// <param name="role">The role.</param>        
        public void SetRole(RoleBase role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info("Set Role {0} in permission manager.",role.Id);

            this.SetRoleInternal(role);

        }

        /// <summary>
        /// Sets the role internal.
        /// </summary>
        /// <param name="role">The role.</param>
        protected abstract void SetRoleInternal(RoleBase role);

        /// <summary>
        /// Sets the role asynchronous.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="cancelToken">Cancel token</param>
        /// <returns></returns>        
        public async Task SetRoleAsync(RoleBase role, CancellationToken? cancelToken = null)
        {
            await Task.Run(() => this.SetRole(role), cancelToken ?? CancellationToken.None);
        }
    }
}
