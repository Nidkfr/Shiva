using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Shiva.IO;
using FluentAssertions;

namespace Shiva.Ressources
{
    [TestClass]
    public class UTXmlRessourceManager
    {
        [TestMethod]
        public void TestInitializer()
        {
            var manager = new XmlRessourceManager(CultureInfo.GetCultureInfo("fr"), new FileSource("./ressources.xml"));
            Assert.IsTrue(manager.Culture == CultureInfo.GetCultureInfo("fr"));            
        }

        [TestMethod]
        public void FailInitializer()
        {
            Action<CultureInfo, StreamSource> _ctor = (x, y) => new XmlRessourceManager(x, y);

            _ctor.Invoking(x => x(null, new FileSource("./ressources.xml"))).Should().Throw<ArgumentNullException>();
            _ctor.Invoking(x => x(CultureInfo.GetCultureInfo("fr"), null)).Should().Throw<ArgumentNullException>();
        }
    }
}
