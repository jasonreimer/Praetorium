using Machine.Specifications;

namespace Praetorium.UnitTests.Utilities
{
    class IsNullableTypeSpecs
    {
        It true_when_checking_nullable_type = () =>
            typeof(int?).IsNullableType().ShouldBeTrue();

        It false_when_checking_non_nullable_type = () =>
            typeof(int).IsNullableType().ShouldBeFalse();

        It false_when_checking_reference_type = () =>
            typeof(string).IsNullableType().ShouldBeFalse();
    }
}
