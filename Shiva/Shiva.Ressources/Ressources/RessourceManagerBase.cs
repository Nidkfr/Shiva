using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shiva.Core.Identities;
using System.Linq;

namespace Shiva.Ressources
{
    /// <summary>
    /// Implementation of ressource manager base
    /// </summary>
    /// <seealso cref="Shiva.Ressources.IRessourceManager" />
    public abstract class RessourceManagerBase : IRessourceManager
    {
        private readonly IDictionary<Identity, IDictionary<Type,IRessource>> _removedRessources = new Dictionary<Identity, IDictionary<Type, IRessource>>();
        private readonly IDictionary<Identity, IDictionary<Type, IRessource>> _addedRessources = new Dictionary<Identity, IDictionary<Type, IRessource>>();
        private readonly IDictionary<Identity, IDictionary<Type, IRessource>> _editedRessources = new Dictionary<Identity, IDictionary<Type, IRessource>>();
        private readonly IDictionary<Identity, IList<Identity>> _editedGroup = new Dictionary<Identity, IList<Identity>>();
        private readonly IList<Identity> _removeGroup = new List<Identity>();

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
        public void AttachRessourceToGroup(Identity ressourceId, Identity groupRessourceId)
        {
            if (ressourceId == null)
                throw new ArgumentNullException(nameof(ressourceId));

            if (groupRessourceId == null)
                throw new ArgumentNullException(nameof(groupRessourceId));

            if(!this._editedGroup.ContainsKey(groupRessourceId))
                this._editedGroup.Add(groupRessourceId, new List<Identity>());

            if (!this._editedGroup[groupRessourceId].Any(x => x == ressourceId))
            {
                this._editedGroup[groupRessourceId].Add(ressourceId);
                this._removeGroup.Remove(groupRessourceId);
            }
        }

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
        public  bool ContainsRessource<TRessource>(Identity idRessource)
        {
            if (idRessource == null)
                throw new ArgumentNullException(nameof(idRessource));

            if(this._addedRessources.ContainsKey(idRessource))
            {
                var elements = this._addedRessources[idRessource];
                if (elements.ContainsKey(typeof(TRessource))) return true;
            }

            return this.ContainsRessourceInternal<TRessource>(idRessource);
        }

        /// <summary>
        /// Determines whether [contains ressource internal] [the specified ressource identifier].
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns>
        ///   <c>true</c> if [contains ressource internal] [the specified ressource identifier]; otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool ContainsRessourceInternal<TRessource>(Identity ressourceID);

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
        public  IEnumerable<Identity> GetAllGroups()
        {
            var groups = new List<Identity>();
            groups.AddRange(this._editedGroup.Keys);
            groups.AddRange(this.GetAllGroupsInternal());
            return groups;
        }

        /// <summary>
        /// Gets all groups internal.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<Identity> GetAllGroupsInternal();

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
            return await Task.Run(() => this.GetGroupRessources<TRessource>(groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Gets the group ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="groupNamespaceRessource">The group namespace ressource.</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns></returns>
        public async Task<IRessourcesGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Namespace groupNamespaceRessource, CancellationToken? cancelToken = null) where TRessource : IRessource
        {
            return await Task.Run(() => this.GetGroupRessources<TRessource>(groupNamespaceRessource), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Gets the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        public  TRessource GetRessource<TRessource>(Identity ressourceID) where TRessource : IRessource
        {
            if (ressourceID == null)
                throw new ArgumentNullException(nameof(ressourceID));

            //search first in added ressource
            if(this._addedRessources.ContainsKey(ressourceID))
            {
                var ressources = this._addedRessources[ressourceID];
                if (ressources.ContainsKey(typeof(TRessource)))
                    return (TRessource)ressources[typeof(TRessource)];
            }
            
            return this.GetRessourceInternal<TRessource>(ressourceID);
        }

        /// <summary>
        /// Gets the ressource internal.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressourceID">The ressource identifier.</param>
        /// <returns></returns>
        protected abstract TRessource GetRessourceInternal<TRessource>(Identity ressourceID) where TRessource : IRessource;


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
        /// Removes the group. if new attached a make, its removed
        /// </summary>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        public void RemoveGroup(Identity groupRessourceId)
        {
            this._removeGroup.Add(groupRessourceId);
            this._editedGroup.Remove(groupRessourceId);
        }

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
        /// Flushes this instance.
        /// </summary>
        /// <param name="stream"></param>
        /// <exception cref="NotImplementedException"></exception>
        public abstract void Save(Stream stream);
        

        /// <summary>
        /// Flushes the asyn.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SaveAsyn(Stream stream)
        {
            await Task.Run(() => this.Save(stream));
        }

        /// <summary>
        /// Saves the ressource.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressource">The ressource.</param>
        public void SetRessource<TRessource>(TRessource ressource) where TRessource : IRessource
        {
            if (ressource == null)
                throw new ArgumentNullException(nameof(ressource));

            //add ressource
            if (!this._addedRessources.ContainsKey(ressource.Id))
                this._addedRessources.Add(ressource.Id, new Dictionary<Type,IRessource>());

            var elts = this._addedRessources[ressource.Id];
            var typeRessouce = ressource.GetType();
            if (!elts.ContainsKey(typeRessouce))
                elts.Add(typeRessouce, ressource);
            else
                elts[typeRessouce] = ressource;

            //remove from removed ressource
            if (this._removedRessources.ContainsKey(ressource.Id))
                if (this._removedRessources[ressource.Id].ContainsKey(typeRessouce))
                    this._removedRessources[ressource.Id].Remove(typeRessouce);
                

        }

        /// <summary>
        /// Saves the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressource">The ressource.</param>
        /// <param name="cancelToken">cancel token</param>
        /// <returns></returns>
        public async Task SetRessourceAsync<TRessource>(TRessource ressource, CancellationToken? cancelToken = null) where TRessource : IRessource
        {
            await Task.Run(() => this.SetRessource<TRessource>(ressource), cancelToken ?? CancellationToken.None);
        }

        
    }
}
