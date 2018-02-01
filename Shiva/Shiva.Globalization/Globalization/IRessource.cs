using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using Shiva.Core.Caches;

namespace Shiva.Globalization
{
    /// <summary>
    /// Ressource
    /// </summary>
    public interface IRessourceReader : ICachable
    {
        /// <summary>
        /// Gets the culture of ressource.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        CultureInfo Culture { get; }

    }

    /// <summary>
    /// Typed Ressource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRessourceReader<T> : IRessourceReader
    {

    }
}
