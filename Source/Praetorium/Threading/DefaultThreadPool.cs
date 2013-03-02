using System;
using System.Threading;

namespace Praetorium.Threading
{
    public class DefaultThreadPool : IThreadPool
    {

        public bool SupportsCancel
        {
            get { return true; }
        }

        public IWorkItem Queue(Action action)
        {
            Ensure.ArgumentNotNull(() => action);

            var workItem = new DefaultWorkItem
            {
                WorkAction = action
            };

            ThreadPool.QueueUserWorkItem(ExecuteWorkItem, workItem);

            return Ensure.ReturnIsNotNull(workItem);
        }

        private void ExecuteWorkItem(object state)
        {
            var workItem = state as DefaultWorkItem;

            if (workItem != null)
            {
                workItem.SetThread();
                workItem.WorkAction();
            }
        }

    }
}
