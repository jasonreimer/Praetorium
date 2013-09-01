using NLog.LayoutRenderers;
using Praetorium.Contexts;
using Praetorium.Logging;

namespace Praetorium.NLog
{
    [LayoutRenderer("lc")]
    public class WcfLogContextLayoutRenderer : LogContextLayoutRendererBase
    {
        private static readonly string _contextKey = typeof(ILogContextScope).FullName;

        protected override ILogContextScope GetContext()
        {
            var context = new WcfOrThreadHybridContext();

            return context.GetOrDefault<ILogContextScope>(_contextKey);
        }
    }
}
