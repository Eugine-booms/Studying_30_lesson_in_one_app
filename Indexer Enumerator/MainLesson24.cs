
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LessonInOne.Indexer_Enumerator
{
    static public class MainLesson24
    {
        static List<Car> cars;
        static Parking parking = new Parking();
        static public void Init()
        {
            cars = new List<Car>()
            {
                new Car(){ Name ="Ford", Number = "A001AA01"},
                new Car(){ Name ="Lada", Number = "B007BB077"},
                new Car() { Name = "VAZ", Number = "C000CC174"}
            };
            foreach (var car in cars)
            {
                parking.Add(car);
            }
        }

        internal static void ExampleStart()
        {
            //example parking indexer
            ExampleIndexer();


            
        }

        //TODO : доделать 
        public static void ExampleIndexer()
        {
            ExampleIndexerFromIdex();
            ExampleIndexerFromNumber();
            
            foreach (var car in parking)
            {
                Console.WriteLine(car);
            }
            foreach (var item in parking.GetNames())
            {
                Console.WriteLine(item);
            }
            foreach (var item in parking.GetNumbers())
            {
                Console.WriteLine(item);
            }
        }

        private static void ExampleIndexerFromNumber()
        {
            Console.WriteLine("ExampleIndexerFromNumber");
            Console.Write("существующий номер");
            Console.WriteLine(parking["A001AA01"]);
            Console.Write("Несуществующий номер");
            Console.WriteLine(parking["A001BB01"]);
        }

        private static void ExampleIndexerFromIdex()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ExampleIndexerFromIdex");
            for (int i = 0; i < parking.Count; i++)
            {
                Console.WriteLine(parking[i]);
            }
            
            ExampleReplaceItemFromIndex();
        }
        private static void ExampleReplaceItemFromIndex()
        {
            Console.WriteLine("ExampleReplaceItemFromIndex");
            Console.WriteLine($"Before parking[1]= {parking[1]}");
            Console.WriteLine("after replace parking[1] to new Car() { Name = VAZ, Number = C000CC174");
            parking[1] = new Car() { Name = "VAZ", Number = "C000CC174" };
            Console.WriteLine(parking);
        }
    }
}


