using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource
    /// </summary>
    public interface IRessource
    {
        /// <summary>
        /// Gets the culture of ressource.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Identity Id { get; }
    }

    /// <summary>
    /// REssource typed
    /// </summary>
    /// <typeparam name="TValue">Type of value</typeparam>
    public interface IRessource<TValue> : IRessource
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        TValue Value { get; }
    }
}
