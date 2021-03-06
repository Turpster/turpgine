using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.ConsoleFormat
{
    public abstract class ConsoleChar
    {
        public const char Prefix = '&';

        private static readonly Dictionary<char, ConsoleChar> ConsoleChars = new Dictionary<char, ConsoleChar>();
        internal readonly char Char;

        protected ConsoleChar(char character)
        {
            Char = character;

            if (!ConsoleChars.ContainsKey(character))
                ConsoleChars[character] = this;
            else
                throw new FormatException("Character " + character + " already has a designated Console Char.");
        }

        public static void FormattedConsoleWrite(string formattedMessage)
        {
            var i = 0;
            for (; i < formattedMessage.Length - 1; i++) // TODO last two characters
            {
                if (formattedMessage[i] == Prefix)
                    if (ConsoleChars.ContainsKey(formattedMessage[i + 1]))
                    {
                        i++;

                        ConsoleChars[formattedMessage[i]].ForegroundExecute();
                        continue;
                    }

                Console.Write(formattedMessage[i]);
            }

            if (i != formattedMessage.Length) Console.Write(formattedMessage[formattedMessage.Length - 1]);
        }

        public static void FormattedConsoleWriteLine(string formattedMessage)
        {
            FormattedConsoleWrite(formattedMessage);
            Console.Write("\n");
        }

        public static string Strip(string formattedMessage)
        {
            var strippedString = new StringBuilder();

            for (var i = 0; i < formattedMessage.Length - 1; i++) // TODO last two characters
            {
                if (formattedMessage[i] == Prefix)
                    if (ConsoleChars.ContainsKey(formattedMessage[i + 1]))
                    {
                        i++;
                        continue;
                    }

                strippedString.Append(formattedMessage[i]);
            }

            strippedString.Append(formattedMessage[formattedMessage.Length - 1]);

            return strippedString.ToString();
        }


        public abstract void ForegroundExecute();
        public abstract void BackgroundExecute();

        public void Values()
        {
            throw new NotImplementedException();
        }
    }
}