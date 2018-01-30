using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using System.Globalization;

namespace Shiva.Globalization
{
    [TestClass]
    public class UTRessourceManagerBase
    {
        [TestMethod]
        public void TestInitialization()
        {
            var m = new Mock<RessourceManagerBase>();
            Assert.IsTrue(m.Object.CurrentCulture == CultureInfo.CurrentCulture);
            Assert.IsTrue(m.Object.CurrentUICulture == CultureInfo.CurrentUICulture);
        }
    }
}
