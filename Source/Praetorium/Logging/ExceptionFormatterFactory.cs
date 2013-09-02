using Praetorium.Collections;
using System;
using System.Linq;

namespace Praetorium.Logging
{
    public class ExceptionFormatterFactory : IExceptionFormatterFactory
    {
        private readonly FunctionKeyedCollection<Type, IExceptionFormatterBuilder> _builders;
        private readonly IExceptionFormatter _defaultExceptionFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionFormatterFactory"/> class.
        /// </summary>
        /// <param name="builders">The builders.</param>
        public ExceptionFormatterFactory(IExceptionFormatterBuilder[] builders)
        {
            builders = builders ?? new IExceptionFormatterBuilder[0];
            _defaultExceptionFormatter = new TextExceptionFormatter(this);
            _builders = new FunctionKeyedCollection<Type, IExceptionFormatterBuilder>(x => x.BaseExceptionType, builders);
        }

        protected internal ExceptionFormatterFactory()
            : this(null)
        {
        }

        public IExceptionFormatter Get(Type exceptionType)
        {
            Ensure.ArgumentNotNull(() => exceptionType);

            var formatter = _builders.IsEmpty() ? _defaultExceptionFormatter : FindExceptionFormatter(exceptionType);

            return Ensure.ReturnIsNotNull(formatter);
        }

        private IExceptionFormatter FindExceptionFormatter(Type exceptionType)
        {
            var builder = _builders.GetValueOrDefault(exceptionType) 
                          ?? _builders.FirstOrDefault(x => exceptionType.DerivesFrom(x.BaseExceptionType));

            return builder != null ? builder.Get() : _defaultExceptionFormatter;
        }
    }
}
