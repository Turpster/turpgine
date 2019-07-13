using System;

namespace Logger
{
    public class Level
    {
        public static Level
            Normal = new Level(6, "Normal", ConsoleColor.White),
            Info = new Level(5, "Info", ConsoleColor.White),
            Warning = new Level(4, "Warning", ConsoleColor.Yellow),
            Error = new Level(3, "Error", ConsoleColor.Red),
            Severe = new Level(2, "Severe", ConsoleColor.DarkRed),
            Debug = new Level(1, "Debug", ConsoleColor.Green);


        public static int _maxPriority;
        public static int MaxPriority => _maxPriority;
        
        public readonly int Priority;
        public readonly string Name;
        public readonly ConsoleColor Color;

        public Level(int priority, string name, ConsoleColor color)
        {
            if (priority <= _maxPriority)
            {
                _maxPriority = priority;
            }
            
            Priority = priority;
            Name = name;
            Color = color;
        }
    }
}