using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource definition base
    /// </summary>
    public abstract class RessourceBase : IRessource
    {
        private readonly Identity _id;
        private CultureInfo _culture;

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="culture">The culture.</param>
        protected RessourceBase (Identity id, CultureInfo culture =null)
        {
            this._id = id ?? throw new ArgumentNullException(nameof(id));
            this._culture = culture;
        }

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
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public abstract IRessource Clone();

        /// <summary>
        /// Sets the culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public void SetCulture(CultureInfo culture)
        {
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        
    }
}
