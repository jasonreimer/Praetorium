using System;
using System.Threading;

namespace Praetorium.Threading
{
    public class DefaultWorkItem : IWorkItem
    {
        private readonly Guid _id = Guid.NewGuid();
        private Thread _thread;

        public Guid Id
        {
            get { return _id; }
        }

        public bool SupportsCancel
        {
            get { return true; }
        }

        internal Action WorkAction
        {
            get;
            set;
        }

        public void Cancel()
        {
            _thread.Abort();
        }

        internal void SetThread()
        {
            _thread = Thread.CurrentThread;
        }
    }
}
