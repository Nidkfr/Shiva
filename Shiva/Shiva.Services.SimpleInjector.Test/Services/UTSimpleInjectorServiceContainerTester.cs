using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.Services;
using SimpleInjector;

namespace Shiva.Core.Ioc
{
    [TestClass]
    public class UTSimpleInjectorServiceContainerTester : BaseTest, ITesterIServiceContainer
    {
        public TesterIServiceContainer Tester { get => new TesterIServiceContainer(); }

        [TestMethod]
        public void FailRegister()
        {
            this.Tester.FailRegister(new SimpleInjectorServiceContainer(new Container(), this.LogManager));
        }

        [TestMethod]
        public void FailRegisterSingleton()
        {
            this.Tester.FailRegisterSingleton(new SimpleInjectorServiceContainer(new Container(), this.LogManager));
        }

        [TestMethod]
        public void TestRegister()
        {
            this.Tester.TestRegister(new SimpleInjectorServiceContainer(new Container(), this.LogManager));
        }

        [TestMethod]
        public void TestRegisterInitializer()
        {
            this.Tester.TestRegisterInitializer(new SimpleInjectorServiceContainer(new Container(), this.LogManager));
        }

        [TestMethod]
        public void TestRegisterSingleton()
        {
            this.Tester.TestRegisterSingleton(new SimpleInjectorServiceContainer(new Container(), this.LogManager));
        }

        [TestMethod]
        public void TestResolveType()
        {       
            this.Tester.TestResolveType(new SimpleInjectorServiceContainer(new Container(), this.LogManager));
        }
    }
}
