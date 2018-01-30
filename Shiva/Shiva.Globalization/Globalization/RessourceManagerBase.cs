using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Shiva.Globalization
{
    /// <summary>
    /// Base implementation of IRessourceManager
    /// </summary>
    public abstract class RessourceManagerBase : IRessourceManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceManagerBase"/> class.
        /// </summary>
        public RessourceManagerBase()
        {
            this.CurrentCulture = CultureInfo.CurrentCulture;
            this.CurrentUICulture = CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        /// <value>
        /// The current culture.
        /// </value>
        public CultureInfo CurrentCulture
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current UI culture.
        /// </summary>
        /// <value>
        /// The current UI culture.
        /// </value>
        public CultureInfo CurrentUICulture
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when [culture changed].
        /// </summary>
        public event EventHandler<RessourceManagerCultureArgs> CultureChanged;

        /// <summary>
        /// Occurs when [culture UI changed].
        /// </summary>
        public event EventHandler<RessourceManagerCultureArgs> CultureUIChanged;

        /// <summary>
        /// Sets the culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public void SetCulture(CultureInfo culture)
        {
            var old = this.CurrentCulture ?? CultureInfo.CurrentCulture;
            this.CurrentCulture = culture ?? throw new ArgumentNullException(nameof(culture));
            this.CultureChanged?.Invoke(this, new RessourceManagerCultureArgs(old, this.CurrentCulture));
        }

        /// <summary>
        /// Sets the UI culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public void SetUICulture(CultureInfo culture)
        {
            var old = this.CurrentUICulture ?? CultureInfo.CurrentUICulture;
            this.CurrentUICulture = culture ?? throw new ArgumentNullException(nameof(culture));
            this.CultureUIChanged?.Invoke(this, new RessourceManagerCultureArgs(old, this.CurrentUICulture));
        }
    }
}
