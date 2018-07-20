using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;

namespace Shiva.Core.IO
{
    [TestClass]   
    public class UTFileSource: BaseTest
    {

        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestInitialize]
        public void Init()
        {
            using (var str = File.CreateText(@".\UTFileSource.test.txt"))
            {
                str.Write("test");
            }
        }

        [TestMethod]
       
        public void TestInitialize()
        {
            using (var src = new FileSource(@".\UTFileSource.test.txt"))
            {
                Assert.IsTrue(src.SaveMode == FileSourceSaveModeEnum.NONE);                
            }

            using (var src = new FileSource(@".\UTFileSource.test.txt", FileSourceSaveModeEnum.KEEPPREVIOUSVERSION))
            {                
                Assert.IsTrue(src.SaveMode == FileSourceSaveModeEnum.KEEPPREVIOUSVERSION);
            }

        }

        [TestMethod]
        public void TestGetStream()
        {
            using (var src = new FileSource(@".\UTFileSource.test.txt"))
            {
                var stream = src.GetStream();

                Assert.IsNotNull(stream);
                Assert.IsTrue(stream == src.GetStream());
            }
        }

        [TestMethod]
        public void FailGetStream()
        {
            using (var src = new FileSource(@".\UTFileSource.test.txt"))
            {
                src.Dispose();
                src.Invoking(x => x.GetStream()).Should().Throw<ObjectDisposedException>();
            }
        }

        [TestMethod]
        public void TestGetSaveStream()
        {
            using (var src = new FileSource(@".\UTFileSource.test.txt"))
            {
                var stream = src.GetSaveStream();

                Assert.IsNotNull(stream);
                Assert.IsTrue(stream == src.GetSaveStream());
            }
        }

        [TestMethod]
        public void FailGetSaveStream()
        {
            var src = new FileSource(@".\UTFileSource.test.txt");
            src.Dispose();
            src.Invoking(x => x.GetStream()).Should().Throw<ObjectDisposedException>();
        }

        [TestMethod]
        public void TestFlush()
        {
            using (var src = new FileSource(@".\UTFileSource.test.txt"))
            {
                var textreader = new StreamReader(src.GetStream());
                Assert.IsTrue(textreader.ReadLine() == "test");
                var textwriter = new StreamWriter(src.GetSaveStream());
                textwriter.Write("test ok");
                textwriter.Flush();
                src.Flush();
                textreader = new StreamReader(src.GetStream());
                Assert.IsTrue(textreader.ReadLine() == "test ok", textreader.ReadLine());
            }
        }
    }
}
