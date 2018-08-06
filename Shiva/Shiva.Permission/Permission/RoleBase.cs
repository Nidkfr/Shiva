using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shiva.Core.Identities;

namespace Shiva.Permission
{
    /// <summary>
    /// Role base 
    /// </summary>
    public abstract class RoleBase : IIdentifiable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public RoleBase(Identity id)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id
        { get; private set; }


        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<IPermission> GetPermissions();

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{this.GetType().Name}]({this.Id})";
        }

    }
}
