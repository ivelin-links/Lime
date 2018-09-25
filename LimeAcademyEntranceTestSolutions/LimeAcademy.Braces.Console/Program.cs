using System;

namespace LimeAcademy.Braces.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputValues = new string[] { "{}[]()", "{[}]}" };

            var fizzBuzzCommand = new BracesCommand();
            var outputValues = fizzBuzzCommand.Braces(inputValues);

            System.Console.WriteLine("Input: ");
            System.Console.WriteLine(string.Join(Environment.NewLine, inputValues));

            System.Console.WriteLine(Environment.NewLine + "Output: ");
            System.Console.WriteLine(string.Join(Environment.NewLine, outputValues));
        }
    }
}
