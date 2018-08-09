using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.Core.IO;
using Shiva.Core.Services;
using FluentAssertions;

namespace Shiva.Permission.Xml
{
    [TestClass]
    public class UTPermissionXmlManager : BaseTest, IPermissionManagerTester
    {
        private PermissionManagerTester _tester = new PermissionManagerTester();

        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitializer()
        {
            using (var streamsource = new FileSource("./GETSET/Permission.xml"))
            {
                using (var manager = new PermissionManagerXml(this.LogManager))
                {
                    Assert.IsFalse(manager.IsInitialized);
                    manager.Initialize(streamsource);
                    Assert.IsTrue(manager.IsInitialized);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/Permission.xml", "GETSET")]
        public void FailGetSetRole()
        {
            using (var streamsource = new FileSource("./GETSET/Permission.xml"))
            {
                using (var manager = new PermissionManagerXml(this.LogManager))
                {                   
                    manager.Initialize(streamsource);
                    this._tester.FailGetSetRole(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/Permission.xml", "GETSET")]
        public void TestGetSetRole()
        {
            using (var streamsource = new FileSource("./GETSET/Permission.xml"))
            {
                using (var manager = new PermissionManagerXml(this.LogManager))
                {                    
                    manager.Initialize(streamsource);
                    this._tester.TestGetSetRole(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/Permission.xml", "GETSETAsync")]
        public void TestGetSetRoleAsync()
        {
            using (var streamsource = new FileSource("./GETSETAsync/Permission.xml"))
            {
                using (var manager = new PermissionManagerXml(this.LogManager))
                {                   
                    manager.Initialize(streamsource);
                    this._tester.TestGetSetRoleAsync(manager);
                }
            }
        }
    }
}
