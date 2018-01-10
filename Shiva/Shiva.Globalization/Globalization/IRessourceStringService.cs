using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Globalization
{
    /// <summary>
    /// REsource string Service
    /// </summary>
    public interface IRessourceStringService
    {
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        string GetString(string Id);
    }
}
