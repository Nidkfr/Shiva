using Shiva.Core.Identities;
using System;
using System.Globalization;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource Group
    /// </summary>
    /// <typeparam name="TRessource">
    /// The type of the ressource.
    /// </typeparam>
    /// <seealso cref="Shiva.Ressources.IRessourceGroup{TRessource}" />
    ///
    public sealed class RessourceGroup<TRessource> : IRessourceGroup<TRessource> where TRessource : class, IRessource
    {
        #region Private Fields

        private readonly CultureInfo _culture;
        private readonly Identity _id;
        private readonly IdentifiableList<TRessource> _ressources;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceGroup{TRessource}" /> class.
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="ressources">
        /// The ressources.
        /// </param>
        public RessourceGroup(Identity id, CultureInfo culture, IdentifiableList<TRessource> ressources)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
            this._ressources = ressources ?? throw new ArgumentNullException(nameof(ressources));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public CultureInfo Culture => this._culture;

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id => this._id;

        /// <summary>
        /// Gets the ressources.
        /// </summary>
        /// <value>
        /// The ressources.
        /// </value>
        public IdentifiableList<TRessource> Ressources => this._ressources;

        #endregion Public Properties
    }
}