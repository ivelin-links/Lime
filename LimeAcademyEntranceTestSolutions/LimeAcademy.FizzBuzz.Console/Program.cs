using System;

namespace LimeAcademy.FizzBuzz.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzzCommand = new FizzBuzzCommand();
            fizzBuzzCommand.FizzBuzz(31);
        }
    }
}
