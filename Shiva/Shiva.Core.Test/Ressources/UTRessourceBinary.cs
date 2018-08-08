using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shiva.Tools;
using FluentAssertions;


namespace Shiva.Ressources
{
    [TestClass]
    public class UTRessourceBinary:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialize()
        {
            var ressource = new RessourceBinary();
            Assert.IsNull(ressource.Value);
            Assert.IsFalse(ressource.HasValue);

            ressource = new RessourceBinary("test", Encoding.ASCII.GetBytes("value"), CultureInfo.GetCultureInfo(1));
            Assert.IsTrue(ressource.Value.Length > 0);
            Assert.IsTrue(ressource.HasValue);
        }

        [TestMethod]
        public void TestClone()
        {
            var ressource = new RessourceBinary("test", Encoding.ASCII.GetBytes("value"), CultureInfo.GetCultureInfo(1));
            var ressource2 = ressource.Clone() as IRessource<byte[]>;

            Assert.IsFalse(ressource == ressource2);
            Assert.IsTrue(ressource.Id == ressource2.Id);
            Assert.IsTrue(ressource.Culture == ressource2.Culture);
            Assert.IsTrue(ressource.Value.Length == ressource2.Value.Length);
            Assert.IsTrue(ressource.Value[0] == ressource2.Value[0]);
        }

        [TestMethod]
        public void TestSerialize()
        {
            var ressource = new RessourceBinary("test", Encoding.ASCII.GetBytes("value"), CultureInfo.GetCultureInfo(1));
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
                    ressource.Serialize(writer,new Shiva.Xml.XmlContext("",""));
                }
                var settingsString = Encoding.UTF8.GetString(stream.ToArray()).Trim();
                settingsString = settingsString.RemoveByteOrderMarkUtf8();
                var xdoc = XDocument.Parse(settingsString);
                Assert.IsTrue(xdoc.Root.Name == "Value");
                Assert.IsTrue(xdoc.Root.Value == "dmFsdWU=", xdoc.Root.Value);
            }
        }

        [TestMethod]
        public void FailSerialize()
        {
            var ressource = new RessourceBinary("test", Encoding.ASCII.GetBytes("value"), CultureInfo.GetCultureInfo(1));
            ressource.Invoking(x => x.Serialize(null,new Shiva.Xml.XmlContext("",""))).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestUnSerialize()
        {
            var ressource = new RessourceBinary("test", Encoding.ASCII.GetBytes("value"), CultureInfo.GetCultureInfo(1));
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
                var newressource = new RessourceBinary();
                using (var reader = xdoc.Root.CreateReader())
                {
                    newressource.UnSerialize(reader,null);
                }
                Assert.IsTrue(System.Text.Encoding.ASCII.GetString(newressource.Value) == "value", Convert.ToBase64String(newressource.Value));
            }
        }

        [TestMethod]
        public void FailUnserialize()
        {
            var ressource = new RessourceBinary("test", Encoding.ASCII.GetBytes("value"), CultureInfo.GetCultureInfo(1));
            ressource.Invoking(x => x.UnSerialize(null, null)).Should().Throw<ArgumentNullException>();

            var xdoc = XDocument.Parse(@"<test/>");
            using (var reader = xdoc.CreateReader())
            {
                ressource.Invoking(x => x.UnSerialize(reader,null))
                     .Should()
                     .Throw<InvalidOperationException>();
            }
        }
    }
}
