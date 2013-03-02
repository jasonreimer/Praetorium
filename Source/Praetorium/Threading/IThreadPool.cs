using System;

namespace Praetorium.Threading
{
    /// <summary>
    /// Represents pool of threads that can be used to post work items.
    /// </summary>
    public interface IThreadPool
    {

        /// <summary>
        /// Gets a value indicating whether or not the work items support the <see cref="Propel.Framework.Common.ICancelable.Cancel"/> operation.
        /// </summary>
        /// <value><c>true</c> if a cancel is supported; otherwise, <c>false</c>.</value>
        bool SupportsCancel { get; }

        /// <summary>
        /// Queues the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        IWorkItem Queue(Action action);

    }
}
