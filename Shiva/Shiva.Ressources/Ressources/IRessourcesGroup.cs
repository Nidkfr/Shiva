using Shiva.Core.Identities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource Groups
    /// </summary>
    public interface IRessourcesGroup
    {
        /// <summary>
        /// Gets the identifier of group.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Identity Id { get; }
    }

    public interface IRessourcesGroup<TRessource> : IRessourcesGroup where TRessource:IRessource
    {
        /// <summary>
        /// Gets the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        TRessource GetRessource(Identity ressourceID);

        /// <summary>
        /// Gets the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        Task<TRessource> GetRessourceAsync(Identity ressourceID);
    }
}
