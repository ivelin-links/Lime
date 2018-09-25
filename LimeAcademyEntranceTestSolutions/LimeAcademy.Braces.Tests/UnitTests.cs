using System;
using Xunit;

namespace LimeAcademy.Braces.Tests
{
    public class UnitTests
    {
        [Fact]
        public void HappyPath1()
        {
            var command = new BracesCommand();
            var result = command.Braces("{}[]()");

            Assert.Single(result);
            Assert.Equal("YES", result[0]);
        }

        [Fact]
        public void HappyPath2()
        {
            var command = new BracesCommand();
            var result = command.Braces("{[]()}", "{[()]}");

            Assert.Equal(2, result.Length);
            Assert.Equal("YES", result[0]);
            Assert.Equal("YES", result[1]);
        }

        [Fact]
        public void MixedPath1()
        {
            var command = new BracesCommand();
            var result = command.Braces("{[]()}", "{[)]}", "");

            Assert.Equal(3, result.Length);
            Assert.Equal("YES", result[0]);
            Assert.Equal("NO", result[1]);
            Assert.Equal("YES", result[2]);
        }
    }
}
