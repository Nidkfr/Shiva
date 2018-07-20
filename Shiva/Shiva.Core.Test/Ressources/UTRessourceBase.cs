using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using System.Globalization;
using Shiva.Core.Identities;

namespace Shiva.Ressources
{
    [TestClass]
    public class UTRessourceBase : BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialize()
        {
            var mock = new Mock<RessourceBase>((Identity)"test", CultureInfo.CurrentCulture);
            var ressource = mock.Object;

            Assert.IsTrue(ressource.Id == "test");
            Assert.IsTrue(ressource.Culture == CultureInfo.CurrentCulture);
            Assert.IsFalse(ressource.IsEmptyRessource);

            mock = new Mock<RessourceBase>();
            ressource = mock.Object;

            Assert.IsNull(ressource.Id);
            Assert.IsNull(ressource.Culture);
            Assert.IsTrue(ressource.IsEmptyRessource);
        }

        [TestMethod]
        public void FailInitialize()
        {
            var mock = new Mock<RessourceBase>((Identity)null, It.IsAny<CultureInfo>());
            mock.Invoking(x => x.Object?.ToString())
                .Should()
                .Throw<System.Reflection.TargetInvocationException>()
                .WithInnerException<ArgumentNullException>();
        }

        [TestMethod]
        public void TestSetCulture()
        {
            var mock = new Mock<RessourceBase>((Identity)"test", CultureInfo.GetCultureInfo(1));
            var ressource = mock.Object;

            ressource.SetCulture(CultureInfo.GetCultureInfo(2));
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo(2));
        }

        [TestMethod]        
        public void FailSetCulture()
        {
            var mock = new Mock<RessourceBase>((Identity)"test", CultureInfo.GetCultureInfo(1));
            var ressource = mock.Object;

            ressource.Invoking(x => x.SetCulture(null))
                .Should()
                .Throw<ArgumentNullException>();
        }
    }
}
