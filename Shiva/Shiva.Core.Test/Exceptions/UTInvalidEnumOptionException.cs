using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shiva.Exceptions
{
    [TestClass]
    public class UTInvalidEnumOptionException:BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestInitialize()
        {
            var ex = new InvalidEnumOptionException("test");
            Assert.IsTrue(ex.EnumValue == "test");
            Assert.IsTrue(ex.Message == "You have a new Enum Value test in Switch statement , it's not implemented.", ex.Message);

            ex = new InvalidEnumOptionException("");
            Assert.IsTrue(ex.EnumValue == "");
            Assert.IsTrue(ex.Message == "You have a new Enum Value [Empty] in Switch statement , it's not implemented.", ex.Message);

        }
    }
}
