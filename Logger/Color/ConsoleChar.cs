using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Color
{
    public abstract class ConsoleChar
    {
        internal readonly char Char;

        public const char Prefix = '&';

        private static Dictionary<char, ConsoleChar> ConsoleChars = new Dictionary<char, ConsoleChar>();

        public ConsoleChar(char character)
        {
            Char = character;
            
            if (!ConsoleChars.ContainsKey(character))
                ConsoleChars[character] = this;
            else
                throw new FormatException("Character " + character + " already has a designated Console Char.");
        }
        
        public static void FormattedConsoleWrite(string formattedMessage)
        {
            for (var i = 0; i < formattedMessage.Length - 1; i++) // TODO last two characters
            {
                if (formattedMessage[i] == Prefix)
                {
                    if (ConsoleChars.ContainsKey(formattedMessage[i + 1]))
                    {
                        i++;
                        
                        ConsoleChars[formattedMessage[i]].ForegroundExecute();
                    }
                }
            }

            Console.Write(formattedMessage[formattedMessage.Length - 1]);
        }

        public static string Strip(string formattedMessage)
        {
            StringBuilder strippedString = new StringBuilder();
            
            for (var i = 0; i < formattedMessage.Length - 1; i++) // TODO last two characters
            {
                if (formattedMessage[i] == Prefix)
                {
                    if (ConsoleChars.ContainsKey(formattedMessage[i + 1]))
                    {
                        i++;
                        continue;
                    }
                }

                strippedString.Append(formattedMessage[i]);
            }

            strippedString.Append(formattedMessage[formattedMessage.Length - 1]);

            return strippedString.ToString();
        }

        public static void FormattedConsoleWriteLine(string formattedMessage)
        {
            FormattedConsoleWrite(formattedMessage);
            Console.Write("\n");
        }
        

        public abstract void ForegroundExecute();
        public abstract void BackgroundExecute();
    }
}