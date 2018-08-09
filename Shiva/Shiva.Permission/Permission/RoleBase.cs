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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{this.GetType().Name}]({this.Id})";
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <typeparam name="TPermission">The type of the permission.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public abstract TPermission GetPermission<TPermission>(Identity id);
    }
}
