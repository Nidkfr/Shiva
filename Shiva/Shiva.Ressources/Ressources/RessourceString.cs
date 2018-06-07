using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource String
    /// </summary>    
    public class RessourceString : IRessource<string>
    {
        private readonly string _value;
        private readonly CultureInfo _culture;
        private readonly Identity _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceString"/> class.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="value">The value.</param>
        /// <param name="culture">culture</param>
        public RessourceString(Identity id, string value,CultureInfo culture)
        {
            this._value = value;
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
            this._id = id?? throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value => this._value;

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
    }
}
