using NUnit.Framework;
using Should;
using System.Collections;

namespace Praetorium.Tests
{
    [TestFixture]
    public class CollectionExtensionTests
    {
        [Test]
        public void CanGetCountOfEnumerable()
        {
            var list = new ArrayList()
            {
                "one",
                "two"
            } as IEnumerable;

            list.Count().ShouldEqual(2);
        }

        [Test]
        public void EmptyCollectionIsEmpty()
        {
            var items = new string[] { };

            items.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void NonEmptyColletionIsNotEmpty()
        {
            var items = new string[] { "hello" };

            items.IsEmpty().ShouldBeTrue();
        }
    }
}
