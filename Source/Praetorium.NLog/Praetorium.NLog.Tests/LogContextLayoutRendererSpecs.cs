using FluentAssertions;
using NLog;
using Praetorium.Contexts;
using Xunit;

namespace Praetorium.NLog.Tests
{
    public class LogContextLayoutRendererSpecs
    {
        private const string item = "user_id";
        private const string value = "value";

        private readonly IContext _context;
        private readonly LogContextLayoutRendererBase _renderer;
        private readonly LogEventInfo _logEventInfo = new LogEventInfo(LogLevel.Fatal, "logger", "message");

        public LogContextLayoutRendererSpecs()
        {
            _context = new DictionaryContext();
            _renderer = new TestLogContextLayoutRenderer(_context)
            {
                Item = item
            };
        }

        [Fact]
        public void Renderer_should_add_the_context_value_to_the_output()
        {
            _context[item] = value;

            _renderer.Render(_logEventInfo).Should().Contain(value);
        }

        [Fact]
        public void Renderer_should_return_empty_string_when_item_missing()
        {
            _renderer.Render(_logEventInfo).Should().BeEmpty();
        }
    }
    
    public class TestLogContextLayoutRenderer : LogContextLayoutRendererBase
    {
        private readonly IContext _context;

        public TestLogContextLayoutRenderer(IContext context)
        {
            Ensure.ArgumentNotNull(() => context, ref _context);
        }

        protected override IContext GetContext()
        {
            return _context;
        }
    }
}
