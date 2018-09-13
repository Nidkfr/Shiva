using Shiva.Core.Identities;
using Shiva.Core.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shiva.Ressources
{
    /// <summary>
    /// Implementation of ressource manager base
    /// </summary>
    /// <seealso cref="Shiva.Ressources.IRessourceService" />
    ///
    public abstract class RessourceServicerBase : IRessourceService
    {
        #region Private Fields

        private readonly IDictionary<IGroupInformation, IdentifiableList<IdentityContainer>> _groupesRessources = new Dictionary<IGroupInformation, IdentifiableList<IdentityContainer>>();
        private readonly IList<Identity> _removedgroupes = new List<Identity>();
        private readonly IDictionary<Type, IdentifiableList<IRessource>> _ressources = new Dictionary<Type, IdentifiableList<IRessource>>();

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RessourceServicerBase" /> class.
        /// </summary>
        /// <param name="logmanager">
        /// The logmanager.
        /// </param>
        protected RessourceServicerBase(ILogManager logmanager = null)
        {
            this.Logger = logmanager?.CreateLogger(this.GetType()) ?? new NoLogger();
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets the culture of ressources.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public abstract CultureInfo Culture { get; }

        #endregion Public Properties

        #region Private Properties

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private ILogger Logger { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Attaches the ressource to group.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressource">
        /// The ressource.
        /// </param>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// ressourceId or groupRessourceId
        /// </exception>
        public void AttachRessourceToGroup<TRessource>(TRessource ressource, Identity groupRessourceId) where TRessource : class, IRessource
        {
            if (ressource == null)
                throw new ArgumentNullException(nameof(ressource));

            if (groupRessourceId == null)
                throw new ArgumentNullException(nameof(groupRessourceId));

            if (ressource.IsEmptyRessource)
                throw new InvalidOperationException("You not be use a empty Ressource");

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Attach Ressource {ressource.Id} to group {groupRessourceId} in culture {this.Culture}");

            var grpInfo = new RessourceGroupInformation(groupRessourceId, typeof(TRessource));
            if (this.ContainsRessource<TRessource>(ressource.Id))
            {
                if (!this._groupesRessources.ContainsKey(grpInfo))
                    this._groupesRessources.Add(grpInfo, new IdentifiableList<IdentityContainer>());

                this._groupesRessources[grpInfo].Add(new IdentityContainer(ressource.Id));
            }
            else
                throw new InvalidOperationException($"The Ressource {ressource} is not valid for this RessourceManager.");
        }

        /// <summary>
        /// Attaches the ressource to group asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressource">
        /// The ressource.
        /// </param>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        /// <param name="cancelToken">
        /// The cancel token.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task AttachRessourceToGroupAsync<TRessource>(TRessource ressource, Identity groupRessourceId, CancellationToken? cancelToken = null) where TRessource : class, IRessource
        {
            await Task.Run(() => this.AttachRessourceToGroup(ressource, groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Determines whether the specified identifier ressource contains ressource.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="idRessource">
        /// The identifier ressource.
        /// </param>
        /// <returns>
        /// <c> true </c> if the specified identifier ressource contains ressource; otherwise, <c>
        /// false </c>.
        /// </returns>
        public bool ContainsRessource<TRessource>(Identity idRessource)
        {
            if (idRessource == null)
                throw new ArgumentNullException(nameof(idRessource));

            if (this._ressources.ContainsKey(typeof(TRessource)))
            {
                var elements = this._ressources[typeof(TRessource)];
                if (elements.Contains(idRessource))
                {
                    if (this.Logger.InfoIsEnabled)
                        this.Logger.Info($"Ressource Manager contains ressource {idRessource} in culture {this.Culture}");
                    return true;
                }
                else
                {
                    if (elements.RemovedElement.Contains(idRessource))
                        return false;
                }
            }

            var internalResponse = this.ContainsRessourceInternal<TRessource>(idRessource);

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Ressource Manager {(internalResponse ? "Contains" : "not contains")} ressource {idRessource} in culture {this.Culture}");

            return internalResponse;
        }

        /// <summary>
        /// Determines whether [contains ressource asynchronous] [the specified identifier ressource].
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="idRessource">
        /// The identifier ressource.
        /// </param>
        /// <param name="cancelToken">
        /// The cancel token.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<bool> ContainsRessourceAsync<TRessource>(Identity idRessource, CancellationToken? cancelToken = null)
        {
            return await Task.Run(() => this.ContainsRessource<TRessource>(idRessource));
        }

        /// <summary>
        /// Detaches the ressource to group.
        /// </summary>
        ///<param name="ressource">ressource</param>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        public void DetachRessourceToGroup<TRessource>(TRessource ressource, Identity groupRessourceId) where TRessource : class, IRessource
        {
            if (ressource == null)
                throw new ArgumentNullException(nameof(ressource));

            if (ressource.IsEmptyRessource)
                throw new InvalidOperationException("You not be use a empty Ressource");

            var grp = new RessourceGroupInformation(groupRessourceId, typeof(TRessource));
            if (!this._groupesRessources.ContainsKey(grp))
                this._groupesRessources.Add(grp, new IdentifiableList<IdentityContainer>());

            this._groupesRessources[grp].Remove(ressource.Id);
        }

        /// <summary>
        /// Detaches the ressource to group asynchronous.
        /// </summary>
        ///<param name="ressource">ressource</param>
        /// <param name="groupRessourceId">The group ressource identifier.</param>
        /// <param name="cancelToken">Cancel token</param>
        /// <returns></returns>
        public async Task DetachRessourceToGroupAsync<TRessource>(TRessource ressource, Identity groupRessourceId, CancellationToken? cancelToken = null) where TRessource : class, IRessource
        {
            await Task.Run(() => this.DetachRessourceToGroup<TRessource>(ressource, groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        public void Flush()
        {
            var info = new RessourcesEditInfo()
            {
                RemovedRessources = this._ressources.ToDictionary(x => x.Key, x => x.Value.RemovedElement),
                AddedRessources = this._ressources.SelectMany(x => x.Value).ToList(),
                AddedGroups = this._groupesRessources.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Id)),
                RemovedGroups = this._removedgroupes,
                DetachedRessourceGroups = this._groupesRessources.ToDictionary(x => x.Key, x => x.Value.RemovedElement)
            };

            this.FlushInternal(info);

            foreach (var item in this._ressources)
            {
                item.Value.Clear();
            }
            this._removedgroupes.Clear();
            this._groupesRessources.Clear();
        }

        /// <summary>
        /// Flushes the asyn.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public async Task FlushAsyn()
        {
            await Task.Run(() => this.Flush());
        }

        /// <summary>
        /// Gets the group list.
        /// </summary>
        /// <returns>
        /// </returns>
        public IEnumerable<IGroupInformation> GetAllGroups()
        {
            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Ressource Manager get All groups from culture {this.Culture}");

            var result = new List<IGroupInformation>();
            result.AddRange(this.GetAllGroupsInternal());
            result.AddRange(this._groupesRessources.Keys);

            return result.Distinct();
        }

        /// <summary>
        /// Gets the group list asynchronous.
        /// </summary>
        /// <param name="cancelToken">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IEnumerable<IGroupInformation>> GetAllGroupsAsync(CancellationToken? cancelToken = null)
        {
            return await Task.Run(() => this.GetAllGroups(), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Gets the group ressource.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        /// <returns>
        /// </returns>
        public IRessourceGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId) where TRessource : class, IRessource, new()
        {
            var grpInfo = new RessourceGroupInformation(groupRessourceId, typeof(TRessource));
            var lst = new IdentifiableList<TRessource>();
            lst.AddRange(this.GetRessourceFromGroupInternal<TRessource>(groupRessourceId));

            if (this._groupesRessources.ContainsKey(grpInfo))
            {
                if (this._ressources.ContainsKey(typeof(TRessource)))
                {
                    foreach (var item in this._groupesRessources[grpInfo])
                    {
                        if (this._ressources[typeof(TRessource)].Contains(item.Id))
                            lst.Add((TRessource)this._ressources[typeof(TRessource)][item.Id]);
                    }
                }
                foreach (var id in this._groupesRessources[grpInfo].RemovedElement)
                {
                    lst.Remove(id);
                }
            }

            if (lst.Count > 0)
                return new RessourceGroup<TRessource>(groupRessourceId, this.Culture, lst);
            return null;
        }

        /// <summary>
        /// Gets the group ressource.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="groupNamespaceRessource">
        /// The group namespace ressource.
        /// </param>
        /// <param name="getChildNamespace">
        /// check child namespace
        /// </param>
        /// <returns>
        /// </returns>
        public abstract IRessourceGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource, bool getChildNamespace) where TRessource : class, IRessource, new();

        /// <summary>
        /// Gets the group ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        /// <param name="cancelToken">
        /// cancel token
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IRessourceGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Identity groupRessourceId, CancellationToken? cancelToken = null) where TRessource : class, IRessource, new()
        {
            return await Task.Run(() => this.GetGroupRessources<TRessource>(groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Gets the group ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="groupNamespaceRessource">
        /// The group namespace ressource.
        /// </param>
        /// <param name="cancelToken">
        /// cancel token
        /// </param>
        /// <param name="getChildNamespace">
        /// get child namespace
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<IRessourceGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Namespace groupNamespaceRessource, bool getChildNamespace, CancellationToken? cancelToken = null) where TRessource : class, IRessource, new()
        {
            return await Task.Run(() => this.GetGroupRessources<TRessource>(groupNamespaceRessource, getChildNamespace), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Gets the ressource.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressourceID">
        /// The ressource identifier.
        /// </param>
        /// <returns>
        /// </returns>
        public TRessource GetRessource<TRessource>(Identity ressourceID) where TRessource : IRessource, new()
        {
            if (ressourceID == null)
                throw new ArgumentNullException(nameof(ressourceID));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Get Ressource {ressourceID}");

            //search first in added ressource
            if (this._ressources.ContainsKey(typeof(TRessource)))
            {
                var ressources = this._ressources[typeof(TRessource)];
                if (ressources.Contains(ressourceID))
                {
                    if (this.Logger.DebugIsEnabled)
                        this.Logger.Debug($"Ressource {ressourceID} is in cache");
                    return (TRessource)ressources[ressourceID];
                }
                else
                {
                    if (this._ressources[typeof(TRessource)].RemovedElement.Contains(ressourceID))
                        return default;
                }
            }

            return this.GetRessourceInternal<TRessource>(ressourceID);
        }

        /// <summary>
        /// Gets the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressourceId">
        /// </param>
        /// <param name="cancelToken">
        /// </param>
        /// <returns>
        /// </returns>
        public async Task<TRessource> GetRessourceAsync<TRessource>(Identity ressourceId, CancellationToken? cancelToken = null) where TRessource : IRessource, new()
        {
            return await Task.Run(() => this.GetRessource<TRessource>(ressourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Removes the group. if new attached a make, its removed
        /// </summary>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        public void RemoveGroup(Identity groupRessourceId)
        {
            if (groupRessourceId == null)
                throw new ArgumentNullException(nameof(groupRessourceId));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Remove group {groupRessourceId} in culture {this.Culture}");

            this._removedgroupes.Remove(groupRessourceId);
            foreach (var grp in this._groupesRessources.Keys.Where(x => x.Id == groupRessourceId))
            {
                this._groupesRessources.Remove(grp);
            }
        }

        /// <summary>
        /// Removes the group asynchronous.
        /// </summary>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        /// <param name="cancelToken">
        /// cancel token
        /// </param>
        /// <returns>
        /// </returns>
        public async Task RemoveGroupAsync(Identity groupRessourceId, CancellationToken? cancelToken = null)
        {
            await Task.Run(() => this.RemoveGroup(groupRessourceId), cancelToken ?? CancellationToken.None);
        }

        /// <summary>
        /// Removes the ressource.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="idRessource">
        /// The identifier ressource.
        /// </param>
        public void RemoveRessource<TRessource>(Identity idRessource)
        {
            if (idRessource == null)
                throw new ArgumentNullException(nameof(idRessource));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Remove ressource {idRessource} in culture {this.Culture}");

            if (this._ressources.ContainsKey(typeof(TRessource)))
                this._ressources[typeof(TRessource)].Remove(idRessource);
        }

        /// <summary>
        /// Removes the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="idRessource">
        /// The identifier ressource.
        /// </param>
        /// <returns>
        /// </returns>
        public async Task RemoveRessourceAsync<TRessource>(Identity idRessource)
        {
            await Task.Run(() => this.RemoveRessource<TRessource>(idRessource));
        }

        /// <summary>
        /// Saves the ressource in culture.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressource">
        /// The ressource.
        /// </param>
        public void SetRessource<TRessource>(TRessource ressource) where TRessource : IRessource
        {
            if (ressource == null)
                throw new ArgumentNullException(nameof(ressource));

            if (ressource.IsEmptyRessource)
                throw new InvalidOperationException("You not be use a empty Ressource");

            ressource = (TRessource)ressource.Clone();
            ressource.SetCulture(this.Culture);

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Add ressource {ressource} in culture {this.Culture}");

            //add ressource
            if (!this._ressources.ContainsKey(typeof(TRessource)))
                this._ressources.Add(typeof(TRessource), new IdentifiableList<IRessource>());
            this._ressources[typeof(TRessource)].Add(ressource);
        }

        /// <summary>
        /// Saves the ressource in the culture asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressource">
        /// The ressource.
        /// </param>
        /// <param name="cancelToken">
        /// cancel token
        /// </param>
        /// <returns>
        /// </returns>
        public async Task SetRessourceAsync<TRessource>(TRessource ressource, CancellationToken? cancelToken = null) where TRessource : IRessource
        {
            await Task.Run(() => this.SetRessource<TRessource>(ressource), cancelToken ?? CancellationToken.None);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Determines whether [contains ressource internal] [the specified ressource identifier].
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressourceID">
        /// The ressource identifier.
        /// </param>
        /// <returns>
        /// <c> true </c> if [contains ressource internal] [the specified ressource identifier];
        /// otherwise, <c> false </c>.
        /// </returns>
        protected abstract bool ContainsRessourceInternal<TRessource>(Identity ressourceID);

        /// <summary>
        /// Flushes the internal.
        /// </summary>
        protected abstract void FlushInternal(RessourcesEditInfo editInformation);

        /// <summary>
        /// Gets all groups internal.
        /// </summary>
        /// <returns>
        /// </returns>
        protected abstract IEnumerable<IGroupInformation> GetAllGroupsInternal();

        /// <summary>
        /// Gets the ressource from group.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        /// <returns>
        /// </returns>
        protected abstract IEnumerable<TRessource> GetRessourceFromGroupInternal<TRessource>(Identity groupRessourceId) where TRessource : class, IRessource, new();

        /// <summary>
        /// Gets the ressource internal.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressourceID">
        /// The ressource identifier.
        /// </param>
        /// <returns>
        /// </returns>
        protected abstract TRessource GetRessourceInternal<TRessource>(Identity ressourceID) where TRessource : IRessource, new();

        #endregion Protected Methods
    }
}