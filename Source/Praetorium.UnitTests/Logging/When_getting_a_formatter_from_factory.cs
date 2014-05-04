using Machine.Specifications;
using Praetorium.Logging;
using System;
using System.Data.SqlClient;
using System.ServiceModel;

namespace Praetorium.UnitTests.Logging
{
    class When_getting_a_formatter_from_factory
    {
        static ExceptionFormatterFactory Factory;
        
        Establish context = () =>
        {
            var builders = new IExceptionFormatterBuilder[] 
            {
                new ExceptionFormatterBuilder<FaultException,FaultExceptionFormatter>(factory => new FaultExceptionFormatter(factory)),
                new ExceptionFormatterBuilder<SqlException,SqlExceptionFormatter>(factory => new SqlExceptionFormatter(factory))
            };

            Factory = new ExceptionFormatterFactory(builders);
        };

        It should_return_the_correct_formatter_for_a_fault_exception = () =>
            (Factory.Get(typeof(FaultException)) is FaultExceptionFormatter).ShouldBeTrue();

        It should_return_the_correct_formatter_for_a_sql_exception = () =>
            (Factory.Get(typeof(SqlException)) is SqlExceptionFormatter).ShouldBeTrue();    

        It should_return_the_default_formatter_for_any_exception = () =>
            (Factory.Get(typeof(Exception)) is TextExceptionFormatter).ShouldBeTrue();

        It should_return_the_formatter_for_the_base_exception_type = () =>
            (Factory.Get(typeof(ArgumentNullException)) is TextExceptionFormatter).ShouldBeTrue();
    }
}
