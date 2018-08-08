using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using System.Globalization;
using Shiva.Core.Identities;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;
using Shiva.Tools;

namespace Shiva.Ressources
{
    [TestClass]
    public class UTRessourceString : BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialize()
        {
            var ressource = new RessourceString();
            Assert.IsTrue(string.IsNullOrWhiteSpace(ressource.Value));
            Assert.IsFalse(ressource.HasValue);

            ressource = new RessourceString("test", "value", CultureInfo.GetCultureInfo(1));
            Assert.IsTrue(ressource.Value == "value");
            Assert.IsTrue(ressource.HasValue);

            ressource = new RessourceString("test", "", CultureInfo.GetCultureInfo(1));
            Assert.IsTrue(ressource.Value == "[Shiva.Ressources.RessourceString]{test:ar}", ressource.Value);
            Assert.IsFalse(ressource.HasValue);

        }

        [TestMethod]
        public void TestClone()
        {
            var ressource = new RessourceString("test", "value", CultureInfo.GetCultureInfo(1));
            var ressource2 = ressource.Clone() as IRessource<string>;

            Assert.IsFalse(ressource == ressource2);
            Assert.IsTrue(ressource.Id == ressource2.Id);
            Assert.IsTrue(ressource.Culture == ressource2.Culture);
            Assert.IsTrue(ressource.Value == ressource2.Value);
        }

        [TestMethod]
        public void TestSerialize()
        {
            var ressource = new RessourceString("test", "value", CultureInfo.GetCultureInfo(1));
            using (var stream = new MemoryStream())
            {
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    ressource.Serialize(writer,null);
                }
                var settingsString = Encoding.UTF8.GetString(stream.ToArray()).Trim();
                settingsString = settingsString.RemoveByteOrderMarkUtf8();
                var xdoc = XDocument.Parse(settingsString);
                Assert.IsTrue(xdoc.Root.Name == "Value");
                Assert.IsTrue(xdoc.Root.Value == "value");
            }
        }

        [TestMethod]
        public void FailSerialize()
        {
            var ressource = new RessourceString("test", "value", CultureInfo.GetCultureInfo(1));
            ressource.Invoking(x => x.Serialize(null,null)).Should().Throw<ArgumentNullException>();

        }

        [TestMethod]
        public void TestUnSerialize()
        {
            var ressource = new RessourceString("test", "value", CultureInfo.GetCultureInfo(1));
            using (var stream = new MemoryStream())
            {
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    ressource.Serialize(writer,null);
                }
                var settingsString = Encoding.UTF8.GetString(stream.ToArray()).Trim();
                settingsString = settingsString.RemoveByteOrderMarkUtf8();
                var xdoc = XDocument.Parse(settingsString);
                var newressource = new RessourceString();
                using (var reader = xdoc.Root.CreateReader())
                {
                    newressource.UnSerialize(reader,null);
                }
                Assert.IsTrue(newressource.Value == "value");
            }
        }

        [TestMethod]
        public void FailUnserialize()
        {
            var ressource = new RessourceString("test", "value", CultureInfo.GetCultureInfo(1));
            ressource.Invoking(x => x.UnSerialize(null, null)).Should().Throw<ArgumentNullException>();

            var xdoc = XDocument.Parse(@"<test></test>");
            using (var reader = xdoc.CreateReader())
            {
                ressource.Invoking(x => x.UnSerialize(reader,null))
                     .Should()
                     .Throw<InvalidOperationException>();
            }
        }

    }
}
