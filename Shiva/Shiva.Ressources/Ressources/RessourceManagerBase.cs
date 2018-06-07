using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    /// <summary>
    /// Implementation of ressource manager base
    /// </summary>
    /// <seealso cref="Shiva.Ressources.IRessourceManager" />
    public abstract class RessourceManagerBase : IRessourceManager
    {
        /// <summary>
        /// Gets the culture of ressources.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public abstract CultureInfo Culture { get; }

        /// <summary>
        /// Attaches the ressource to group.
        /// </summary>
        /// <param name="ressourceId">The ressource identifier.</param>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        public abstract void AttachRessourceToGroup(Identity ressourceId, Identity groupRessourceId);

        /// <summary>
        /// Attaches the ressource to group asynchronous.
        /// </summary>
        /// <param name="ressourceId">The ressource identifier.</param>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task AttachRessourceToGroupAsync(Identity ressourceId, Identity groupRessourceId, CancellationToken? cancelToken = null)
        {
            await Task.Run(() => this.AttachRessourceToGroup(ressourceId, groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Determines whether the specified identifier ressource contains ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="idRessource">The identifier ressource.</param>
        /// <returns>
        ///   <c>true</c> if the specified identifier ressource contains ressource; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool ContainsRessource<TRessource>(Identity idRessource);

        /// <summary>
        /// Determines whether [contains ressource asynchronous] [the specified identifier ressource].
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="idRessource">The identifier ressource.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns></returns>
        public async Task<bool> ContainsRessourceAsync<TRessource>(Identity idRessource, CancellationToken? cancelToken = null)
        {
            return await Task.Run(() => this.ContainsRessource<TRessource>(idRessource));
        }

        /// <summary>
        /// Gets the group list.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Identity> GetAllGroups();

        /// <summary>
        /// Gets the group list asynchronous.
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Identity>> GetAllGroupsAsync(CancellationToken? cancelToken = null)
        {
            return await Task.Run(() => this.GetAllGroups(), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Gets the group ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <returns></returns>
        public abstract IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId) where TRessource : IRessource;

        /// <summary>
        /// Gets the group ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupNamespaceRessource">The group namespace ressource.</param>
        /// <returns></returns>
        public abstract IRessourcesGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource) where TRessource : IRessource;

        /// <summary>
        /// Gets the group ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns></returns>
        public async Task<IRessourcesGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Identity groupRessourceId, CancellationToken? cancelToken = null) where TRessource : IRessource
        {
            return await Task.Run(()=>this.GetGroupRessources<TRessource>(groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Gets the group ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupNamespaceRessource">The group namespace ressource.</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns></returns>
        public async  Task<IRessourcesGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Namespace groupNamespaceRessource, CancellationToken? cancelToken = null) where TRessource : IRessource
        {
            return await Task.Run(() => this.GetGroupRessources<TRessource>(groupNamespaceRessource), cancelToken ?? CancellationToken.None);
        }

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
        /// <param name="ressourceId"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task<TRessource> GetRessourceAsync<TRessource>(Identity ressourceId, CancellationToken? cancelToken = null) where TRessource : IRessource
        {
            return await Task.Run(() => this.GetRessource<TRessource>(ressourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Removes the group.
        /// </summary>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        public abstract void RemoveGroup(Identity groupRessourceId);

        /// <summary>
        /// Removes the group asynchronous.
        /// </summary>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns></returns>
        public async Task RemoveGroupAsync(Identity groupRessourceId, CancellationToken? cancelToken = null)
        {
            await Task.Run(() => this.RemoveGroup(groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Removes the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="idRessource">The identifier ressource.</param>
        public abstract void RemoveRessource<TRessource>(Identity idRessource);

        /// <summary>
        /// Removes the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="idRessource">The identifier ressource.</param>
        /// <returns></returns>
        public async Task RemoveRessourceAsync<TRessource>(Identity idRessource)
        {
            await Task.Run(() => this.RemoveRessource<TRessource>(idRessource));
        }

        /// <summary>
        /// Saves the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressource">The ressource.</param>
        public abstract void SaveRessource<TRessource>(TRessource ressource) where TRessource : IRessource;

        /// <summary>
        /// Saves the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressource">The ressource.</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns></returns>
        public async Task SaveRessourceAsync<TRessource>(TRessource ressource, CancellationToken? cancelToken = null) where TRessource : IRessource
        {
            await Task.Run(() => this.SaveRessource<TRessource>(ressource), cancelToken ?? CancellationToken.None);
        }
    }
}
