namespace Shiva.Core.Identities
{
    /// <summary>
    /// Object is identifiable
    /// </summary>
    public interface IIdentifiable
    {
        #region Public Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Identity Id { get; }

        #endregion Public Properties
    }
}