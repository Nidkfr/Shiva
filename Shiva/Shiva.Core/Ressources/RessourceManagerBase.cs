using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shiva.Core.Identities;
using System.Linq;
using Shiva.Core.Services;

namespace Shiva.Ressources
{
    /// <summary>
    /// Implementation of ressource manager base
    /// </summary>
    /// <seealso cref="Shiva.Ressources.IRessourceManager" />
    public abstract class RessourceManagerBase : IRessourceManager
    {
        private readonly IDictionary<Identity, IList<Type>> _removedRessources = new Dictionary<Identity, IList<Type>>();
        private readonly IDictionary<Identity, IDictionary<Type, IRessource>> _addedRessources = new Dictionary<Identity, IDictionary<Type, IRessource>>();
        private readonly IDictionary<Identity, IList<Identity>> _editedGroup = new Dictionary<Identity, IList<Identity>>();
        private readonly IList<Identity> _removeGroup = new List<Identity>();

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceManagerBase"/> class.
        /// </summary>
        /// <param name="logmanager">The logmanager.</param>
        protected RessourceManagerBase(ILogManager logmanager = null)
        {
            this.Logger = logmanager?.CreateLogger(this.GetType()) ?? new NoLogger();
        }

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

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Attach Ressource {ressourceId} to group {groupRessourceId} in culutre {this.Culture}");

            if (!this._editedGroup.ContainsKey(groupRessourceId))
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
        public bool ContainsRessource<TRessource>(Identity idRessource)
        {
            if (idRessource == null)
                throw new ArgumentNullException(nameof(idRessource));

            if (this._addedRessources.ContainsKey(idRessource))
            {
                var elements = this._addedRessources[idRessource];
                if (elements.ContainsKey(typeof(TRessource)))
                {
                    if (this.Logger.InfoIsEnabled)
                        this.Logger.Info($"Ressource Manager contains ressource {idRessource} in culture {this.Culture}");
                    return true;
                }
            }

            var internalResponse = this.ContainsRessourceInternal<TRessource>(idRessource);

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Ressource Manager {(internalResponse ? "Contains" : "not contains")} ressource {idRessource} in culture {this.Culture}");

            return internalResponse;
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
        public IEnumerable<Identity> GetAllGroups()
        {
            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Ressource Maanger get All groups from culture {this.Culture}");

            var groups = new List<Identity>();
            groups.AddRange(this._editedGroup.Keys);
            groups.AddRange(this.GetAllGroupsInternal());

            if (this.Logger.DebugIsEnabled)
            {
                foreach (var group in groups)
                {
                    this.Logger.Debug($"Group : {group}");
                }
            }

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
        public TRessource GetRessource<TRessource>(Identity ressourceID) where TRessource : IRessource
        {
            if (ressourceID == null)
                throw new ArgumentNullException(nameof(ressourceID));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Get Ressource {ressourceID}");

            //search first in added ressource
            if (this._addedRessources.ContainsKey(ressourceID))
            {
                var ressources = this._addedRessources[ressourceID];
                if (ressources.ContainsKey(typeof(TRessource)))
                {
                    if (this.Logger.DebugIsEnabled)
                        this.Logger.Debug($"Ressource {ressourceID} is in cache");
                    return (TRessource)ressources[typeof(TRessource)];
                }
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
            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Remove group {groupRessourceId} in culture {this.Culture}");
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
        public void RemoveRessource<TRessource>(Identity idRessource)
        {
            if (idRessource == null)
                throw new ArgumentNullException(nameof(idRessource));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Remove ressource {idRessource} in culture {this.Culture}");

            //add ressource
            if (!this._removedRessources.ContainsKey(idRessource))
                this._removedRessources.Add(idRessource, new List<Type>());

            var elts = this._removedRessources[idRessource];
            var typeRessouce = typeof(TRessource);
            if (!elts.Any(x => typeRessouce == x))
                elts.Add(typeRessouce);


            //remove from added ressource et edit ressource
            if (this._addedRessources.ContainsKey(idRessource))
                if (this._addedRessources[idRessource].ContainsKey(typeRessouce))
                    this._addedRessources[idRessource].Remove(typeRessouce);


        }

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
        /// <exception cref="NotImplementedException"></exception>
        public void Flush()
        {
            var info = new RessourcesEditInfo()
            {
                RemovedRessources = this._removedRessources
                .SelectMany(x => x.Value.Select(type => new { type, x.Key }))
                .ToDictionary(x => x.type, x => x.Key),
                AddedRessources = this._addedRessources.SelectMany(x=>x.Value.Values).ToList()
            };



            this.FlushInternal(info);

            this._addedRessources.Clear();
            this._editedGroup.Clear();
            this._removedRessources.Clear();
            this._removeGroup.Clear();
        }

        /// <summary>
        /// Flushes the internal.
        /// </summary>
        protected abstract void FlushInternal(RessourcesEditInfo editInformation);

        /// <summary>
        /// Flushes the asyn.
        /// </summary>        
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task FlushAsyn()
        {
            await Task.Run(() => this.Flush());
        }

        /// <summary>
        /// Saves the ressource in culture.
        /// </summary>
        /// <typeparam name="TRessource">The type of the ressource.</typeparam>
        /// <param name="ressource">The ressource.</param>
        public void SetRessource<TRessource>(TRessource ressource) where TRessource : IRessource
        {
            if (ressource == null)
                throw new ArgumentNullException(nameof(ressource));

            ressource = (TRessource)ressource.Clone();
            ressource.SetCulture(this.Culture);

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Add ressource {ressource} in culture {this.Culture}");

            //add ressource
            if (!this._addedRessources.ContainsKey(ressource.Id))
                this._addedRessources.Add(ressource.Id, new Dictionary<Type, IRessource>());

            var elts = this._addedRessources[ressource.Id];
            var typeRessouce = ressource.GetType();
            if (!elts.ContainsKey(typeRessouce))
                elts.Add(typeRessouce, ressource);
            else
                elts[typeRessouce] = ressource;

            //remove from removed ressource
            if (this._removedRessources.ContainsKey(ressource.Id))
                if (this._removedRessources[ressource.Id].Contains(typeRessouce))
                    this._removedRessources[ressource.Id].Remove(typeRessouce);


        }

        /// <summary>
        /// Saves the ressource in the culture asynchronous.
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
