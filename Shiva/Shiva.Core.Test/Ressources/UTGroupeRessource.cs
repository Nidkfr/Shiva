using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Shiva.Core.Identities;
using FluentAssertions;

namespace Shiva.Ressources
{
    [TestClass]
    public class UTGroupeRessource : BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialize()
        {
            var grp = new RessourceGroup<RessourceString>("test", CultureInfo.CurrentCulture, new Core.Identities.IdentifiableList<RessourceString> { new RessourceString("test","value") });
            Assert.IsTrue(grp.Id == "test");
            Assert.IsTrue(grp.Culture == CultureInfo.CurrentCulture);
            Assert.IsTrue(grp.Ressources.Count == 1);
        }

        [TestMethod]
        public void FailInitialize()
        {
            Action<Identity,CultureInfo,IdentifiableList<RessourceString>> ctor = (X, y, z) => new RessourceGroup<RessourceString>(X,y,z);
            ctor.Invoking(x => x(null, CultureInfo.CurrentCulture, new IdentifiableList<RessourceString>())).Should().Throw<ArgumentNullException>();
            ctor.Invoking(x => x("test",null, new IdentifiableList<RessourceString>())).Should().Throw<ArgumentNullException>();
            ctor.Invoking(x => x("test", CultureInfo.CurrentCulture, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
