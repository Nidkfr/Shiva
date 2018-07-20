using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.IO;
using System.Xml;

namespace Shiva.Xml
{
    [TestClass]
    public class UTXmlParser
    {
        [TestMethod]
        public void TestWrite()
        {
            var mock = new Mock<XmlParser>();
            mock.Protected().Setup("WriteStartRoot", ItExpr.IsAny<XmlWriter>())
                .Callback<XmlWriter>(x=>
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
            var mock = new Mock<XmlParser>();
            var parser = mock.Object;

            parser.Invoking(x => x.Write(null)).Should().Throw<ArgumentNullException>();            
        }

        [TestMethod]
        public void TestUpdate()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void FailUpdate()
        {
            var mock = new Mock<XmlParser>();
            var parser = mock.Object;

            parser.Invoking(x => x.Update(null,new MemoryStream())).Should().Throw<ArgumentNullException>();
            parser.Invoking(x => x.Update(new MemoryStream(),null )).Should().Throw<ArgumentNullException>();
        }
    }
}
