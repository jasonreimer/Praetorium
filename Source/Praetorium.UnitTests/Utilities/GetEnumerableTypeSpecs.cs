using Machine.Specifications;
using System;
using System.Collections.Generic;

namespace Praetorium.UnitTests.Utilities
{
    class GetEnumerableTypeSpecs : TypeExtensionSpecs
    {
        It should_return_the_closing_type_parameter_for_closed_interface = () =>
            typeof(IEnumerable<string>).GetEnumerableType().ShouldEqual(typeof(string));

        It should_return_the_closing_type_parameter_for_closed_class = () =>
            typeof(string).GetEnumerableType().ShouldEqual(typeof(char));

        It should_throw_for_open_type = () =>
            Catch.Exception(() => typeof(IEnumerable<>).GetEnumerableType())
                .ShouldBeOfType<ArgumentException>();

        It should_throw_for_non_enumerable_type = () =>
            Catch.Exception(() => typeof(int).GetEnumerableType())
                .ShouldBeOfType<ArgumentException>();

        It should_return_null_for_null_type = () =>
            Catch.Exception(() => NullType.GetEnumerableType())
                .ShouldBeOfType<ArgumentNullException>();
    }
}
