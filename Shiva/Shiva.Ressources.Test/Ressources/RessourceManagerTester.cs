using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            //create 2 resource with different type
            manager.SetRessource(new RessourceString("Test.Ressource1", "test value"));            
            manager.SetRessource(new RessourceBinary("Test.Ressource1", System.Text.Encoding.ASCII.GetBytes("test value")));

            var ressource = manager.GetRessource<RessourceString>("Test.Ressource1");
            Assert.IsTrue(ressource.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Value == "test value");

            manager.RemoveRessource<RessourceString>("Test.Ressource1");

            Assert.IsFalse(manager.ContainsRessource<RessourceString>("Test.Ressource1"));
            Assert.IsTrue(manager.ContainsRessource<RessourceBinary>("Test.Ressource1"));

            var ressourceb = manager.GetRessource<RessourceBinary>("Test.Ressource1");
            Assert.IsTrue(ressourceb.Id == "Test.Ressource1");
            Assert.IsTrue(ressourceb.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(System.Text.Encoding.ASCII.GetString(ressourceb.Value) == "test value");

            //add ressource with another culture
            manager.SetRessource(new RessourceString("Test.Ressource1", "test value in fr",CultureInfo.GetCultureInfo("fr")));

            //ressource is overrided but save in culture of ressource manager
            ressource = manager.GetRessource<RessourceString>("Test.Ressource1");
            Assert.IsTrue(ressource.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Value == "test value in fr");

        }
    }
}
