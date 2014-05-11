using FluentAssertions;
using System;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Praetorium.UnitTests.Extensions
{
    public class StringBuilderExtensionsSpecs
    {
        [Theory]
        [InlineData(true, "value", "value\r\n")]
        [InlineData(false, "value", "")]
        [InlineData(true, "", "\r\n")]
        public void AppendLineIf_with_value_should_only_append_when_condition_is_true(bool condition, string input, string expectedOutput)
        {
            var builder = new StringBuilder().AppendLineIf(condition, input);

            builder.ToString().Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(true, "\r\n")]
        [InlineData(false, "")]
        public void AppendLineIf_should_only_append_when_condition_is_true(bool condition, string expectedOutput)
        {
            (new StringBuilder()).AppendLineIf(condition)
                .ToString().Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(true, "{0}", "value", "value")]
        [InlineData(false, "{0}", "value", "")]
        public void AppendFormatIf_should_only_append_when_condition_is_true(bool condition, string format, string arg, string expectedOutput)
        {
            (new StringBuilder()).AppendFormatIf(condition, format, arg)
                .ToString().Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(true, "value", "value")]
        [InlineData(false, "value", "")]
        public void AppendIf_should_only_append_when_condition_is_true(bool condition, string value, string expectedOutput)
        {
            (new StringBuilder()).AppendIf(condition, value)
                .ToString().Should().Be(expectedOutput);
        }

        [Fact]
        public void ToUpper_should_uppercase_all_chars_in_the_builder()
        {
            const string input = "hello there";

            (new StringBuilder(input)).ToUpper()
                .ToString().Should().Be(input.ToUpper());
        }

        [Fact]
        public void ToLower_should_lowercase_all_chars_in_the_builder()
        {
            const string input = "HELLO THERE";

            (new StringBuilder(input)).ToLower()
                .ToString().Should().Be(input.ToLower());
        }

        [Theory]
        [InlineData(" ", 5)]
        [InlineData("h", 0)]
        [InlineData("z", -1)]
        public void IndexOf_should_return_the_correct_index(string find, int expectedIndex)
        {
            (new StringBuilder("hello there")).IndexOf(find)
                .Should().Be(expectedIndex);
        }

        [Theory]
        [InlineData(" ", 0, 5)]
        [InlineData("h", 0, 0)]
        [InlineData("z", 0, -1)]
        [InlineData("h", 2, 7)]
        [InlineData("h", -1, 0)]
        
        public void IndexOf_with_start_index_should_return_the_correct_index(string find, int startIndex, int expectedIndex)
        {
            (new StringBuilder("hello there")).IndexOf(find, startIndex)
                .Should().Be(expectedIndex);
        }

        [Fact]
        public void IndexOf_with_invalid_start_index_should_throw_arg_ex()
        {
            (new StringBuilder("test"))
                .Invoking(x => x.IndexOf("5", 10))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void IndexOf_with_empty_search_should_throw_arg_null_ex()
        {
            (new StringBuilder("test"))
                .Invoking(x => x.IndexOf(""))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void IndexOf_empty_builder_should_return_neg_one()
        {
            (new StringBuilder("")).IndexOf("h")
                .Should().Be(-1);
        }
    }
}
