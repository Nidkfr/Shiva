using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Shiva.Exceptions
{
    [TestClass]
    public class UTInvalidServiceContainerTypeException: BaseTest
    {
        [ClassInitialize]
        public new static void ClassInit(TestContext context)
        {
            BaseTest.ClassInit(context);
        }

        [TestMethod]
        public void TestIntialize()
        {
            var ex = new InvalidTypeConstructorForServiceContainerException(typeof(string));
            Assert.IsTrue(ex.InvalidType == typeof(string));
            Assert.IsTrue(ex.Message == "System.String is not valide for service container. Constructor need have not parameter.", ex.Message);
        }

        [TestMethod]
        public void FailInitialize()
        {
            Action<Type> ctor = x => new InvalidTypeConstructorForServiceContainerException(x);
            ctor.Invoking(x => x(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
