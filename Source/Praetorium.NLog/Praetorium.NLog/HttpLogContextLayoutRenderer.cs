using NLog.LayoutRenderers;
using Praetorium.Contexts;

namespace Praetorium.NLog
{
    [LayoutRenderer("http-ctx")] 
    public class HttpLogContextLayoutRenderer : LogContextLayoutRendererBase
    {
        protected override IContext GetContext()
        {
            return new HttpRequestOrThreadHybridContext();
        }
    }
}
