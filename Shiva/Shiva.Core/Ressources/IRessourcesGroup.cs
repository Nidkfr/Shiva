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
    public interface IRessourcesGroup :IIdentifiable
    {
        /// <summary>
        /// Gets the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        TRessource GetRessource<TRessource>(Identity ressourceID) where TRessource:IRessource;

        /// <summary>
        /// Gets the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        Task<TRessource> GetRessourceAsync<TRessource>(Identity ressourceID);
    }
}
