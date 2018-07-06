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
        [DeploymentItem("DeployItems/RessourceXml.xml", "Data")]
        public void TestIntialize()
        {
            using (var streamsource = new FileSource("./Data/RessourceXml.xml"))
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
        [DeploymentItem("DeployItems/RessourceXml.xml", "Data")]
        public void FailInitialize()
        {
            using (var streamsource = new FileSource("./Data/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Invoking(x => x.Initialize(null, streamsource)).Should().Throw<ArgumentNullException>();
                    manager.Invoking(x => x.Initialize(CultureInfo.GetCultureInfo("en"), null)).Should().Throw<ArgumentNullException>();
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Data")]
        public void TestGetRessource()
        {

            using (var streamsource = new FileSource("./Data/RessourceXml.xml", FileSourceSaveModeEnum.KEEPALLPREVIOUSVERSION))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestGetRessource(manager);
                }
            }

        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Data")]
        public void TestGetRessourceAsync()
        {
            using (var streamsource = new FileSource("./Data/RessourceXml.xml"))
            {
                using (var manager = new RessourceXmlManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), streamsource);
                    this._tester.TestGetRessourceAsync(manager);
                }
            }
        }
    }
}
