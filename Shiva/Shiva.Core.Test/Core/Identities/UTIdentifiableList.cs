using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FluentAssertions;

namespace Shiva.Core.Identities
{
    [TestClass]
    public class UTIdentifiableList:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitializer()
        {
            var lst = new IdentifiableList<Ressources.RessourceString>();
            Assert.IsTrue(lst.Count == 0);
            Assert.IsTrue(lst.Ids.Count() == 0);
            Assert.IsTrue(lst.RemovedElement.Count() == 0);            
        }

        [TestMethod]
        public void TestAdd()
        {
            var lst = new IdentifiableList<Ressources.RessourceString>();
            lst.Add(new Ressources.RessourceString("test", "value"));
            Assert.IsTrue(lst.Count == 1);
            Assert.IsTrue(lst.Ids.Count() == 1);
            Assert.IsTrue(lst.RemovedElement.Count() == 0);
            Assert.IsTrue(lst.Count() == 1);

            Assert.IsTrue(lst["test"].Value == "value");
            
        }

        [TestMethod]
        public void FailAdd()
        {
            var lst = new IdentifiableList<Ressources.RessourceString>();
            lst.Invoking(x => x.Add(null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestRemoveElement()
        {
            var lst = new IdentifiableList<Ressources.RessourceString>();
            var ressource = new Ressources.RessourceString("test2", "value");
            lst.Add(new Ressources.RessourceString("test", "value"));
            lst.Add(ressource);

            Assert.IsTrue(lst.Count == 2);
            Assert.IsTrue(lst.Ids.Count() == 2);
            Assert.IsTrue(lst.RemovedElement.Count() == 0);
            Assert.IsTrue(lst.Count() == 2);

            lst.Remove("test");
            Assert.IsTrue(lst.Count == 1);
            Assert.IsTrue(lst.Ids.Count() == 1);
            Assert.IsTrue(lst.RemovedElement.Count() == 1);
            Assert.IsTrue(lst.Count() == 1);

            lst.Remove(ressource);
            Assert.IsTrue(lst.Count == 0);
            Assert.IsTrue(lst.Ids.Count() == 0);
            Assert.IsTrue(lst.RemovedElement.Count() == 2);
            Assert.IsTrue(lst.Count() == 0);
        }

        [TestMethod]
        public void FailRemoveElement()
        {
            var lst = new IdentifiableList<Ressources.RessourceString>();
            lst.Invoking(x => x.Remove((Identity)null)).Should().Throw<ArgumentNullException>();
            lst.Invoking(x => x.Remove((Ressources.RessourceString)null)).Should().Throw<ArgumentNullException>();
        }

        public void TestClear()
        {
            var lst = new IdentifiableList<Ressources.RessourceString>();
            var ressource = new Ressources.RessourceString("test2", "value");
            lst.Add(new Ressources.RessourceString("test", "value"));
            lst.Add(ressource);
            lst.Remove("test");
            Assert.IsTrue(lst.Count == 1);
            Assert.IsTrue(lst.Ids.Count() == 1);
            Assert.IsTrue(lst.RemovedElement.Count() == 1);
            Assert.IsTrue(lst.Count() == 1);

            lst.Clear();
            Assert.IsTrue(lst.Count == 0);
            Assert.IsTrue(lst.Ids.Count() == 0);
            Assert.IsTrue(lst.RemovedElement.Count() == 0);
            Assert.IsTrue(lst.Count() == 0);

        }

        [TestMethod]
        public void TestContains()
        {
            var lst = new IdentifiableList<Ressources.RessourceString>();
            var ressource = new Ressources.RessourceString("test2", "value");
            lst.Add(new Ressources.RessourceString("test", "value"));

            Assert.IsTrue(lst.Contains("test"));
            Assert.IsFalse(lst.Contains("test2"));
        }
    }
}
