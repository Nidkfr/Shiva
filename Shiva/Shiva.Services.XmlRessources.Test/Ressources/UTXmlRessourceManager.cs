using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using FluentAssertions;

namespace Shiva.Ressources
{
    [TestClass]    
    public class UTXmlRessourceManager:BaseTest, IRessourceManagerTester
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
            using (var manager = new XmlRessourceManager(this.LogManager))
            {
                Assert.IsNull(manager.Culture);
                Assert.IsFalse(manager.IsInitialized);
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Data")]
        public void TestIntialize()
        {
            using (var stream = File.Open("./Data/RessourceXml.xml", FileMode.Open))
            {
                using (var manager = new XmlRessourceManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"),stream);
                    Assert.IsTrue(manager.Culture == CultureInfo.GetCultureInfo("en"));
                    Assert.IsTrue(manager.IsInitialized);
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Data")]
        public void FailInitialize()
        {
            using (var stream = File.Open("./Data/RessourceXml.xml", FileMode.Open))
            {
                using (var manager = new XmlRessourceManager(this.LogManager))
                {
                    manager.Invoking(x => x.Initialize(null, stream)).Should().Throw<ArgumentNullException>();
                    manager.Invoking(x => x.Initialize(CultureInfo.GetCultureInfo("en"), null)).Should().Throw<ArgumentNullException>();
                }
            }
        }

        [TestMethod]
        [DeploymentItem("DeployItems/RessourceXml.xml", "Data")]
        public void TestGetRessource()
        {
            using (var stream = File.Open("./Data/RessourceXml.xml", FileMode.Open))
            {
                using (var manager = new XmlRessourceManager(this.LogManager))
                {
                    manager.Initialize(CultureInfo.GetCultureInfo("en"), stream);
                    this._tester.TestGetRessource(manager);
                }
            }
        }
    }
}
