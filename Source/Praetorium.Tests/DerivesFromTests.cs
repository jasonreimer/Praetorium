using NUnit.Framework;
using Should;
using System.Collections;
using System.Collections.Generic;

namespace Praetorium.Tests.Utilities
{
    /// <summary>
    /// Summary description for DerivesFromTests
    /// </summary>
    [TestFixture]
    public class DerivesFromTests
    {
        [Test]
        public void DerivesFrom()
        {
            typeof(IEnumerable<string>).DerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();
            typeof(IEnumerable<string>).DerivesFrom<IEnumerable>().ShouldBeTrue();
            typeof(IEnumerable<>).DerivesFrom(typeof(IEnumerable<>)).ShouldBeFalse();
            typeof(IEnumerable<>).IsOrDerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();
            typeof(IEnumerable<>).DerivesFrom(typeof(IEnumerable)).ShouldBeTrue();
            typeof(string).DerivesFrom<object>().ShouldBeTrue();
            typeof(string).DerivesFrom(typeof(object)).ShouldBeTrue();
            typeof(string).IsOrDerivesFrom(typeof(string)).ShouldBeTrue();
            typeof(string).IsOrDerivesFrom<string>().ShouldBeTrue();
            typeof(Dictionary<string, string>).IsOrDerivesFrom(typeof(Dictionary<,>)).ShouldBeTrue();
            typeof(Dictionary<string, string>).DerivesFrom(typeof(Dictionary<,>)).ShouldBeTrue();
            typeof(Dictionary<string, string>).IsOrDerivesFrom(typeof(Dictionary<string, string>)).ShouldBeTrue();
            typeof(Dictionary<string, string>).DerivesFrom<IEnumerable>().ShouldBeTrue();
            typeof(List<string>).DerivesFrom(typeof(IEnumerable<>)).ShouldBeTrue();
            typeof(Dictionary<string, string>).DerivesFrom(typeof(IDictionary<,>)).ShouldBeTrue();
            typeof(SomeThing).DerivesFrom<ISomeThing>().ShouldBeTrue();
            typeof(SomeThing).DerivesFrom<ISomeThing<string>>().ShouldBeTrue();
            typeof(SomeThing).DerivesFrom(typeof(ISomeThing<>)).ShouldBeTrue();
            typeof(ISomeThing<>).IsOrDerivesFrom(typeof(ISomeThing<>)).ShouldBeTrue();
            typeof(ISomeThing<>).DerivesFrom<ISomeThing>().ShouldBeTrue();
            typeof(Dictionary<,>).DerivesFrom(typeof(IDictionary<,>)).ShouldBeTrue();
            typeof(AnotherSomething<>).DerivesFrom(typeof(AnotherSomethingBase<>)).ShouldBeTrue();
            typeof(ISomeThing<string>).DerivesFrom(typeof(ISomeThing<>)).ShouldBeTrue();
            typeof(int).IsOrDerivesFrom<string>().ShouldBeFalse();
            typeof(string).DerivesFrom<string>().ShouldBeFalse();
            typeof(Dictionary<string, string>).DerivesFrom(typeof(Dictionary<string, string>)).ShouldBeFalse();

            // sanity checks
            typeof(string).IsSubclassOf<IEnumerable<char>>().ShouldBeFalse();
        }

        [Test]
        public void InterfaceDerviesFromBaseInterface()
        {
            Assert.IsTrue(typeof(ICollection).DerivesFrom<IEnumerable>());
        }

        [Test]
        public void InterfaceDerviesTypedGenericInterface()
        {
            Assert.IsTrue(typeof(IOneThing).DerivesFrom<ISomeThing<object>>());
        }

        [Test]
        public void InterfaceDerviesUntypedGenericInterface()
        {
            Assert.IsTrue(typeof(IDictionary<string, string>).DerivesFrom(typeof(IDictionary<,>)));
            Assert.IsTrue(typeof(IOneThing).DerivesFrom(typeof(ISomeThing<>)));
        }

        [Test]
        public void StructDerivesFromInterface()
        {
            Assert.IsTrue(typeof(StructThing).DerivesFrom<ISomeThing>());
        }

        [Test]
        public void StructDerivesFromTypedGenericInterface()
        {
            Assert.IsTrue(typeof(StructThing).DerivesFrom(typeof(ISomeThing<object>)));
        }

        [Test]
        public void StructDerivesFromUntypedGenericInterface()
        {
            Assert.IsTrue(typeof(StructThing).DerivesFrom(typeof(ISomeThing<>)));
        }


        [Test]
        public void ClassDerivesFromInterface()
        {
            Assert.IsTrue(typeof(SomeThing).DerivesFrom<ISomeThing>());
        }

        [Test]
        public void ClassDerivesFromTypedGenericInterface()
        {
            Assert.IsTrue(typeof(SomeThing).DerivesFrom(typeof(ISomeThing<string>)));
        }

        [Test]
        public void ClassDerivesFromUntypedGenericInterface()
        {
            Assert.IsTrue(typeof(SomeThing).DerivesFrom(typeof(ISomeThing<>)));
        }

        [Test]
        public void StringDerivesFromObject()
        {
            Assert.IsTrue(typeof(string).DerivesFrom<object>());
        }

        [Test]
        public void ClassDoesNotDeriveFromSelf()
        {
            Assert.IsFalse(typeof(SomeThing).DerivesFrom<SomeThing>());
        }

        [Test]
        public void ClassDerivesFromTypedGenericClass()
        {
            Assert.IsTrue(typeof(SomeOtherThing).DerivesFrom<AnotherSomethingBase<string>>());
        }

        [Test]
        public void ClassDoesNotDeriveFromDictionary()
        {
            Assert.IsFalse(typeof(SomeOtherThing).DerivesFrom<Dictionary<string, string>>());
        }

        public struct StructThing : IOneThing
        {
        }

        public interface IOneThing : ISomeThing<object>
        {
        }

        public interface ISomeThing { }

        public interface ISomeThing<T> : ISomeThing { }

        public class SomeThing : ISomeThing<string> { }

        public class AnotherSomethingBase<T> { }

        public class AnotherSomething<T> : AnotherSomethingBase<T> { }

        public class SomeOtherThing : AnotherSomething<string> { }
    }
}
