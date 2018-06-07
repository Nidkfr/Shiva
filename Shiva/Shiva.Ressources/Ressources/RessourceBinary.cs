using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Shiva.Core.Identities;
using System.Globalization;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource Stream
    /// </summary>    
    public class RessourceBinary : IRessource<byte[]>
    {
        private readonly Identity _id;
        private readonly byte[] _data;
        private readonly CultureInfo _culture;

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceBinary"/> class.
        /// </summary>
        /// <param name="idRessource">The identifier ressource.</param>
        /// <param name="data">The stream.</param>
        /// <param name="culture">The culture.</param>
        public RessourceBinary(Identity idRessource, byte[] data, CultureInfo culture)
        {
            this._id = idRessource ?? throw new ArgumentNullException(nameof(idRessource));
            this._data = data ?? throw new ArgumentNullException(nameof(data));
            this._culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public byte[] Value => this._data;

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
