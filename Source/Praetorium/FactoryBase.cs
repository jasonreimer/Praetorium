using System.Linq;

namespace Praetorium
{
    public abstract class FactoryBase<TBuilder, TContext, TBuild> where TBuilder : class, IBuilder<TContext, TBuild>
    {
        private readonly TBuilder[] _builders;
        private readonly TBuilder _defaultBuilder;

        protected FactoryBase(TBuilder[] builders)
        {
            _builders = builders ?? new TBuilder[0];
        }

        protected FactoryBase(TBuilder[] builders, TBuilder defaultBuilder)
            : this(builders)
        {
            Ensure.ArgumentNotNull(() => defaultBuilder, ref _defaultBuilder);
        }

        public virtual TBuild Create(TContext context)
        {
            var builder = _builders.FirstOrDefault(x => x.Supports(context)) ?? _defaultBuilder;

            return builder == null ? default(TBuild) : builder.Create(context);
        }
    }
}
