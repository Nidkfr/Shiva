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
        /// Gets the current culture.
        /// </summary>
        /// <value>
        /// The current culture.
        /// </value>
        CultureInfo CurrentCulture { get; }

        /// <summary>
        /// Gets the current UI culture.
        /// </summary>
        /// <value>
        /// The current UI culture.
        /// </value>
        CultureInfo CurrentUICulture { get; }

        /// <summary>
        /// Sets the culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        void SetCulture(CultureInfo culture);

        /// <summary>
        /// Sets the UI culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        void SetUICulture(CultureInfo culture);

        /// <summary>
        /// Occurs when [culture changed].
        /// </summary>
        event EventHandler<RessourceManagerCultureArgs> CultureChanged;

        /// <summary>
        /// Occurs when [culture UI changed].
        /// </summary>
        event EventHandler<RessourceManagerCultureArgs> CultureUIChanged;

    }

    /// <summary>
    /// Event Arg for Ressource Manager
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public sealed class RessourceManagerCultureArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceManagerCultureArgs"/> class.
        /// </summary>
        /// <param name="oldculture">The oldculture.</param>
        /// <param name="newculture">The newculture.</param>
        public RessourceManagerCultureArgs(CultureInfo oldculture ,CultureInfo newculture)
        {
            this.OldCulture = oldculture ?? throw new ArgumentNullException(nameof(oldculture));
            this.NewCulture = newculture ?? throw new ArgumentNullException(nameof(newculture));
        }

        /// <summary>
        /// Gets the old culture.
        /// </summary>
        /// <value>
        /// The old culture.
        /// </value>
        public CultureInfo OldCulture { get; private set; }

        /// <summary>
        /// Gets the new culture.
        /// </summary>
        /// <value>
        /// The new culture.
        /// </value>
        public CultureInfo NewCulture { get; private set; }
    }
}
