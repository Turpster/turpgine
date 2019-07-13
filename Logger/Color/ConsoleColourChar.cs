using System;

namespace Logger.Color
{
    public class ConsoleColourChar : ConsoleChar
    {
        private static readonly ConsoleColourChar
            _black = new ConsoleColourChar('0', ConsoleColor.Black),
            _darkBlue = new ConsoleColourChar('1', ConsoleColor.DarkBlue),
            _darkGreen = new ConsoleColourChar('2', ConsoleColor.DarkGreen),
            _darkCyan = new ConsoleColourChar('3', ConsoleColor.DarkCyan),
            _darkRed = new ConsoleColourChar('4', ConsoleColor.DarkRed),
            _darkMagenta = new ConsoleColourChar('5', ConsoleColor.DarkMagenta),
            _darkYellow = new ConsoleColourChar('6', ConsoleColor.DarkYellow),
            _gray = new ConsoleColourChar('7', ConsoleColor.Gray),
            _darkGray = new ConsoleColourChar('8', ConsoleColor.DarkGray),
            _blue = new ConsoleColourChar('9', ConsoleColor.Blue),
            _green = new ConsoleColourChar('a', ConsoleColor.Green),
            _cyan = new ConsoleColourChar('b', ConsoleColor.Cyan),
            _red = new ConsoleColourChar('c', ConsoleColor.Red),
            _magenta = new ConsoleColourChar('d', ConsoleColor.Magenta),
            _yellow = new ConsoleColourChar('e', ConsoleColor.Yellow),
            _white = new ConsoleColourChar('f', ConsoleColor.White);

        public static readonly char
            Black = _black.Char,
            DarkBlue = _darkBlue.Char,
            DarkGreen = _darkGreen.Char,
            DarkCyan = _darkCyan.Char,
            DarkRed = _darkRed.Char,
            DarkMagenta = _darkMagenta.Char,
            DarkYellow = _darkYellow.Char,
            Gray = _gray.Char,
            DarkGray = _darkGray.Char,
            Blue = _blue.Char,
            Green = _green.Char,
            Cyan = _cyan.Char,
            Red = _red.Char,
            Magenta = _magenta.Char,
            Yellow = _yellow.Char,
            White = _white.Char;
        
        private readonly ConsoleColor ConsoleColor;

        public ConsoleColourChar(char character, ConsoleColor consoleColor) : base(character)
        {
            ConsoleColor = consoleColor;
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
            Console.ForegroundColor = ConsoleColor;
        }

        public override void BackgroundExecute()
        {
            Console.BackgroundColor = ConsoleColor;
        }
    }
}