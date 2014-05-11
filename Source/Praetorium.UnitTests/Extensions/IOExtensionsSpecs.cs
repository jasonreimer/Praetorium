using FluentAssertions;
using System.IO;
using Xunit;
using Xunit.Extensions;

namespace Praetorium.UnitTests.Extensions
{
    public class IOExtensionsSpecs
    {
        [Fact]
        public void ReadToEnd_should_return_all_bytes()
        {
            var data = new byte[] { 0x1, 0x2, 0x3 };
            var stream = new MemoryStream(data);

            stream.ReadToEnd().Should().ContainInOrder(data);           
        }

        [Fact]
        public void ReadAllText_should_return_the_correct_string()
        {
            const string expectedValue = "hello";

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream)
            {
                AutoFlush = true
            };

            writer.Write(expectedValue);
            stream.Position = 0;

            stream.ReadAllText().Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(true, "value", "value")]
        [InlineData(false, "value", "")]
        public void WriteIf_with_format_should_write_if_the_condition_is_true(bool condition, string input, string expectedOutput)
        {
            var writer = new StringWriter();

            writer.WriteIf(condition, "{0}", input);

            writer.ToString().Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(true, "value", "value")]
        [InlineData(false, "value", "")]
        public void WriteIf_should_write_if_the_condition_is_true(bool condition, string input, string expectedOutput)
        {
            var writer = new StringWriter();

            writer.WriteIf(condition, input);

            writer.ToString().Should().Be(expectedOutput);
        }
    }
}
