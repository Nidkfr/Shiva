using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Shiva.Xml
{
    [TestClass]
    public class UTXmlParserTool:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestWriteToEndElement()
        {
            var xdoc = XDocument.Parse("<!-- test --><test><child></child></test>");
            using (var stream = new MemoryStream())
            {
                var writer = XmlWriter.Create(stream);
                XmlBuilderTool.WriteToEndElement(xdoc.CreateReader(),writer, "child");
                writer.Flush();
                var writting = System.Text.ASCIIEncoding.UTF8.GetString(stream.ToArray());
                Assert.IsTrue(writting == "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?><!-- test --><test><child", writting);
            }
        }

        [TestMethod]
        public void TestReadAndWriteToNextStartOrEndElement()
        {
            var xdoc = XDocument.Parse("<!-- test --><test><child></child></test>");
            using (var stream = new MemoryStream())
            {
                var writer = XmlWriter.Create(stream);
                var reader = xdoc.CreateReader();
                XmlBuilderTool.ReadAndWriteToNextStartOrEndElement(reader, writer);
                writer.Flush();
                var writting = System.Text.ASCIIEncoding.UTF8.GetString(stream.ToArray());
                Assert.IsTrue(writting == "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?><!-- test -->", writting);
                Assert.IsTrue(reader.NodeType == XmlNodeType.Element);
                Assert.IsTrue(reader.LocalName == "test");
            }
        }

        [TestMethod]
        public void TestReadToEndOfElement()
        {
            var xdoc = XDocument.Parse("<!-- test --><test><child></child></test>");
                var reader = xdoc.CreateReader();
                XmlBuilderTool.ReadToEndOfElement(reader,"child");                                
                Assert.IsTrue(reader.NodeType == XmlNodeType.EndElement);
                Assert.IsTrue(reader.LocalName == "child");
            
        }
        
    }
}
