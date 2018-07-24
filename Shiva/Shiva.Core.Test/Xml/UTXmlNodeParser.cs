using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using FluentAssertions;

namespace Shiva.Xml
{
    [TestClass]
    public class UTXmlNodeParser:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestWrite()
        {
            var mock = new Mock<XmlNodeParser>();
            mock.Protected().Setup("WriteStartElement", ItExpr.IsAny<XmlWriter>())
                .Callback<XmlWriter>(x =>
                {
                    x.WriteStartElement("test");
                })
                .Verifiable();
            mock.Protected().Setup("WriteChildren", ItExpr.IsAny<XmlWriter>()).Verifiable();
            var parser = mock.Object;
            using (var stream = new MemoryStream())
            {
                parser.Write(XmlWriter.Create(stream));
            }
            mock.Verify();
        }

        [TestMethod]
        public void FailWrite()
        {
            var mock = new Mock<XmlNodeParser>();
            var parser = mock.Object;

            parser.Invoking(x => x.Write(null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            var mock = new Mock<XmlNodeParser>();
            mock.Protected().Setup("WriteStartElement", ItExpr.IsAny<XmlWriter>())
                .Callback<XmlWriter>(x =>
                {
                    x.WriteStartElement("test");
                })
                .Verifiable();
            mock.Protected().Setup("UpdateChildren", ItExpr.IsAny<XmlReader>(), ItExpr.IsAny<XmlWriter>()).Verifiable();
            var parser = mock.Object;
            using (var stream = new MemoryStream())
            {
                using (var rstream = new MemoryStream(System.Text.ASCIIEncoding.UTF8.GetBytes("<test><test><test/></test>")))
                {
                    parser.Update(XmlReader.Create(rstream), XmlWriter.Create(stream));
                }
            }
            mock.Verify();
        }

        [TestMethod]
        public void FailUpdate()
        {
            var mock = new Mock<XmlNodeParser>();
            var parser = mock.Object;

            parser.Invoking(x => x.Update(null, XmlWriter.Create(new MemoryStream()))).Should().Throw<ArgumentNullException>();
            parser.Invoking(x => x.Update(XmlReader.Create(new MemoryStream()), null)).Should().Throw<ArgumentNullException>();
        }
    }
}
