using System;
using System.Collections.Generic;
using System.Text;

namespace Shiva.Core.Ioc
{
    /// <summary>
    /// Abstraction for Ioc containers with basic functionallity
    /// </summary>
    public interface IServiceContainer
    {
        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">Type of instance of service</typeparam>
        /// <param name="scope">Scope for service</param>
        void Register<TService,TImplementation>(ScopeServiceEnum scope = ScopeServiceEnum.Transient);

        /// <summary>
        /// Registers the specified service in container.
        /// </summary>
        /// <typeparam name="TService">Service contract</typeparam>
        /// <param name="service">The service implementation.</param>
        void RegisterSingleton<TService>(TService service);

        /// <summary>
        /// Registers the specified service factory.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceFactory">The service factory.</param>
        /// <param name="scope">Scope for service</param>
        void Register<TService>(Func<TService> serviceFactory, ScopeServiceEnum scope = ScopeServiceEnum.Transient);

        /// <summary>
        /// Resolves the type.
        /// </summary>
        /// <typeparam name="TService">Service Contract</typeparam>
        /// <returns>Service implementation</returns>
        TService ResolveType<TService>();
    }
}
