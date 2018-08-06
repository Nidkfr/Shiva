using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.Core.Identities;
using FluentAssertions;
using System.Linq;


namespace Shiva.Permission
{
    [TestClass]
    public class UTRole:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitializer()
        {
            var r = new Role("test");
            Assert.IsTrue(r.Id == "test");
        }

        [TestMethod]
        public void FailInitializer()
        {
            Action<Identity> ctor = x => new Role(x);
            ctor.Invoking(x => x(null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestSetPermission()
        {
            var r = new Role("test");
            r.SetPermission(new PermissionAccess("test"));
            r.SetPermission(new PermissionAccess("test"));
            r.SetPermission(new PermissionAccess("test2"));

            Assert.IsTrue(r.GetPermissions().Count() == 2);
        }

        [TestMethod]
        public void FailAddPermission()
        {
            var r = new Role("test");
            r.Invoking(x => x.SetPermission(null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestRemovePermission()
        {
            var r = new Role("test");
            r.SetPermission(new PermissionAccess("test"));
            r.SetPermission(new PermissionAccess("test"));
            r.SetPermission(new PermissionAccess("test2"));
            r.RemovePermission(new PermissionAccess("test"));
            Assert.IsTrue(r.GetPermissions().Count() == 1);
            r.RemovePermission("test2");
            Assert.IsTrue(r.GetPermissions().Count() == 0);
            r.RemovePermission("test2"); // no exeception
        }

        [TestMethod]
        public void FailRemovePermission()
        {
            var r = new Role("test");
            r.Invoking(x => x.RemovePermission((Identity)null)).Should().Throw<ArgumentNullException>();
            r.Invoking(x => x.RemovePermission((IPermission)null)).Should().Throw<ArgumentNullException>();
        }
    }
}
