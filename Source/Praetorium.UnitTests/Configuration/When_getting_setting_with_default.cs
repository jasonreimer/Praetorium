using Machine.Specifications;
using Praetorium.Configuration;

namespace Praetorium.UnitTests.Configuration
{
    class When_getting_setting_with_default : ConfigReaderSpecs
    {
        It should_return_default_if_value_is_missing = () =>
            Reader.GetSetting("missing", "default").ShouldEqual("default");

        It should_return_default_if_value_is_empty = () =>
            Reader.GetSetting("empty", "default").ShouldEqual("default");

        It should_return_the_value_if_it_exists = () =>
            Reader.GetSetting("value", "default").ShouldEqual("value");
    }
}