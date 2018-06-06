using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using Shiva.Core.Services;
using Shiva.Services;
using Shiva.Exceptions;

namespace Shiva.Core.Ioc
{
    [TestClass]
    public class UTServiceContainerBase:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialization()
        {
            var mock = new Mock<ServiceContainerBase>(this.LogManager);
            var instance = mock.Object;
        }

        [TestMethod]
        public void TestRegisterType()
        {
            var mock = new Mock<ServiceContainerBase>(this.LogManager);
            var instance = mock.Object;

            instance.Register<ILogManager>(() => this.LogManager, ScopeServiceEnum.Singleton);
            instance.Register<ILogManager, Log4NetLogManager>();
        }

        [TestMethod]
        public void TestRegisterInitializer()
        {
            #pragma warning disable 0219
            var mock = new Mock<ServiceContainerBase>(this.LogManager);
            var instance = mock.Object;
                        
            instance.Register<ILogManager>(() => this.LogManager, ScopeServiceEnum.Singleton);
            instance.Register<ILogManager, Log4NetLogManager>();
            instance.RegisterInitialize<ILogManager>(x => { var ok= true; });            
        }
        
    }
}
