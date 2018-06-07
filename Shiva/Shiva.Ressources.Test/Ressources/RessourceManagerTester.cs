using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.IO;
using FluentAssertions;

namespace Shiva.Ressources
{
    public interface IRessourceManagerTester
    {
        void TestGetRessource();
    }

    public class RessourceManagerTester
    {
        public void TestGetRessource(IRessourceManager manager)
        {
            manager.SaveRessource(new RessourceString("Test.Ressource1", "test value", CultureInfo.GetCultureInfo("en")));
            manager.SaveRessource(new RessourceString("Test.Ressource1", "test value", CultureInfo.GetCultureInfo("fr")));

            var ressource = manager.GetRessource<RessourceString>("Test.Ressource1");
            Assert.IsTrue(ressource.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("fr"));
            Assert.IsTrue(ressource.Value == "test value");

            manager.RemoveRessource<RessourceString>("Test.Ressource1");

            Assert.IsFalse(manager.ContainsRessource<RessourceString>("Test.Ressource1"));
        }
    }
}
