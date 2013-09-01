using Machine.Specifications;

namespace Praetorium.UnitTests.Utilities
{
    class IsNullableSpecs : TypeExtensionSpecs
    {
        It true_when_nullable_type = () =>
            typeof(int?).IsNullable().ShouldBeTrue();

        It false_when_not_nullable_type = () =>
            typeof(int).IsNullable().ShouldBeFalse();

        It true_when_reference_type = () =>
            typeof(string).IsNullable().ShouldBeTrue();

        It false_when_null = () =>
            NullType.IsNullable().ShouldBeFalse();
    }
}
