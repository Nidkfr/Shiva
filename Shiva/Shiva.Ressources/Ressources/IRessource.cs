using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

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
    }
}
