using NLog.LayoutRenderers;
using Praetorium.Contexts;

namespace Praetorium.NLog
{
    [LayoutRenderer("lc")] 
    public class HttpLogContextLayoutRenderer : LogContextLayoutRendererBase
    {
        protected override IContext GetContext()
        {
            return new HttpRequestOrThreadHybridContext();
        }
    }
}
