using Machine.Specifications;
using Praetorium.Configuration;
using Rhino.Mocks;

namespace Praetorium.UnitTests.Configuration
{
    class When_getting_a_config_section_with_attribute : ConfigReaderSpecs
    {
        static ITestSection Section;

        Establish context = () =>
            Reader.Expect(x => x.GetSection<ITestSection>())
                  .Return(MockRepository.GenerateMock<ITestSection>());

        Because of = () =>
            Section = Reader.GetSection<ITestSection>();

        It should_get_the_section_by_the_attribute_name = () =>
            Reader.AssertWasCalled(x => x.GetSection<ITestSection>("TestSection"));

        It should_return_the_section = () => Section.ShouldNotBeNull();
    }

    [DefaultSectionName("TestSection")]
    public interface ITestSection { }
}
