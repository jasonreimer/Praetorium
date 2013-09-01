using Machine.Specifications;
using Praetorium.Configuration;
using Rhino.Mocks;

namespace Praetorium.UnitTests.Configuration
{
    abstract class ConfigReaderSpecs
    {
        protected static IConfigReader Reader;

        Establish context = () =>
        {
            Reader = MockRepository.GenerateMock<IConfigReader>();

            Reader.Expect(x => x.GetSetting("empty"))
                .Return("");

            Reader.Expect(x => x.GetSetting("value"))
                .Return("value");
        };
    }
}
