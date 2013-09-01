using Machine.Specifications;
using System;
using System.Collections.Generic;

namespace Praetorium.UnitTests.Utilities
{
    class IsGenericTypeOfSpecs : TypeExtensionSpecs
    {
        It true_when_comparing_closed_generic_type_to_its_open = () =>
            typeof(int?).IsGenericTypeOf(typeof(Nullable<>)).ShouldBeTrue();

        It false_when_comparing_non_generic_type = () =>
            typeof(string).IsGenericTypeOf(typeof(IEnumerable<>)).ShouldBeFalse();

        It false_when_comparing_closed_generic_type_to_wrong_open = () =>
            typeof(int?).IsGenericTypeOf(typeof(IEnumerable<>)).ShouldBeFalse();

        It false_when_comparing_null_type_to_open = () =>
            NullType.IsGenericTypeOf(typeof(Nullable<>)).ShouldBeFalse();

        It false_when_comparing_generic_type_to_null = () =>
            typeof(int?).IsGenericTypeOf(null).ShouldBeFalse();

        It false_when_comparing_null_to_null = () =>
            NullType.IsGenericTypeOf(null).ShouldBeFalse();
    }
}
