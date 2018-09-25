using System;

namespace LimeAcademy.FizzBuzz
{
    public class FizzBuzzCommand
    {
        private const string Fizz = "Fizz";
        private const string Buzz = "Buzz";
        private const int FizzMultiplier = 3;
        private const int BuzzMultiplier = 5;
        private const int FizzBuzzMultiplier = FizzMultiplier * BuzzMultiplier;

        public Action<string> Output { get; set; }

        public FizzBuzzCommand()
        {
            Output = message => Console.WriteLine(message);
        }

        public FizzBuzzCommand(Action<string> outputAction)
        {
            Output = outputAction;
        }

        public void FizzBuzz(uint iterations)
        {
            for (int i=1; i<=iterations; i++)
            {
                if (i % FizzBuzzMultiplier == 0)
                    Output($"{Fizz}{Buzz}");
                else if (i % FizzMultiplier == 0)
                    Output(Fizz);
                else if (i % BuzzMultiplier == 0)
                    Output(Buzz);
                else
                    Output(i.ToString());
            }
        }
    }
}
