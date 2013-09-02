using Machine.Specifications;
using Praetorium.Logging;
using System;

namespace Praetorium.UnitTests
{
    class When_comparing_exception_types
    {
        static ExceptionTypeComparer Comparer = new ExceptionTypeComparer();

        It should_indicate_a_derived_is_greater_than_its_base_type = () =>
            Comparer.Compare(typeof(ArgumentNullException), typeof(ArgumentException)).ShouldBeGreaterThan(0);

        It should_indicate_a_base_class_is_less_than_a_derived_class = () =>
            Comparer.Compare(typeof(Exception), typeof(ArgumentException)).ShouldBeLessThan(0);

        It should_return_zero_for_unrelated_types = () =>
            Comparer.Compare(typeof(ArgumentException), typeof(InvalidOperationException)).ShouldEqual(0);

        It should_return_zero_for_same = () =>
            Comparer.Compare(typeof(ArgumentException), typeof(ArgumentException)).ShouldEqual(0);
    }
}
