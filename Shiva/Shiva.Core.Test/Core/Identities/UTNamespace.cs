using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Shiva.Core.Identities
{
    [TestClass]
    public class UTNamespace:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialization()
        {
            var ns = new Namespace("Shiva.Core.Identity");
            Assert.IsTrue(ns == "Shiva.Core.Identity");
            Assert.IsTrue(ns.RootNode == "Shiva");
            Assert.IsTrue(ns.LastNode == "Identity");

            ns = new Namespace(" Shiva . Core . Identity ");
            Assert.IsTrue(ns == "Shiva.Core.Identity");
            Assert.IsTrue(ns.RootNode == "Shiva");
            Assert.IsTrue(ns.LastNode == "Identity");
        }

        [TestMethod]
        public void FailInitialization()
        {
            Action<string> ctor = x => new Namespace(x);

            ctor.Invoking(x => x(null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestGetNextNode()
        {
            var ns = new Namespace("Shiva.Core.Identity");
            Assert.IsTrue(ns.GetNextNode(ns.RootNode) == "Core");
            Assert.IsTrue(ns.GetNextNode(ns.GetNextNode(ns.RootNode)) == "Identity");
            Assert.IsNull(ns.GetNextNode(ns.LastNode));
        }

        [TestMethod]
        public void FailGetNextNode()
        {
            var ns = new Namespace("Shiva.Core.Identity");
            ns.Invoking(x => x.GetNextNode(null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestOperator()
        {
            var ns = new Namespace("Shiva.Core.Identity");

            Assert.IsTrue(ns == "Shiva.Core.Identity");
            Assert.IsTrue("Shiva.Core.Identity" == ns);
            Assert.IsTrue("Shiva.Core.Identity.1" != ns);
            Assert.IsTrue(ns != "Shiva.Core.Identity.1");
            Assert.IsTrue(Namespace.Null == "");
            Assert.IsTrue("" == Namespace.Null);


            var l = new List<string>();
            l.Invoking(x => x.Add(ns)).Should().NotThrow();

            var l2 = new List<Namespace>();
            l2.Invoking(x => x.Add("Shiva.Core.Identity")).Should().NotThrow();

        }
    }
}
