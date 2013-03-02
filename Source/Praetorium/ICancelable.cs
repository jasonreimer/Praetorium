namespace Praetorium
{
    /// <summary>
    /// An abstraction for supporting cancel operations.
    /// </summary>
    public interface ICancelable
    {
        /// <summary>
        /// Gets a value indicating whether or not the object supports the <see cref="Cancel"/> operation.
        /// </summary>
        /// <value><c>true</c> if a cancel is supported; otherwise, <c>false</c>.</value>
        bool SupportsCancel { get; }

        /// <summary>
        /// Cancels the running operation.
        /// </summary>
        void Cancel();
    }
}
