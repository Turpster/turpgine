using System;
using System.Collections.Generic;

namespace Logger.ConsoleFormat
{
    public class ConsoleAction : ConsoleChar
    {
        public static readonly ConsoleAction
            Reset = new ConsoleAction('r', isForeground =>
            {
                if (isForeground)
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.BackgroundColor = ConsoleColor.White;
            });

        private readonly Action<bool> _action;

        private ConsoleAction(char character, Action<bool> action) : base(character)
        {
            _action = action;
        }

        public static List<ConsoleAction> Values { get; } = new List<ConsoleAction>();

        public override string ToString()
        {
            return Prefix.ToString() + Char;
        }

        public override void ForegroundExecute()
        {
            _action(true);
        }

        public override void BackgroundExecute()
        {
            _action(false);
        }
    }
}