using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Shiva.Core.Identities;
using System.Threading.Tasks;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressources Management
    /// </summary>
    public interface IRessourceManager
    {
        /// <summary>
        /// Gets the culture of ressources.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        CultureInfo Culture { get; }

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
        Task<TRessource> GetRessourceAsync<TRessource>(Identity ressourceId) where TRessource : IRessource;

        /// <summary>
        /// Saves the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressource">The ressource.</param>
        void SaveRessource<TRessource>(TRessource ressource) where TRessource : IRessource;

        /// <summary>
        /// Saves the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressource">The ressource.</param>
        /// <returns></returns>
        Task SaveRessourceAsync<TRessource>(TRessource ressource) where TRessource : IRessource;

        /// <summary>
        /// Gets the group ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <returns></returns>
        IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId) where TRessource : IRessource;

        /// <summary>
        /// Gets the group ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupNamespaceRessource">The group namespace ressource.</param>
        /// <returns></returns>
        IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource) where TRessource : IRessource;

        /// <summary>
        /// Gets the group ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <returns></returns>
        Task<IRessourcesGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Identity groupRessourceId) where TRessource : IRessource;

        /// <summary>
        /// Gets the group ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupNamespaceRessource">The group namespace ressource.</param>
        /// <returns></returns>
        Task<IRessourcesGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Namespace groupNamespaceRessource) where TRessource : IRessource;

        /// <summary>
        /// Attaches the ressource to group.
        /// </summary>
        /// <param name="ressourceId">The ressource identifier.</param>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        void AttachRessourceToGroup(Identity ressourceId, Identity groupRessourceId);

        /// <summary>
        /// Attaches the ressource to group asynchronous.
        /// </summary>
        /// <param name="ressourceId">The ressource identifier.</param>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <returns></returns>
        Task AttachRessourceToGroupAsync(Identity ressourceId, Identity groupRessourceId);

        /// <summary>
        /// Removes the group.
        /// </summary>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        void RemoveGroup(Identity groupRessourceId);

        /// <summary>
        /// Removes the group asynchronous.
        /// </summary>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <returns></returns>
        Task RemoveGroupAsync(Identity groupRessourceId);

        /// <summary>
        /// Gets the group list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Identity> GetAllGroups();

        /// <summary>
        /// Gets the group list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Identity>> GetAllGroupsAsync();
    }
}
