using System;
using System.Collections.Generic;

namespace Logger.ConsoleFormat
{
    public class ConsoleAction : ConsoleChar
    {
        public static List<ConsoleAction> Values => _values;
        private static List<ConsoleAction> _values = new List<ConsoleAction>();
        
        public static readonly ConsoleAction
            Reset = new ConsoleAction('r', (isForeground) =>
            {
                if (isForeground)
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.BackgroundColor = ConsoleColor.White;
            });

        private readonly Action<bool> _action;

        public override string ToString()
        {
            return Prefix.ToString() + Char;
        }

        public ConsoleAction(char character, Action<bool> action) : base(character)
        {
            _action = action;
        }

        public override void ForegroundExecute() => _action(true);
        public override void BackgroundExecute() => _action(false);
    }
}