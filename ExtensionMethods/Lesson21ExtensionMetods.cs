using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.ExtensionMethods
{
    /// <summary>
    /// Все методы расширения лежат в классе Helper
    /// </summary>
    public class Lesson21ExtensionMetods : LessonBase
    {
        protected override void Init()
        {
            throw new NotImplementedException();
        }
        public override void LessonMain()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            IsEvenExtensionMetodsForIntExample();
            IsDevidedByExtensionMetodsExample();
            RandomRoadRangeGeneratorExtensionMethodExample();
            RandomRoadGeneratorExtensionMethodExample();

            Console.ReadKey();
        }
        static void IsEvenExtensionMetodsForIntExample()
        {
            Console.Write("Проверка числа на четность: \n");
            var result = ConsoleTryParserInt();
            Console.WriteLine("Число четное -" + result.IsEven());
            Console.WriteLine();
        }
        private static int ConsoleTryParserInt()
        {
            while (true)
            {
                Console.WriteLine("Введите число");
                if (int.TryParse(Console.ReadLine(), out int result)) return result;
            }
        }
        /// <summary>
        /// Допустим у класса Road нет необходимого нам конструктора и класс запечатанный (нельзя отнаследоваться)
        /// но мы можем создать метод расширения с которым нам будет удобно работать.
        /// </summary>
        private static void RandomRoadGeneratorExtensionMethodExample()
        {
            var road = new Road();
            road.CreateRandomRoad(5, 10);
            Console.WriteLine("RandomRoadGeneratorExtensionMethodExample");
            Console.WriteLine(road);
        }
        private static void RandomRoadRangeGeneratorExtensionMethodExample()
        {
            var road = new Road();
            var RandomRoadsList = road.CreateRandomRoadRange(10, 100, 1000);
            var str = string.Join(", ", RandomRoadsList.Select(x => x.ToString()));
            Console.WriteLine("RandomRoadRangeGeneratorExtensionMethodExample");
            Console.WriteLine("\n" + str);
        }

        private static void IsDevidedByExtensionMetodsExample()
        {
            Console.Write("Проверка делится ли одно число на другое без остатка\n");
            var firstNumber = ConsoleTryParserInt();
            Console.WriteLine("Первое число = " + firstNumber + " \nВведите второе число ");
            var secondNumber = ConsoleTryParserInt();
            Console.WriteLine("Первое число делится на второе без остатка это - " + firstNumber.DividedBy(secondNumber));

        }


    }
}
