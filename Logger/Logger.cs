using System;
using System.Collections.Generic;
using System.IO;
using Logger.Color;

namespace Logger
{
    public class Logger
    {
        public int Priority;
        
        private List<TextWriter> _output = new List<TextWriter>();
        public List<TextWriter> Output => _output;
        
        private Logger(int priority) 
        {
            Priority = priority;
        }

        public Logger(Level level) : this(level.Priority) 
        {}

        ~Logger()
        {
            foreach (var output in _output)
            {
                output.Close();
            }
        }
        
        
        public void Log(Level level, string message, Exception exception = null)
        {
            message += ConsoleActionChar.Reset;
            
            if (level.Priority > Priority) {
                return;
            }
            
            foreach (var output in _output)
            {
                if (output == Console.Out)
                {
                    ConsoleChar.FormattedConsoleWriteLine(message);
                }
                else
                {
                    output.Write(ConsoleChar.Strip(message));
                }
            }
        }

        public void AddOutput(string file)
        {
            TextWriter textWriter = new StreamWriter(file);
            _output.Add(textWriter);
        }
        
        public void AddOutput(TextWriter output)
        {
            _output.Add(output);
        }
    }
}