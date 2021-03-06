﻿using System;
using System.Collections.Generic;
using System.Text;
using Shiva.Core.Ioc;
using Shiva.Core.Services;
using SimpleInjector;


namespace Shiva.Services
{
    /// <summary>
    /// Service Injector service container implementation
    /// </summary>
    public sealed class SimpleInjectorServiceContainer : ServiceContainerBase
    {
        private Container _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorServiceContainer"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="logmanager">LogManager</param>
        public SimpleInjectorServiceContainer(Container container, ILogManager logmanager = null):base(logmanager)
        {            
            this._container = container ?? throw new ArgumentNullException(nameof(container));            
        }

        /// <summary>
        /// Internals the register.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceFactory">The service factory.</param>
        /// <param name="scope">The scope.</param>
        protected override void InternalRegister<TService>(Func<TService> serviceFactory, ScopeServiceEnum scope)
        {
            switch (scope)
            {
                case ScopeServiceEnum.TRANSIENT:
                    this._container.Register<TService>(serviceFactory, Lifestyle.Transient);
                    break;
                case ScopeServiceEnum.SINGLETON:
                    this._container.Register<TService>(serviceFactory, Lifestyle.Singleton);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Internals the type of the resolve.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        protected override TService InternalResolveType<TService>()
        {
            return this._container.GetInstance<TService>();
        }

        /// <summary>
        /// Registers the specified scope.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="scope">The scope.</param>
        public override void Register<TService, TImplementation>(ScopeServiceEnum scope = ScopeServiceEnum.TRANSIENT)
        {
            if (this.Logger.InfoIsEnabled)
                this.Logger.Info("Register Type {0} with instance {1}  in {2} scope mode",typeof(TService),typeof(TImplementation),scope);

            switch (scope)
            {
                case ScopeServiceEnum.TRANSIENT:
                    this._container.Register<TService, TImplementation>(Lifestyle.Transient);
                    break;
                case ScopeServiceEnum.SINGLETON:
                    this._container.Register<TService, TImplementation>(Lifestyle.Singleton);
                    break;
                default:
                    throw new NotSupportedException();                    
            }
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="service">The service.</param>
        public override void RegisterSingleton<TService>(TService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (this.Logger.InfoIsEnabled)
                this.Logger.Info("Register Type {0} with instance {1}  in singleton scope mode", typeof(TService), service.GetType());
           
            this._container.RegisterInstance<TService>(service);
        }

        /// <summary>
        /// Internals the register initializer.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="initializer">The initializer.</param>
        protected override void InternalRegisterInitializer<TService>(Action<TService> initializer)
        {
            this._container.RegisterInitializer(initializer);
        }
    }
}
