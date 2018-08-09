using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Shiva.Permission
{

    public interface IPermissionManagerTester
    {
        void TestGetSetRole();

        void TestGetSetRoleAsync();

        void FailGetSetRole();
    }


    public class PermissionManagerTester
    {
        public void TestGetSetRole(IPermissionManager manager)
        {
            var role = new Role("test");
            role.SetPermission(new PermissionAccess("test") { Acces = true });

            manager.SetRole(role);

            role = manager.GetRole<Role>("test");

            Assert.IsTrue(role.Id == "test");
            Assert.IsTrue(role.GetPermissions().Count() == 1);
            Assert.IsTrue(role.GetPermission<PermissionAccess>("test").Acces);
            Assert.IsNull(role.GetPermission<PermissionAccess>("fail"));

            role.SetPermission(new PermissionData("test2") { Mode = PermissionDataEnum.EDITABLE });

            manager.SetRole(role);

            Assert.IsTrue(role.Id == "test");
            Assert.IsTrue(role.GetPermissions().Count() == 2);
            Assert.IsTrue(role.GetPermission<PermissionData>("test2").Mode == PermissionDataEnum.EDITABLE);
            
        }

        public void TestGetSetRoleAsync(IPermissionManager manager)
        {
            var role = new Role("test");
            role.SetPermission(new PermissionAccess("test") { Acces = true });

            var t = manager.SetRoleAsync(role);
            t.Wait(100);

            Assert.IsTrue(t.IsCompleted);
        }

        public void FailGetSetRole(IPermissionManager manager)
        {
            manager.Invoking(x => x.SetRole(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
