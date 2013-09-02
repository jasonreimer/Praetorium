using Machine.Specifications;
using Praetorium.Logging;
using Rhino.Mocks;
using System;

namespace Praetorium.UnitTests.Logging
{
    class When_getting_an_exception_formatter
    {
        static ExceptionFormatterFactory Factory;
        static IExceptionFormatter[] Formatters;

        Establish context = () =>
        {
            Formatters = new[] 
            {
                MockRepository.GenerateMock<IExceptionFormatter>(),
                MockRepository.GenerateMock<IExceptionFormatter>()
            };

            var builders = new[] 
            {
                MockRepository.GenerateMock<IExceptionFormatterBuilder>(),
                MockRepository.GenerateMock<IExceptionFormatterBuilder>()
            };

            builders[0].Expect(x => x.BaseExceptionType)
                .Return(typeof(ArgumentException));

            builders[0].Expect(x => x.Get())
                .Return(Formatters[0]);

            builders[1].Expect(x => x.BaseExceptionType)
                .Return(typeof(InvalidOperationException));

            builders[1].Expect(x => x.Get())
                .Return(Formatters[1]);

            Factory = new ExceptionFormatterFactory(builders);
        };

        It should_return_the_default_formatter_for_exception = () =>
            Factory.Get(typeof(Exception)).ShouldBeOfType<TextExceptionFormatter>();

        It should_return_the_formatter_for_the_base_exception_type = () =>
            Factory.Get(typeof(ArgumentNullException)).ShouldBeTheSameAs(Formatters[0]);

        It should_return_the_formatter_for_the_matching_exception_type = () =>
            Factory.Get(typeof(InvalidOperationException)).ShouldBeTheSameAs(Formatters[1]);
    }
}
