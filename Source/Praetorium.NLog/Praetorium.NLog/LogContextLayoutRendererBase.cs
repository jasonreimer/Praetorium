using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using Praetorium.Logging;
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

            if (context != null)
            {
                var item = context[Item];

                if (item != null)
                    builder.Append(item);
            }
        }

        protected abstract ILogContextScope GetContext();
    }
}
