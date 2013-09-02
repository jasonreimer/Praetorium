using Machine.Specifications;
using Praetorium.Logging;
using Rhino.Mocks;
using System;

namespace Praetorium.UnitTests.Logging
{
    class When_formatting_an_exception
    {
        static IExceptionFormatterFactory FormatterFactory;
        static Exception Exception;
        static TextExceptionFormatter Formatter;
        static string Message;

        Establish context = () =>
        {
            FormatterFactory = MockRepository.GenerateMock<IExceptionFormatterFactory>();
            Exception = new Exception("wrapped", Catch.Exception(() => { throw new ArgumentException(); }));
            Exception.Data.Add("var1", "value");

            Formatter = new TextExceptionFormatter(FormatterFactory);

            FormatterFactory.Expect(x => x.Get(null))
                .IgnoreArguments()
                .Return(Formatter);
        };

        Because of = () => Message = Formatter.Format(Exception);

        It should_return_a_message = () => Message.ShouldNotBeEmpty();
    }
}
