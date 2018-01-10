using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace Shiva.Globalization
{
    /// <summary>
    /// Service to manage ressource globalization
    /// </summary>
    public interface IRessourceManager
    {
        /// <summary>
        /// Loads All string from culture in merory.
        /// </summary>
        /// <param name="culture">The culture, if null then current culture of ressourceManager.</param>
        /// <returns>IRessrouceStringService</returns>
        Task<IRessourceStringService> LoadStringService(CultureInfo culture=null);

        /// <summary>
        /// Change the current culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        void SetCurrentCulture(CultureInfo culture);

        /// <summary>
        /// Occurs when [current culture changed].
        /// </summary>
        event EventHandler<RessourceManagerCultureArgs> CurrentCultureChanged;

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        /// <value>
        /// The current culture.
        /// </value>
        CultureInfo CurrentCulture { get; }
    }

    /// <summary>
    /// Event Arg for Ressource Manager
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public sealed class RessourceManagerCultureArgs : EventArgs
    {

    }
}
