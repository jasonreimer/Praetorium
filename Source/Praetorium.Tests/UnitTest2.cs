using NUnit.Framework;
using Should;
using System.Collections.Generic;

namespace Praetorium.Tests
{
    [TestFixture]
    public class TypeExtensionTests
    {
        [Test]
        public void TestMethod1()
        {
            typeof(IEnumerable<string>).Closes(typeof(IEnumerable<>)).ShouldBeTrue();
            typeof(IEnumerable<string>).GetEnumerableType().ShouldEqual(typeof(string));
        }
    }
}
