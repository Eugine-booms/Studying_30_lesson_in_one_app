using System;

namespace LessonInOne
{
    public abstract class LessonBase
    {
        protected abstract void Init();
        public abstract void LessonMain();
        protected void PrintConsole(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}