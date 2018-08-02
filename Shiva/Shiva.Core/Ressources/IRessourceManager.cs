using Shiva.Core.Identities;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Shiva.Ressources
{
    /// <summary>
    /// Ressources Management
    /// </summary>
    public interface IRessourceManager
    {
        #region Public Properties

        /// <summary>
        /// Gets the culture of ressources.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        CultureInfo Culture { get; }

        #endregion Public Properties

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
        void AttachRessourceToGroup<TRessource>(TRessource ressource, Identity groupRessourceId) where TRessource : class, IRessource;

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
        Task AttachRessourceToGroupAsync<TRessource>(TRessource ressource, Identity groupRessourceId, CancellationToken? cancelToken = null) where TRessource : class, IRessource;

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
        bool ContainsRessource<TRessource>(Identity idRessource);

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
        Task<bool> ContainsRessourceAsync<TRessource>(Identity idRessource, CancellationToken? cancelToken = null);

        /// <summary>
        /// Detaches the ressource to group.
        /// </summary>
        /// <param name="ressource">
        /// The ressource identifier.
        /// </param>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        void DetachRessourceToGroup<TRessource>(TRessource ressource, Identity groupRessourceId) where TRessource : class, IRessource;

        /// <summary>
        /// Detaches the ressource to group asynchronous.
        /// </summary>
        /// <param name="ressource">
        /// The ressource identifier.
        /// </param>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        /// <param name="cancelToken">
        /// Cancel token
        /// </param>
        Task DetachRessourceToGroupAsync<TRessource>(TRessource ressource, Identity groupRessourceId, CancellationToken? cancelToken = null) where TRessource : class, IRessource;

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        void Flush();

        /// <summary>
        /// Flushes the asyn.
        /// </summary>
        /// <returns>
        /// </returns>
        Task FlushAsyn();

        /// <summary>
        /// Gets the group list.
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerable<IGroupInformation> GetAllGroups();

        /// <summary>
        /// Gets the group list asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<IEnumerable<IGroupInformation>> GetAllGroupsAsync(CancellationToken? cancelToken = null);

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
        IRessourceGroup<TRessource> GetGroupRessources<TRessource>(Identity groupRessourceId) where TRessource : class, IRessource, new();

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
        /// Get child namespace
        /// </param>
        /// <returns>
        /// </returns>
        IRessourceGroup<TRessource> GetGroupRessources<TRessource>(Namespace groupNamespaceRessource, bool getChildNamespace) where TRessource : class, IRessource, new();

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
        Task<IRessourceGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Identity groupRessourceId, CancellationToken? cancelToken = null) where TRessource : class, IRessource, new();

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
        Task<IRessourceGroup<TRessource>> GetGroupRessourcesAsync<TRessource>(Namespace groupNamespaceRessource, bool getChildNamespace, CancellationToken? cancelToken = null) where TRessource : class, IRessource, new();

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
        TRessource GetRessource<TRessource>(Identity ressourceID) where TRessource : IRessource, new();

        /// <summary>
        /// Gets the ressource asynchronous.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <returns>
        /// </returns>
        Task<TRessource> GetRessourceAsync<TRessource>(Identity ressourceId, CancellationToken? cancelToken = null) where TRessource : IRessource, new();

        /// <summary>
        /// Removes the group.
        /// </summary>
        /// <param name="groupRessourceId">
        /// The group ressource identifier.
        /// </param>
        void RemoveGroup(Identity groupRessourceId);

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
        Task RemoveGroupAsync(Identity groupRessourceId, CancellationToken? cancelToken = null);

        /// <summary>
        /// Removes the ressource.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="idRessource">
        /// The identifier ressource.
        /// </param>
        void RemoveRessource<TRessource>(Identity idRessource);

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
        Task RemoveRessourceAsync<TRessource>(Identity idRessource);

        /// <summary>
        /// Saves the ressource.
        /// </summary>
        /// <typeparam name="TRessource">
        /// The type of the ressource.
        /// </typeparam>
        /// <param name="ressource">
        /// The ressource.
        /// </param>
        void SetRessource<TRessource>(TRessource ressource) where TRessource : IRessource;

        /// <summary>
        /// Saves the ressource asynchronous.
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
        Task SetRessourceAsync<TRessource>(TRessource ressource, CancellationToken? cancelToken = null) where TRessource : IRessource;

        #endregion Public Methods
    }
}