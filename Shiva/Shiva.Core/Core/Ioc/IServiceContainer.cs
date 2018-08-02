using System;

namespace Shiva.Core.Ioc
{
    /// <summary>
    /// Abstraction for Ioc containers with basic functionallity
    /// </summary>
    public interface IServiceContainer
    {
        #region Public Methods

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// Type of instance of service
        /// </typeparam>
        /// <param name="scope">
        /// Scope for service
        /// </param>
        void Register<TService, TImplementation>(ScopeServiceEnum scope = ScopeServiceEnum.TRANSIENT) where TImplementation : class, TService where TService : class;

        /// <summary>
        /// Registers the specified service factory.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <param name="serviceFactory">
        /// The service factory.
        /// </param>
        /// <param name="scope">
        /// Scope for service
        /// </param>
        void Register<TService>(Func<TService> serviceFactory, ScopeServiceEnum scope = ScopeServiceEnum.TRANSIENT) where TService : class;

        /// <summary>
        /// Registers the initialize action.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <param name="initializer">
        /// The initializer.
        /// </param>
        void RegisterInitialize<TService>(Action<TService> initializer) where TService : class;

        /// <summary>
        /// Registers the specified service in container.
        /// </summary>
        /// <typeparam name="TService">
        /// Service contract
        /// </typeparam>
        /// <param name="service">
        /// The service implementation.
        /// </param>
        void RegisterSingleton<TService>(TService service) where TService : class;

        /// <summary>
        /// Resolves the type.
        /// </summary>
        /// <typeparam name="TService">
        /// Service Contract
        /// </typeparam>
        /// <returns>
        /// Service implementation
        /// </returns>
        TService ResolveType<TService>() where TService : class;

        #endregion Public Methods
    }
}