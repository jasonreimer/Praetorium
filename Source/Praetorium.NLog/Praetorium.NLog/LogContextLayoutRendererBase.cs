using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using Praetorium.Contexts;
using System.Text;

namespace Praetorium.NLog
{
    public abstract class LogContextLayoutRendererBase : LayoutRenderer
    {
        [RequiredParameter]
        [DefaultParameter]
        public string Item { get; set; }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var context = GetContext();

            if (context.Contains(Item))
                builder.Append(context[Item]);
        }

        protected abstract IContext GetContext();
    }
}
