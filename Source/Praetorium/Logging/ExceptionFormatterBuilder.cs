﻿using System;

namespace Praetorium.Logging
{
    public class ExceptionFormatterBuilder<TException, TFormatter> : ExceptionFormatterBuilder<TFormatter>
        where TException : Exception
        where TFormatter : class, IExceptionFormatter
    {
        public ExceptionFormatterBuilder(Func<IExceptionFormatterFactory,TFormatter> formatterLocator)
            : base(formatterLocator, typeof(TException))
        {
        }
    }

    public class ExceptionFormatterBuilder<TFormatter> : IExceptionFormatterBuilder
        where TFormatter : class, IExceptionFormatter
    {
        private readonly Type _baseExceptionType;
        private readonly Func<IExceptionFormatterFactory,TFormatter> _formatterLocator;

        public ExceptionFormatterBuilder(Func<IExceptionFormatterFactory,TFormatter> formatterLocator, Type baseExceptionType)
        {
            Ensure.ArgumentNotNull(() => formatterLocator, ref _formatterLocator);
            Ensure.ArgumentNotNull(() => baseExceptionType, ref _baseExceptionType);
        }

        public Type BaseExceptionType
        {
            get { return _baseExceptionType; }
        }

        public IExceptionFormatter Get(IExceptionFormatterFactory factory)
        {
            return _formatterLocator(factory);
        }
    }
}
