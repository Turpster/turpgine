using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Logger.ConsoleFormat;

namespace Logger
{
    public class Logger
    {
        public int Priority;

        private Logger(int priority)
        {
            Priority = priority;
        }

        public Logger(Level level) : this(level.Priority)
        {
            
        }

        public List<TextWriter> Outputs { get; } = new List<TextWriter>();

        ~Logger()
        {
            foreach (var output in Outputs) output.Close();
        }


        public void Log(Level level, string message, Exception exception = null)
        {
            if (level.Priority < Priority) return;

            var _message = new StringBuilder();

            _message.Append(ConsoleAction.Reset + "[" + ConsoleColour.GetConsoleColour(level.Color) + level.Name +
                            ConsoleAction.Reset + "]: " + message);

/*
 *            TODO Disable Default Unhandled Exception handler that outputs exception to console.
 * 
 *            if (exception != null)
 *            {
 *                Console.WriteLine(exception.GetType());
 *                _message.Append(ConsoleColour.Red + "\n" + exception.Message + "\n" + exception.StackTrace);
 *            }
 *            
 */

            if (exception != null)
                _message.Append(ConsoleColour.Red);
            else
                _message.Append(ConsoleAction.Reset);

            foreach (var output in Outputs)
                if (output == Console.Out)
                    ConsoleChar.FormattedConsoleWriteLine(_message.ToString());
                else
                    output.Write(ConsoleChar.Strip(_message.ToString()));
        }

        public void AddOutput(string file)
        {
            TextWriter textWriter = new StreamWriter(file);
            Outputs.Add(textWriter);
        }

        public void AddOutput(TextWriter output)
        {
            Outputs.Add(output);
        }
    }
}