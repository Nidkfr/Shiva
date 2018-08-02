using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using FluentAssertions;
using Moq;
using Shiva.Core.Services;
using Shiva.Core.Ioc;
using System.Collections;
using System.Collections.Generic;

namespace Shiva.Services
{
    [TestClass]
    public class UTSimpleInjectorServiceContainer:Shiva.BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialization()
        {
            var instance = new SimpleInjectorServiceContainer(new Container(),this.LogManager);            
        }

        [TestMethod]
        public void FailInitialization()
        {
            Action _ctor = () => new SimpleInjectorServiceContainer(null);
            _ctor.Invoking(x => x()).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestRegister()
        {
            var instance = new SimpleInjectorServiceContainer(new Container(), this.LogManager);

            instance.Register<IServiceContainer>(() => instance, ScopeServiceEnum.SINGLETON);
            instance.Register<ILogManager, Log4NetLogManager>();
            instance.RegisterSingleton<IEnumerable>(new List<string>());
        }

        [TestMethod]
        public void TestResolveType()
        {
            var instance = new SimpleInjectorServiceContainer(new Container(), this.LogManager);

            instance.Register<IServiceContainer>(() => instance, ScopeServiceEnum.SINGLETON);
            instance.Register<ILogManager, Log4NetLogManager>();
            instance.RegisterSingleton<IEnumerable>(new List<string>());

            Assert.IsNotNull(instance.ResolveType<IServiceContainer>());
            Assert.IsNotNull(instance.ResolveType<ILogManager>());
            Assert.IsNotNull(instance.ResolveType<IEnumerable>());
        }
    }
}
