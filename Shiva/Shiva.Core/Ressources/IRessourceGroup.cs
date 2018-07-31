using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Identities;
using System.Globalization;

namespace Shiva.Ressources
{
    /// <summary>
    /// IRessourceGroup
    /// </summary>
    public interface IRessourceGroup<TRessource> where TRessource:class,IRessource
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Identity Id { get; }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the ressources.
        /// </summary>
        /// <value>
        /// The ressources.
        /// </value>
        IdentifiableList<TRessource> Ressources { get; }
    }
}
