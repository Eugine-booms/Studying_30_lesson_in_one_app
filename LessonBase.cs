using System;

namespace LessonInOne
{
    public abstract class LessonBase
    {

        protected abstract void Init();
        public abstract void LessonMain();
        protected int ConsoleTryParseInt()
        {
            int result = default;
            do { Console.WriteLine("Введите число "); }
            while (!int.TryParse(Console.ReadLine(), out result));
            return result;
        }
        public static void PrintLineConsole(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void PrintConsole(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}