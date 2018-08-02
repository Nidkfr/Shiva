using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using FluentAssertions;
using Shiva.Core.IO;

namespace Shiva.Ressources.Xml
{
    [TestClass]
    public class UTXmlRessourceManager : BaseTest, IRessourceManagerTester
    {

        private RessourceManagerTester _tester = new RessourceManagerTester();

        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }


        [TestMethod]
        public void TestInitializer()
        {
            using (var manager = new RessourceXmlManager(this.LogManager))
            {
                Assert.IsNull(manager.Culture);
                Assert.IsFalse(manager.IsInitialized);
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "GETSET")]
        public void TestIntialize()
        {
            using (var streamsource = new FileSource("./GETSET/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    Assert.IsTrue(manager.Culture == CultureInfo.GetCultureInfo("en"));
                    Assert.IsTrue(manager.IsInitialized);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "GETSET")]
        public void FailInitialize()
        {
            using (var streamsource = new FileSource("./GETSET/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Invoking(x => x.Initialize(null, streamsource)).Should().Throw<ArgumentNullException>();
                    manager.Invoking(x => x.Initialize(CultureInfo.GetCultureInfo("en"), null)).Should().Throw<ArgumentNullException>();
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "GETSET")]
        public void TestGeSettRessource()
        {

            using (var streamsource = new FileSource("./GETSET/RessourceXml.xml", FileSourceSaveModeEnum.KEEPALLPREVIOUSVERSION))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestGetSetRessource(manager);
                }
            }

        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "GETSET")]
        public void TestGetSetRessourceAsync()
        {
            using (var streamsource = new FileSource("./GETSET/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestGetSetRessourceAsync(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Performance")]
        public void TestPerformanceGetRessource()
        {
            using (var streamsource = new FileSource("./Performance/RessourceXml.xml"))
            {
                using (var managerEn = new RessourceXmlManager(this.LogManager))
                {
                    managerEn.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    using (var managerFr = new RessourceXmlManager(this.LogManager))
                    {
                        managerFr.Initialize(CultureInfo.GetCultureInfo("fr"), streamsource);
                        this._tester.TestPerformanceGetRessource(managerEn, managerFr);
                    }
                }
            }
        }


        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "GETSET")]
        public void FailGetSetRessource()
        {
            using (var streamsource = new FileSource("./GETSET/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.FailGetSetRessource(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Contains")]
        public void TestContainsRessource()
        {
            using (var streamsource = new FileSource("./Contains/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestContainsRessource(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Contains")]
        public void TesContainsRessourceAsync()
        {
            using (var streamsource = new FileSource("./Contains/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestContainsRessourceAsync(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Contains")]
        public void FailContainsRessource()
        {
            using (var streamsource = new FileSource("./Contains/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.FailContainsRessource(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Remove")]
        public void TestRemoveRessource()
        {
            using (var streamsource = new FileSource("./Remove/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestRemoveRessource(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Remove")]
        public void TestRemoveRessourceAsync()
        {
            using (var streamsource = new FileSource("./Remove/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestRemoveRessourceAsync(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Remove")]
        public void FailRemoveRessourceAsync()
        {
            using (var streamsource = new FileSource("./Remove/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.FailRemoveRessourceAsync(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Group")]
        public void TestGroup()
        {
            using (var streamsource = new FileSource("./Group/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestGroup(manager);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Group")]
        public void TestPerformanceGroup()
        {
            using (var streamsource = new FileSource("./Group/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestPerformanceGroup(manager);
                }
            }
        }
    }
}
