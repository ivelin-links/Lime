using System;
using System.Text;
using Xunit;

namespace LimeAcademy.FizzBuzz.Tests
{
    public class UnitTests
    {
        StringBuilder _outputResult = new StringBuilder();
        Action<string> _stringBuilderOutput => message => _outputResult.Append($"{message}, ");

        [Fact]
        public void HappyPath35()
        {
            const string Iterations35 =
                "1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz, 11, Fizz, 13, 14, FizzBuzz, 16, 17, Fizz, 19, Buzz, " +
                "Fizz, 22, 23, Fizz, Buzz, 26, Fizz, 28, 29, FizzBuzz, 31, 32, Fizz, 34, Buzz, ";

            var fizzBuzzSolver = new FizzBuzzCommand(_stringBuilderOutput);
            fizzBuzzSolver.FizzBuzz(35);

            Assert.Equal(Iterations35, _outputResult.ToString());
        }

        [Fact]
        public void HappyPath1()
        {
            const string Iteration1 = "1, ";

            var fizzBuzzSolver = new FizzBuzzCommand(_stringBuilderOutput);
            fizzBuzzSolver.FizzBuzz(1);

            Assert.Equal(Iteration1, _outputResult.ToString());
        }

        [Fact]
        public void HappyPath0()
        {
            const string Iterations0 = "";

            var fizzBuzzSolver = new FizzBuzzCommand(_stringBuilderOutput);
            fizzBuzzSolver.FizzBuzz(0);

            Assert.Equal(Iterations0, _outputResult.ToString());
        }
    }
}
