namespace Praetorium
{
    /// <summary>
    /// Represents a builder strategy for use within a factory.
    /// </summary>
    /// <typeparam name="TContext">
    /// The factory context.  Examples would include Exception for an ExceptionHandlerBuilder, etc.
    /// </typeparam>
    /// <typeparam name="TBuild">
    /// The output of the builder.
    /// </typeparam>
    public interface IBuilder<TContext, TBuild>
    {

        bool Supports(TContext context);

        TBuild Create(TContext context);

    }
}
