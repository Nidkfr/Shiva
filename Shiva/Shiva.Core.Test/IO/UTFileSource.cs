using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shiva.IO
{
    [TestClass]
    [DeploymentItem("DeployItems", "DeployItems")]
    public class UTFileSource
    {
        [TestMethod]
        public void TestInitializer()
        {
            var file = new FileSource("./DeployItems/XmlSample.xml");
            Assert.IsFalse(file.IsOpen);
            Assert.IsFalse(file.IsReadOnly);            
        }

        [TestMethod]
        public void TestOpen()
        {
            var file = new FileSource("./DeployItems/XmlSample.xml");
            Assert.IsFalse(file.IsOpen);
            Assert.IsFalse(file.IsReadOnly);

            file.Open();

            Assert.IsTrue(file.IsOpen);
            Assert.IsFalse(file.IsReadOnly);
        }

        [TestMethod]
        public void TestClose()
        {
            var file = new FileSource("./DeployItems/XmlSample.xml");
            file.Open();

            Assert.IsTrue(file.IsOpen);
            Assert.IsFalse(file.IsReadOnly);

            file.Close();

            Assert.IsFalse(file.IsOpen);

            file.Open();
            Assert.IsTrue(file.IsOpen);
        }

        [TestMethod]
        public void TestDispose()
        {
            FileSource file = null;
            using (file = new FileSource("./DeployItems/XmlSample.xml"))
            {
                Assert.IsTrue(file.Stream.CanRead);
                Assert.IsTrue(file.Stream.CanWrite);
            }
            Assert.IsFalse(file.IsOpen);
        }
    }
}
