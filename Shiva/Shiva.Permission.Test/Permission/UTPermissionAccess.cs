using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.Core.Identities;
using FluentAssertions;

namespace Shiva.Permission
{
    [TestClass]
    public class UTPermissionAccess:BaseTest
    {

        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitializer()
        {
            var p = new PermissionAccess("test");
            Assert.IsTrue(p.Id == "test");
            Assert.IsFalse(p.Acces);
        }

        [TestMethod]
        public void FailInitializer()
        {
            Action<Identity> ctor = x => new PermissionAccess(x);
            ctor.Invoking(x => x(null)).Should().Throw<ArgumentNullException>();
        }

        
    }
}
