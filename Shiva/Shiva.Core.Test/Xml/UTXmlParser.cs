using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Shiva.Xml
{
    [TestClass]
    public class UTXmlParser:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestWrite()
        {
            var mock = new Mock<XmlBuilder>();
            mock.Protected().Setup("WriteStartRoot", ItExpr.IsAny<XmlWriter>())
                .Callback<XmlWriter>(x =>
                {
                    x.WriteStartElement("test");
                })
                .Verifiable();
            mock.Protected().Setup("WriteChildren", ItExpr.IsAny<XmlWriter>()).Verifiable();
            var parser = mock.Object;
            using (var stream = new MemoryStream())
            {
                parser.Write(stream);
            }
            mock.Verify();
        }

        [TestMethod]
        public void FailWrite()
        {
            var mock = new Mock<XmlBuilder>();
            var parser = mock.Object;

            parser.Invoking(x => x.Write(null)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void TestUpdate()
        {
            var mock = new Mock<XmlBuilder>();
            mock.Protected().Setup("WriteStartRoot", ItExpr.IsAny<XmlWriter>())
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
                    parser.Update(rstream, stream);
                }
            }
            mock.Verify();
        }

        [TestMethod]
        public void FailUpdate()
        {
            var mock = new Mock<XmlBuilder>();
            var parser = mock.Object;

            parser.Invoking(x => x.Update(null, new MemoryStream())).Should().Throw<ArgumentNullException>();
            parser.Invoking(x => x.Update(new MemoryStream(), null)).Should().Throw<ArgumentNullException>();
        }
    }
}
