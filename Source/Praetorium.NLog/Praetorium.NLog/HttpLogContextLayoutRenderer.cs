using NLog.LayoutRenderers;
using Praetorium.Contexts;
using Praetorium.Logging;

namespace Praetorium.NLog
{
    [LayoutRenderer("lc")] 
    public class HttpLogContextLayoutRenderer : LogContextLayoutRendererBase
    {
        private static readonly string _contextKey = typeof(ILogContextScope).FullName;

        protected override ILogContextScope GetContext()
        {
            var context = new HttpRequestOrThreadHybridContext();

            return context.GetOrDefault<ILogContextScope>(_contextKey);
        }
    }
}
