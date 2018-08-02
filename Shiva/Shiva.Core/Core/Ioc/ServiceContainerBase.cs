using Shiva.Core.Services;
using System;

namespace Shiva.Core.Ioc
{
    /// <summary>
    /// Service container base
    /// </summary>
    /// <seealso cref="Shiva.Core.Ioc.IServiceContainer" />
    ///
    public abstract class ServiceContainerBase : IServiceContainer
    {
        #region Private Fields

        private readonly ILogger _logger;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContainerBase" /> class.
        /// </summary>
        /// <param name="logmanager">
        /// The logger.
        /// </param>
        public ServiceContainerBase(ILogManager logmanager = null)
        {
            this._logger = logmanager?.CreateLogger(this.GetType()) ?? new NoLogger();
        }

        #endregion Public Constructors

        #region Protected Properties

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger => this._logger;

        #endregion Protected Properties

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
        public virtual void Register<TService, TImplementation>(ScopeServiceEnum scope = ScopeServiceEnum.TRANSIENT) where TImplementation : class, TService where TService : class
        {
            this.Register<TService>(this._createInstance<TService, TImplementation>, scope);
        }

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

        public virtual void Register<TService>(Func<TService> serviceFactory, ScopeServiceEnum scope = ScopeServiceEnum.TRANSIENT) where TService : class
        {
            if (this._logger.InfoIsEnabled)
                this._logger.Info("Register Type {0} in mode {1}", typeof(TService), scope);

            this.InternalRegister<TService>(serviceFactory, scope);
        }

        /// <summary>
        /// Registers the initialize.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <param name="initializer">
        /// The initializer.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void RegisterInitialize<TService>(Action<TService> initializer) where TService : class
        {
            if (initializer == null)
                throw new ArgumentNullException(nameof(initializer));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info($"Register a initializer for type {typeof(TService)}");

            this.InternalRegisterInitializer<TService>(initializer);
        }

        /// <summary>
        /// Registers the specified service in container.
        /// </summary>
        /// <typeparam name="TService">
        /// Service contract
        /// </typeparam>
        /// <param name="service">
        /// The service implementation.
        /// </param>
        public virtual void RegisterSingleton<TService>(TService service) where TService : class
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            this.Register<TService>(() => service, ScopeServiceEnum.SINGLETON);
        }

        /// <summary>
        /// Resolves the type.
        /// </summary>
        /// <typeparam name="TService">
        /// Service Contract
        /// </typeparam>
        /// <returns>
        /// Service implementation
        /// </returns>
        public virtual TService ResolveType<TService>() where TService : class
        {
            if (this._logger.InfoIsEnabled)
                this._logger.Info("Resolve Type {0}.", typeof(TService));

            var val = this.InternalResolveType<TService>();

            if (this._logger.DebugIsEnabled)
                this._logger.Debug("Object type: {0}", val?.GetType());

            return val;
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Internals the register.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <param name="serviceFactory">
        /// The service factory.
        /// </param>
        /// <param name="scope">
        /// The scope.
        /// </param>
        protected abstract void InternalRegister<TService>(Func<TService> serviceFactory, ScopeServiceEnum scope) where TService : class;

        /// <summary>
        /// Internals the register initializer.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <param name="initializer">
        /// The initializer.
        /// </param>
        protected abstract void InternalRegisterInitializer<TService>(Action<TService> initializer) where TService : class;

        /// <summary>
        /// Internals implementation of resolving type.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <returns>
        /// service implementation
        /// </returns>
        protected abstract TService InternalResolveType<TService>() where TService : class;

        #endregion Protected Methods

        #region Private Methods

        private TService _createInstance<TService, TImplementation>() where TImplementation : class, TService
        {
            try
            {
                if (this._logger.InfoIsEnabled)
                    this._logger.Info("Create instance {0}", typeof(TImplementation));

                var instance = Activator.CreateInstance<TImplementation>();
                return instance;
            }
            catch (Exception)
            {
                throw new Shiva.Exceptions.InvalidTypeConstructorForServiceContainerException(typeof(TImplementation));
            }
        }

        #endregion Private Methods
    }
}