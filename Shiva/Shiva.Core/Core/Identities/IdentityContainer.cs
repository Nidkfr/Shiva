using System;

namespace Shiva.Core.Identities
{
    /// <summary>
    /// Id container for list
    /// </summary>
    /// <seealso cref="Shiva.Core.Identities.IIdentifiable" />
    ///
    public sealed class IdentityContainer : IIdentifiable
    {
        #region Private Fields

        private readonly Identity _id;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityContainer" /> class.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        public IdentityContainer(Identity id)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id => this._id;

        #endregion Public Properties
    }
}