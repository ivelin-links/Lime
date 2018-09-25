using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LimeAcademy.Braces
{
    public class BracesCommand
    {
        private static IReadOnlyDictionary<char, char> _supportedBrackets =
            new ReadOnlyDictionary<char, char>(new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' }
            });

        public string[] Braces(params string[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var result = new string[values.Count()];

            for (int currentValueIndex = 0; currentValueIndex < values.Length; currentValueIndex++)
            {
                result[currentValueIndex] = AreBracketsCorrect(values[currentValueIndex])
                    ? "YES" 
                    : "NO";
            }

            return result;
        }

        private bool AreBracketsCorrect(string text)
        {
            var stack = new Stack<char>();
            for (int charIndex = 0; charIndex < text.Length; charIndex++)
            {
                if (_supportedBrackets.ContainsKey(text[charIndex]))
                {
                    stack.Push(text[charIndex]);
                }
                else if (stack.Count == 0 || text[charIndex] != _supportedBrackets[stack.Pop()])
                {
                    return false;
                }
            }

            return !stack.Any();
        }
    }
}
