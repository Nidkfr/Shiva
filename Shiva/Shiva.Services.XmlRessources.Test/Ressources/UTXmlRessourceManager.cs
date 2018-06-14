using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using FluentAssertions;

namespace Shiva.Ressources
{
    [TestClass]
    public class UTXmlRessourceManager: IRessourceManagerTester
    {
        private RessourceManagerTester _tester = new RessourceManagerTester();

        [TestMethod]
        public void TestInitializer()
        {
            using (var manager = new XmlRessourceManager(CultureInfo.GetCultureInfo("fr"), File.Open("./ressources.xml", FileMode.OpenOrCreate)))
            {
                Assert.IsTrue(manager.Culture == CultureInfo.GetCultureInfo("fr"));
            }
        }

        [TestMethod]
        public void FailInitializer()
        {
            Action<CultureInfo, Stream> _ctor = (x, y) => new XmlRessourceManager(x, y);
            var stream = File.Open("./ressources.xml", FileMode.OpenOrCreate);
            _ctor.Invoking(x => x(null,stream )).Should().Throw<ArgumentNullException>();
            _ctor.Invoking(x => x(CultureInfo.GetCultureInfo("fr"), null)).Should().Throw<ArgumentNullException>();
            stream.Close();
        }

        [TestMethod]
        public void TestGetRessource()
        {
            using (var manager = new XmlRessourceManager(CultureInfo.GetCultureInfo("en"), File.Open("./ressources.xml", FileMode.OpenOrCreate)))
            {
                this._tester.TestGetRessource(manager);
            }
        }
    }
}
