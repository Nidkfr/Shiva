﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.Core.Services;
using Shiva.Mocks;

namespace Shiva.Core.Ioc
{
    public sealed class TesterIServiceContainer
    {
        public void TestRegister(IServiceContainer container)
        {
            container.Register<IEnumerable<string>>(() => new List<string>(), ScopeServiceEnum.TRANSIENT);
            container.Register<IEnumerable<int>>(() => new List<int>(), ScopeServiceEnum.SINGLETON);
            container.Register<AbstractVoidClass<string>, VoidClass1<string>>(ScopeServiceEnum.TRANSIENT);
            container.Register<AbstractVoidClass<bool>, VoidClass1<bool>>(ScopeServiceEnum.SINGLETON);
        }

        public void FailRegister(IServiceContainer container)
        {
            container.Invoking(x => x.Register<IEnumerable<string>>(null)).Should().Throw<ArgumentNullException>();
        }

        public void TestRegisterSingleton(IServiceContainer container)
        {
            container.RegisterSingleton<IEnumerable<string>>(new List<string>());
        }

        public void FailRegisterSingleton(IServiceContainer container)
        {
            container.Invoking(x => x.RegisterSingleton<IEnumerable<string>>(null)).Should().Throw<ArgumentNullException>();
        }

        public void TestResolveType(IServiceContainer container)
        {
            container.Register<ILogger, NoLogger>(ScopeServiceEnum.SINGLETON);
            this.TestRegister(container);
            Assert.IsNotNull(container.ResolveType<IEnumerable<string>>());
            Assert.IsTrue(container.ResolveType<IEnumerable<string>>() is IEnumerable<string>);

            Assert.IsNotNull(container.ResolveType<IEnumerable<int>>());
            Assert.IsTrue(container.ResolveType<IEnumerable<int>>()== container.ResolveType<IEnumerable<int>>());

            Assert.IsNotNull(container.ResolveType<AbstractVoidClass<string>>());
            Assert.IsTrue(container.ResolveType<AbstractVoidClass<string>>() is VoidClass1<string>);

            Assert.IsNotNull(container.ResolveType<AbstractVoidClass<bool>>());
            Assert.IsTrue(container.ResolveType<AbstractVoidClass<bool>>() == container.ResolveType<AbstractVoidClass<bool>>());
        }

        public void TestRegisterInitializer(IServiceContainer container)
        {            
            container.Register<AbstractVoidClass<string>, VoidClass1<string>>();
            container.Register<ILogger, NoLogger>();
            container.RegisterInitialize<AbstractVoidClass<string>>(x => 
            {
                x.Value = "test";
            });
            var val = container.ResolveType<AbstractVoidClass<string>>();
            Assert.IsTrue(val.Value == "test");
        }
    }

    public interface ITesterIServiceContainer
    {
        
        TesterIServiceContainer Tester { get; }

        void TestRegister();
        void FailRegister();
        void TestRegisterSingleton();
        void FailRegisterSingleton();
        void TestResolveType();
        void TestRegisterInitializer();
    }
}
