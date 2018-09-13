using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Shiva.Core.Identities;

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

        void TestGroup();       

        void FailGroup();        
        
        void TestPerformanceGroup();
    }

    public class RessourceServiceTester
    {
        public void TestGetSetRessource(IRessourceService manager)
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

        public void TestGetSetRessourceAsync(IRessourceService manager)
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

        public void FailGetSetRessource(IRessourceService manager)
        {
            manager.Invoking(x => x.GetRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            Func<Task> call = () => manager.GetRessourceAsync<RessourceString>(null);
            call.Should().Throw<ArgumentNullException>();

            manager.Invoking(x => x.SetRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            Func<Task> call2 = () => manager.SetRessourceAsync<RessourceString>(null);
            call2.Should().Throw<ArgumentNullException>();
            
        }

        public void TestContainsRessource(IRessourceService manager)
        {
            manager.SetRessource(new RessourceString("test", "value", CultureInfo.GetCultureInfo(1)));
            Assert.IsTrue(manager.ContainsRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceBinary>("test"));
            manager.Flush();
            Assert.IsTrue(manager.ContainsRessource<RessourceString>("test"));
            Assert.IsFalse(manager.ContainsRessource<RessourceBinary>("test"));
        }

        public void TestContainsRessourceAsync(IRessourceService manager)
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

        public void FailContainsRessource(IRessourceService manager)
        {
            manager.Invoking(x => x.ContainsRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            var response = manager.ContainsRessourceAsync<RessourceString>(null);
            Func<Task> call = () => manager.ContainsRessourceAsync<RessourceString>(null);
            call.Should().Throw<ArgumentNullException>();            
        }

        public void TestPerformanceGetRessource(IRessourceService managerEn, IRessourceService managerFr)
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

            ressource = managerEn.GetRessource<RessourceString>($"Test.Ressource1");
            Assert.IsTrue(ressource.Id == $"Test.Ressource1");
            Assert.IsTrue(ressource.Culture == CultureInfo.GetCultureInfo("en"));
            Assert.IsTrue(ressource.Value == "Test value 1", ressource.Value);


        }

        public void TestRemoveRessource(IRessourceService manager)
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

        public void TestRemoveRessourceAsync(IRessourceService manager)
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

        public void FailRemoveRessourceAsync(IRessourceService manager)
        {
            manager.Invoking(x => x.RemoveRessource<RessourceString>(null)).Should().Throw<ArgumentNullException>();
            Func<Task> call = () => manager.RemoveRessourceAsync<RessourceString>(null);
            call.Should().Throw<ArgumentNullException>();
        }

        public void TestGroup(IRessourceService manager)
        {
            var r1 = new RessourceString("Test.test1", "value1");
            var r2 = new RessourceString("Test2.test2", "value2");
            var r3 = new RessourceString("Test.child.test3", "value3");

            manager.SetRessource<RessourceString>(r1);
            manager.SetRessource<RessourceString>(r2);
            manager.SetRessource<RessourceString>(r3);

            manager.AttachRessourceToGroup<RessourceString>(r1, "grp1");
            manager.AttachRessourceToGroup<RessourceString>(r2, "grp2");
            manager.AttachRessourceToGroup<RessourceString>(r3, "grp2");

            Assert.IsTrue(manager.GetAllGroups().Count() == 2, manager.GetAllGroups().Count().ToString());

            var grp1 = manager.GetGroupRessources<RessourceString>((Identity)"grp1");
            Assert.IsTrue(grp1.Culture == manager.Culture);
            Assert.IsTrue(grp1.Id == "grp1");
            Assert.IsTrue(grp1.Ressources.Count == 1);
            Assert.IsTrue(grp1.Ressources.First().Id == "Test.test1");
            Assert.IsTrue(grp1.Ressources.First().Value == "value1");

            var grp2 = manager.GetGroupRessources<RessourceString>((Identity)"grp2");
            Assert.IsTrue(grp2.Culture == manager.Culture);
            Assert.IsTrue(grp2.Id == "grp2");
            Assert.IsTrue(grp2.Ressources.Count == 2);
            Assert.IsTrue(grp2.Ressources.First().Id == "Test2.test2");
            Assert.IsTrue(grp2.Ressources.First().Value == "value2");
            Assert.IsTrue(grp2.Ressources.Last().Id == "Test.child.test3");
            Assert.IsTrue(grp2.Ressources.Last().Value == "value3");

            manager.Flush();

            Assert.IsTrue(manager.GetAllGroups().Count() == 2, manager.GetAllGroups().Count().ToString());

            var r4 = new RessourceString("Test.test2", "value2");
            manager.SetRessource<RessourceString>(r4);
            manager.AttachRessourceToGroup<RessourceString>(r1, "grp4");

            Assert.IsTrue(manager.GetAllGroups().Count() == 3);

            manager.DetachRessourceToGroup<RessourceString>(r3, "grp2");

            grp2 = manager.GetGroupRessources<RessourceString>((Identity)"grp2");
            Assert.IsTrue(grp2.Culture == manager.Culture);
            Assert.IsTrue(grp2.Id == "grp2");
            Assert.IsTrue(grp2.Ressources.Count == 1);
            Assert.IsTrue(grp2.Ressources.First().Id == "Test2.test2");
            Assert.IsTrue(grp2.Ressources.First().Value == "value2");

            manager.AttachRessourceToGroup<RessourceString>(r1, "grp2");

            manager.Flush();

            grp2 = manager.GetGroupRessources<RessourceString>((Identity)"grp2");
            Assert.IsTrue(grp2.Culture == manager.Culture);
            Assert.IsTrue(grp2.Id == "grp2");
            Assert.IsTrue(grp2.Ressources.Count == 2);
            

            var grpnamespace = manager.GetGroupRessources<RessourceString>((Namespace)"Test",false);
            Assert.IsTrue(grpnamespace.Culture == manager.Culture);
            Assert.IsTrue(grpnamespace.Id == "Test");
            Assert.IsTrue(grpnamespace.Ressources.Count == 2);
            Assert.IsTrue(grpnamespace.Ressources.First().Id == "Test.test1");
            Assert.IsTrue(grpnamespace.Ressources.First().Value == "value1");

            grpnamespace = manager.GetGroupRessources<RessourceString>((Namespace)"Test", true);
            Assert.IsTrue(grpnamespace.Culture == manager.Culture);
            Assert.IsTrue(grpnamespace.Id == "Test");
            Assert.IsTrue(grpnamespace.Ressources.Count == 3);
            Assert.IsTrue(grpnamespace.Ressources.First().Id == "Test.test1");
            Assert.IsTrue(grpnamespace.Ressources.First().Value == "value1");

        }

        public void FailGroup(IRessourceService manager)
        {
            var r1 = new RessourceString("test.fail", "value1");
            manager.Invoking(x => x.AttachRessourceToGroup<RessourceString>(null, "test")).Should().Throw<ArgumentNullException>();
            manager.Invoking(x => x.AttachRessourceToGroup<RessourceString>(new RessourceString(), "test")).Should().Throw<InvalidOperationException>();
            manager.Invoking(x => x.AttachRessourceToGroup<RessourceString>(r1, null)).Should().Throw<ArgumentNullException>();
            manager.Invoking(x => x.DetachRessourceToGroup<RessourceString>(null, "test")).Should().Throw<ArgumentNullException>();
            manager.Invoking(x => x.DetachRessourceToGroup<RessourceString>(new RessourceString(), "test")).Should().Throw<InvalidOperationException>();
            manager.Invoking(x => x.DetachRessourceToGroup<RessourceString>(r1, null)).Should().Throw<ArgumentNullException>();
            manager.Invoking(x => x.GetGroupRessources<RessourceString>((Identity)null)).Should().Throw<ArgumentNullException>();
            manager.Invoking(x => x.GetGroupRessources<RessourceString>((Namespace)null,true)).Should().Throw<ArgumentNullException>();
            manager.Invoking(x => x.RemoveGroup((Identity)null)).Should().Throw<ArgumentNullException>();
            manager.Invoking(x => x.AttachRessourceToGroup(r1, "test")).Should().Throw<InvalidOperationException>();
        }

        public void TestPerformanceGroup(IRessourceService manager)
        {
            //i will create 100000 ressource string
            var tot = 10000;
            for (var i = 1; i <= tot; i++)
            {
                var r = new RessourceString($"Test.Ressource{i}", $"Test value {i}");
                manager.SetRessource(r);
                if(i%100 == 0)
                {
                    manager.AttachRessourceToGroup(r, "Test");
                }
            }

            manager.Flush();

            var grp = manager.GetGroupRessources<RessourceString>("Test");
            Assert.IsTrue(grp.Ressources.Count == 100, grp.Ressources.Count.ToString());
        }
    }
}
