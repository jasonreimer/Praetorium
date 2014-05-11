using FluentAssertions;
using Praetorium.Contexts;
using System.Threading;
using Xunit;

namespace Praetorium.UnitTests.Contexts
{
    public class ThreadContextSpecs
    {
        private const string _key = "key";
        private const string _value = "value";

        private readonly IContext _context = new ThreadContext();

        public ThreadContextSpecs()
        {
            _context[_key] = _value;
        }

        [Fact]
        public void Context_returns_different_values_across_threads()
        {
            var waitHandler = new AutoResetEvent(false);

            ThreadPool.QueueUserWorkItem(s =>
            {
                _context.Add(_key, "test");
                waitHandler.Set();
            });

            waitHandler.WaitOne(10000).Should().BeTrue();
            _context[_key].Should().Be(_value);
        }

        [Fact]
        public void Context_returns_same_value_for_same_thread()
        {
            var newContext = new ThreadContext();

            _context[_key].Should().Be(newContext[_key]);
        }
    }
}
