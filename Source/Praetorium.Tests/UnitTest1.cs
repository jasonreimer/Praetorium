using NUnit.Framework;
using Should;
using System;

namespace Praetorium.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            // truth checks
            typeof(StringHandler).IsSubclassOf(typeof(IHandler<>)).ShouldBeFalse();

            typeof(StringHandler).IsOrDerivesFrom(typeof(IHandler<string>)).ShouldBeTrue();
            typeof(StringHandler).Closes(typeof(IHandler<string>)).ShouldBeFalse();
            typeof(StringHandler).Closes(typeof(IHandler<>)).ShouldBeTrue();
            typeof(IHandler<string>).Closes(typeof(IHandler<>)).ShouldBeTrue();
            typeof(StringHandler).DerivesFrom(typeof(IHandler<>)).ShouldBeTrue();
            typeof(IStringHandler).DerivesFrom(typeof(IHandler<>)).ShouldBeTrue();
            typeof(int?).DerivesFrom(typeof(Nullable<>)).ShouldBeTrue();
            typeof(int?).IsNullableType().ShouldBeTrue();
            typeof(int?).IsNullableOf(typeof(int)).ShouldBeTrue();
            typeof(int?).IsNullableOf<int>().ShouldBeTrue();
        }
    }

    public interface IHandler<T>
    {
        void Handle(T message);
    }

    public interface IStringHandler : IHandler<string> { }

    public class StringHandler : IHandler<string>
    {
        public void Handle(string message)
        {
        }
    }

}
