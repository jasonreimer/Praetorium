using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace Praetorium.UnitTests.Utilities
{
    public class CollectionExtensionSpecs
    {
        public static IEnumerable<object[]> IsEmptyTestParameters
        {
            get
            {
                return new[] 
                {
                    new object[] { null, true },
                    new object[] { new string[0], true },
                    new object[] { new [] { "1" }, false}
                };
            }
        }

        public static IEnumerable<object[]> IsNotEmptyTestParameters
        {
            get
            {
                return new[] 
                {
                    new object[] { null, false },
                    new object[] { new string[0], false },
                    new object[] { new [] { "1" }, true }
                };
            }
        }

        public static IEnumerable<object[]> NoneTestParameters
        {
            get
            {
                return new[] 
                {
                    new object[] { null, true },
                    new object[] { new string[0], true },
                    new object[] { new [] { "1" }, false },
                    new object[] { new [] { "2" }, true }
                };
            }
        }

        [Fact]
        public void ForEach_should_not_throw_for_null_collection()
        {
            IEnumerable<string> values = null;

            values.ForEach(x => { });
        }

        [Fact]
        public void ForEach_should_enumerate_the_collection()
        {
            int sum = 0;
            var values = new[] { 1, 1, 1 };

            values.ForEach(x => sum += x);

            sum.Should().Be(3);
        }

        [Theory]
        [PropertyData("NoneTestParameters")]
        public void None_should_return_the_expected_value_for_the_given_collection(IEnumerable<string> collection, bool expectedReturn)
        {
            collection.None(x => x == "1").Should().Be(expectedReturn);
        }

        [Theory]
        [PropertyData("IsEmptyTestParameters")]
        public void IsEmpty_should_return_the_expected_value_for_the_given_collection(IEnumerable<string> collection, bool expectedReturn)
        {
            collection.IsEmpty().Should().Be(expectedReturn);
        }

        [Theory]
        [PropertyData("IsNotEmptyTestParameters")]
        public void IsNotEmpty_should_return_the_expected_value_for_the_given_collection(IEnumerable<string> collection, bool expectedReturn)
        {
            collection.IsNotEmpty().Should().Be(expectedReturn);
        }
    }
}
