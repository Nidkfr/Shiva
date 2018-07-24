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
        void TestGeSettRessource();

        void TestGetSetRessourceAsync();

        void FailGetSetRessource();

        void TestContainsRessource();

        void TesContainsRessourceAsync();

        void FailContainsRessource();

        void TestPerformanceGetRessource();

        void TestRemoveRessource();

        void TestRemoveRessourceAsync();

        void FailRemoveRessourceAsync();
    }

    public class RessourceManagerTester
    {
        public void TestGetSetRessource(IRessourceManager manager)
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
            manager.SetRessource(new RessourceString("Test.Ressource1", "test value in fr", CultureInfo.GetCultureInfo("fr")));

            //ressource is overrided but save in culture of ressource manager
            ressource = manager.GetRessource<RessourceString>("Test.Ressource1");
            Assert.IsTrue(ressource.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Value == "test value in fr");

            manager.Flush();

            ressource = manager.GetRessource<RessourceString>("Test.Ressource1");
            Assert.IsTrue(ressource.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Value == "test value in fr");

            ressourceb = manager.GetRessource<RessourceBinary>("Test.Ressource1");
            Assert.IsTrue(ressourceb.Id == "Test.Ressource1");
            Assert.IsTrue(ressourceb.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(System.Text.Encoding.ASCII.GetString(ressourceb.Value) == "test value");
        }

        public void TestGetSetRessourceAsync(IRessourceManager manager)
        {
            //create 2 resource with different type
            manager.SetRessource(new RessourceString("Test.Ressource1", "test value"));
            manager.SetRessource(new RessourceBinary("Test.Ressource1", System.Text.Encoding.ASCII.GetBytes("test value")));

            var ressource = manager.GetRessourceAsync<RessourceString>("Test.Ressource1");
            ressource.Wait(100);
            Assert.IsTrue(ressource.IsCompleted);
            Assert.IsTrue(ressource.Result.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Result.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Result.Value == "test value");

            manager.RemoveRessource<RessourceString>("Test.Ressource1");

            Assert.IsFalse(manager.ContainsRessource<RessourceString>("Test.Ressource1"));
            Assert.IsTrue(manager.ContainsRessource<RessourceBinary>("Test.Ressource1"));

            var ressourceb = manager.GetRessourceAsync<RessourceBinary>("Test.Ressource1");
            ressourceb.Wait(100);
            Assert.IsTrue(ressourceb.IsCompleted);
            Assert.IsTrue(ressourceb.Result.Id == "Test.Ressource1");
            Assert.IsTrue(ressourceb.Result.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(System.Text.Encoding.ASCII.GetString(ressourceb.Result.Value) == "test value");

            //add ressource with another culture
            manager.SetRessource(new RessourceString("Test.Ressource1", "test value in fr", CultureInfo.GetCultureInfo("fr")));

            //ressource is overrided but save in culture of ressource manager
            ressource = manager.GetRessourceAsync<RessourceString>("Test.Ressource1");
            ressource.Wait(100);
            Assert.IsTrue(ressource.IsCompleted);
            Assert.IsTrue(ressource.Result.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Result.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Result.Value == "test value in fr");

            manager.Flush();

            ressource = manager.GetRessourceAsync<RessourceString>("Test.Ressource1");
            ressource.Wait(100);
            Assert.IsTrue(ressource.IsCompleted);
            Assert.IsTrue(ressource.Result.Id == "Test.Ressource1");
            Assert.IsTrue(ressource.Result.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Result.Value == "test value in fr");

            ressourceb = manager.GetRessourceAsync<RessourceBinary>("Test.Ressource1");
            ressourceb.Wait(100);
            Assert.IsTrue(ressourceb.IsCompleted);
            Assert.IsTrue(ressourceb.Result.Id == "Test.Ressource1");
            Assert.IsTrue(ressourceb.Result.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(System.Text.Encoding.ASCII.GetString(ressourceb.Result.Value) == "test value");
        }

        public void FailGetSetRessource(IRessourceManager manager)
        {
            manager.Invoking(x => x.GetRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            Func<Task> call = () => manager.GetRessourceAsync<RessourceString>(null);
            call.Should().Throw<ArgumentNullException>();

            manager.Invoking(x => x.SetRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            Func<Task> call2 = () => manager.SetRessourceAsync<RessourceString>(null);
            call.Should().Throw<ArgumentNullException>();
            
        }

        public void TestContainsRessource(IRessourceManager manager)
        {
            manager.SetRessource(new RessourceString("test", "value", CultureInfo.GetCultureInfo(1)));
            Assert.IsTrue(manager.ContainsRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceBinary>("test"));
            manager.Flush();
            Assert.IsTrue(manager.ContainsRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceBinary>("test"));
        }

        public void TestContainsRessourceAsync(IRessourceManager manager)
        {
            manager.SetRessource(new RessourceString("test", "value", CultureInfo.GetCultureInfo(1)));
            var waitingresult = manager.ContainsRessourceAsync<RessourceString>("test");
            waitingresult.Wait(100);
            Assert.IsTrue(waitingresult.Result);
            waitingresult = manager.ContainsRessourceAsync<RessourceBinary>("test");
            waitingresult.Wait(100);
            Assert.IsFalse(waitingresult.Result);
            manager.Flush();
            waitingresult = manager.ContainsRessourceAsync<RessourceString>("test");
            waitingresult.Wait(100);
            Assert.IsTrue(waitingresult.Result);
            waitingresult = manager.ContainsRessourceAsync<RessourceBinary>("test");
            waitingresult.Wait(100);
            Assert.IsFalse(waitingresult.Result);
        }

        public void FailContainsRessource(IRessourceManager manager)
        {
            manager.Invoking(x => x.ContainsRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            var response = manager.ContainsRessourceAsync<RessourceString>(null);
            Func<Task> call = () => manager.ContainsRessourceAsync<RessourceString>(null);
            call.Should().Throw<ArgumentNullException>();
        }

        public void TestPerformanceGetRessource(IRessourceManager managerEn, IRessourceManager managerFr)
        {
            //i will create 100000 ressource string
            var tot = 10000;
            for (var i = 1; i <= tot; i++)
            {
                managerEn.SetRessource(new RessourceString($"Test.Ressource{i}", $"Test value {i}"));
            }

            managerEn.Flush();

            for (var i = 1; i <= tot; i++)
            {
                managerFr.SetRessource(new RessourceString($"Test.Ressource{i}", $"Test value {i} in fr"));
            }

            managerFr.Flush();

            var ressource = managerEn.GetRessource<RessourceString>($"Test.Ressource{tot}");
            Assert.IsTrue(ressource.Id == $"Test.Ressource{tot}");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Value == $"Test value {tot}");

            managerEn.SetRessource(new RessourceString($"Test.Ressource{tot}", $"Test value ok"));

            managerEn.Flush();

            ressource = managerEn.GetRessource<RessourceString>($"Test.Ressource{tot}");
            Assert.IsTrue(ressource.Id == $"Test.Ressource{tot}");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Value == "Test value ok", ressource.Value);


        }

        public void TestRemoveRessource(IRessourceManager manager)
        {
            manager.SetRessource<RessourceString>(new RessourceString("test", "value", CultureInfo.GetCultureInfo(1)));
            manager.RemoveRessource<RessourceString>("test");
            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));

            manager.Flush();

            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));

            manager.SetRessource<RessourceString>(new RessourceString("test", "value", CultureInfo.GetCultureInfo(1)));
            manager.Flush();
            manager.RemoveRessource<RessourceString>("test");
            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));

            manager.Flush();

            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));
        }

        public void TestRemoveRessourceAsync(IRessourceManager manager)
        {
            manager.SetRessource<RessourceString>(new RessourceString("test", "value", CultureInfo.GetCultureInfo(1)));
            var awaitingResult = manager.RemoveRessourceAsync<RessourceString>("test");
            awaitingResult.Wait(100);
            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));

            manager.Flush();

            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));

            manager.SetRessource<RessourceString>(new RessourceString("test", "value", CultureInfo.GetCultureInfo(1)));
            manager.Flush();

            awaitingResult = manager.RemoveRessourceAsync<RessourceString>("test");
            awaitingResult.Wait(100);

            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));

            manager.Flush();

            Assert.IsNull(manager.GetRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceString>("test"));
        }

        public void FailRemoveRessourceAsync(IRessourceManager manager)
        {
            manager.Invoking(x => x.RemoveRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            Func<Task> call = () => manager.RemoveRessourceAsync<RessourceString>(null);
            call.Should().Throw<ArgumentNullException>();
        }
    }
}
