using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Services;

namespace Shiva.Core.Ioc
{
    /// <summary>
    /// Service container base
    /// </summary>
    /// <seealso cref="Shiva.Core.Ioc.IServiceContainer" />
    public abstract class ServiceContainerBase : IServiceContainer
    {
        private ILogger _logger;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger => this._logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContainerBase"/> class.
        /// </summary>
        /// <param name="logmanager">The logger.</param>
        public ServiceContainerBase(ILogManager logmanager=null)
        {
            this._logger = logmanager?.CreateLogger(this.GetType()) ?? new NoLogger();
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">Type of instance of service</typeparam>
        /// <param name="scope">Scope for service</param>
        public virtual void Register<TService, TImplementation>(ScopeServiceEnum scope = ScopeServiceEnum.Transient) where TImplementation:class,TService where TService:class
        {
            this.Register<TService>(this._createInstance<TService, TImplementation>, scope);
        }

        /// <summary>
        /// Registers the specified service factory.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceFactory">The service factory.</param>
        /// <param name="scope">Scope for service</param>

        public virtual void Register<TService>(Func<TService> serviceFactory, ScopeServiceEnum scope = ScopeServiceEnum.Transient) where TService:class
        {
            if (this._logger.InfoIsEnabled)
                this._logger.Info("Register Type {0} in mode {1}",typeof(TService),scope);

            this.InternalRegister<TService>(serviceFactory, scope);
        }

        /// <summary>
        /// Registers the specified service in container.
        /// </summary>
        /// <typeparam name="TService">Service contract</typeparam>
        /// <param name="service">The service implementation.</param>
        public virtual void RegisterSingleton<TService>(TService service)where TService:class
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.Register<TService>(() => service, ScopeServiceEnum.Singleton);
        }

        /// <summary>
        /// Resolves the type.
        /// </summary>
        /// <typeparam name="TService">Service Contract</typeparam>
        /// <returns>
        /// Service implementation
        /// </returns>
        public virtual TService ResolveType<TService>() where TService:class
        {
            if (this._logger.InfoIsEnabled)
                this._logger.Info("Resolve Type {0}.", typeof(TService));

            var val = this.InternalResolveType<TService>();

            if (this._logger.DebugIsEnabled)
                this._logger.Debug("Object type: {0}", val?.GetType());

            return val;
        }

        private TService _createInstance<TService,TImplementation>() where TImplementation : class, TService
        {
            try
            {
                if (this._logger.InfoIsEnabled)
                    this._logger.Info("Create instance {0}",typeof(TImplementation));

                var instance = Activator.CreateInstance<TImplementation>();
                return instance;
            }
            catch(Exception)
            {
                throw new Shiva.Exceptions.InvalidTypeConstructorForServiceContainerException(typeof(TImplementation));
            }
        }

        /// <summary>
        /// Internals implementation of resolving type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>service implementation</returns>
        protected abstract TService InternalResolveType<TService>() where TService:class;

        /// <summary>
        /// Internals the register.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceFactory">The service factory.</param>
        /// <param name="scope">The scope.</param>
        protected abstract void InternalRegister<TService>(Func<TService> serviceFactory, ScopeServiceEnum scope) where TService:class;
    }
}
