using Machine.Specifications;

namespace Praetorium.UnitTests.Utilities
{
    class IsNullableOfSpecs : TypeExtensionSpecs
    {
        It should_return_true_for_the_nullable_of_the_type = () =>
            typeof(int?).IsNullableOf(typeof(int)).ShouldBeTrue();

        It should_return_false_for_the_nullable_of_the_type = () =>
            typeof(int?).IsNullableOf(typeof(double)).ShouldBeFalse();

        It should_return_false_for_null = () =>
            NullType.IsNullableOf(typeof(double)).ShouldBeFalse();
    }
}
