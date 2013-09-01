using Machine.Specifications;
using Praetorium.Configuration;

namespace Praetorium.UnitTests.Configuration
{
    class When_getting_setting_with_throw : ConfigReaderSpecs
    {
        It should_throw_if_value_is_missing = () =>
            Catch.Exception(() => Reader.GetSettingOrThrow("missing")).ShouldNotBeNull();

        It should_throw_if_value_is_empty = () =>
            Catch.Exception(() => Reader.GetSettingOrThrow("empty")).ShouldNotBeNull();

        It should_return_the_value_if_it_exists = () =>
            Reader.GetSettingOrThrow("value").ShouldEqual("value");
    }
}
