using NLog.LayoutRenderers;
using Praetorium.Contexts;

namespace Praetorium.NLog
{
    [LayoutRenderer("lc")]
    public class WcfLogContextLayoutRenderer : LogContextLayoutRendererBase
    {
        protected override IContext GetContext()
        {
            return new WcfOrThreadHybridContext();
        }
    }
}
