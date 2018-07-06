using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressource Groupe base
    /// </summary>
    /// <seealso cref="Shiva.Ressources.IRessourcesGroup" />
    public abstract class RessourceGroupeBase : IRessourcesGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceGroupeBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected RessourceGroupeBase(Identity id)
        {
            this.Id = id ??throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Identity Id { get; private set; }

        /// <summary>
        /// Gets the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        public abstract TRessource GetRessource<TRessource>(Identity ressourceID) where TRessource : IRessource;

        /// <summary>
        /// Gets the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        public abstract Task<TRessource> GetRessourceAsync<TRessource>(Identity ressourceID);
    }
}
