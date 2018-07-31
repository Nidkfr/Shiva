using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// Id container
    /// </summary>
    /// <seealso cref="Shiva.Core.Identities.IIdentifiable" />
    public sealed class IdentityContainer : IIdentifiable
    {
        private readonly Identity _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityContainer"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public IdentityContainer(Identity id)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
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
