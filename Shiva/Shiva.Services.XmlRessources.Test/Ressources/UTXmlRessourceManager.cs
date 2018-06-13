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
            var manager = new XmlRessourceManager(CultureInfo.GetCultureInfo("fr"), File.OpenRead("./ressources.xml"));
            Assert.IsTrue(manager.Culture == CultureInfo.GetCultureInfo("fr"));            
        }

        [TestMethod]
        public void FailInitializer()
        {
            Action<CultureInfo, Stream> _ctor = (x, y) => new XmlRessourceManager(x, y);

            _ctor.Invoking(x => x(null, File.OpenRead("./ressources.xml"))).Should().Throw<ArgumentNullException>();
            _ctor.Invoking(x => x(CultureInfo.GetCultureInfo("fr"), null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestGetRessource()
        {
            var manager = new XmlRessourceManager(CultureInfo.GetCultureInfo("fr"), File.OpenRead("./ressources.xml"));
            this._tester.TestGetRessource(manager);
        }
    }
}
