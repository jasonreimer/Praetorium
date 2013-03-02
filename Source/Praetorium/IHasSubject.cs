namespace Praetorium
{
    /// <summary>
    /// An interface that is optionally implemented that adds a typed subject to the underlying implementation.
    /// </summary>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <remarks>
    /// This interface is typically implemented by objects that have a unique subject and need to 
    /// be distinguished from other instances of the same type.
    /// </remarks>
    public interface IHasSubject<TSubject> where TSubject : class
    {

        /// <summary>
        /// Gets the subject of the instance.
        /// </summary>
        /// <value>The subject.</value>
        TSubject Subject { get; }

    }
}
