using FluentAssertions;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Extensions;

namespace Praetorium.UnitTests.Extensions
{
    public class TypeExtensionsSpecs
    {
        public static IEnumerable<object[]> Properties
        {
            get
            {
                return new[] 
                {
                    new object[] { typeof(Dictionary<,>).GetProperty("Item"), true },
                    new object[] { typeof(Dictionary<,>).GetProperty("Count"), false }
                };
            }
        }

        [Theory]
        [PropertyData("Properties")]
        public void IsIndexer_should_return_the_expected_result(PropertyInfo property, bool expectedResult)
        {
            var props = typeof(Dictionary<,>).GetProperties();
            property.IsIndexer().Should().Be(expectedResult);
        }
    }
}
