using System;

namespace Logger.Color
{
    public class ConsoleActionChar : ConsoleChar
    {
        private static readonly ConsoleActionChar
            _reset = new ConsoleActionChar('r', (isForeground) =>
            {
                if (isForeground)
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.BackgroundColor = ConsoleColor.White;
            });
        public static readonly string
            Reset = Prefix.ToString() + _reset.Char;

        private readonly Action<bool> _action;
        
        public ConsoleActionChar(char character, Action<bool> action) : base(character)
        {
            _action = action;
        }

        public override void ForegroundExecute() => _action(true);
        public override void BackgroundExecute() => _action(false);
    }
}