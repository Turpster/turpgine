using System;
using System.Collections.Generic;

namespace Logger.ConsoleFormat
{
    public class ConsoleColour : ConsoleChar
    {
        public static List<ConsoleColour> Values => _values;
        private static List<ConsoleColour> _values = new List<ConsoleColour>();
        
        public static readonly ConsoleColour
            Black = new ConsoleColour('0', ConsoleColor.Black),
            DarkBlue = new ConsoleColour('1', ConsoleColor.DarkBlue),
            DarkGreen = new ConsoleColour('2', ConsoleColor.DarkGreen),
            DarkCyan = new ConsoleColour('3', ConsoleColor.DarkCyan),
            DarkRed = new ConsoleColour('4', ConsoleColor.DarkRed),
            DarkMagenta = new ConsoleColour('5', ConsoleColor.DarkMagenta),
            DarkYellow = new ConsoleColour('6', ConsoleColor.DarkYellow),
            Gray = new ConsoleColour('7', ConsoleColor.Gray),
            DarkGray = new ConsoleColour('8', ConsoleColor.DarkGray),
            Blue = new ConsoleColour('9', ConsoleColor.Blue),
            Green = new ConsoleColour('a', ConsoleColor.Green),
            Cyan = new ConsoleColour('b', ConsoleColor.Cyan),
            Red = new ConsoleColour('c', ConsoleColor.Red),
            Magenta = new ConsoleColour('d', ConsoleColor.Magenta),
            Yellow = new ConsoleColour('e', ConsoleColor.Yellow),
            White = new ConsoleColour('f', ConsoleColor.White);

        private readonly ConsoleColor _consoleColor;

        public override string ToString()
        {
            return Prefix.ToString() + Char;
        }

        public ConsoleColour(char character, ConsoleColor consoleColor) : base(character)
        {
            _consoleColor = consoleColor;

            _values.Add(this);
        }
        
        public static ConsoleColour GetConsoleColour(ConsoleColor color)
        {
            foreach (var consoleColour in _values)
            {
                if (consoleColour._consoleColor == color)
                {
                    return consoleColour;
                }
            }

            return null;
        }
        
        public int[] ToRGB()
        {
            throw new System.NotImplementedException();
        }

        public string ToHEX()
        {
            throw new System.NotImplementedException();
        }

        public override void ForegroundExecute()
        {
            Console.ForegroundColor = _consoleColor;
        }

        public override void BackgroundExecute()
        {
            Console.BackgroundColor = _consoleColor;
        }
    }
}