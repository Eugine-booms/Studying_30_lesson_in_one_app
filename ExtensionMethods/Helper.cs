using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.ExtensionMethods
{
   public static class Helper
    {
        public static bool IsEven(this int i) => i % 2==0;
        public static bool DividedBy(this int i, int j) => i % j == 0;

        public static List<Road> CreateRandomRoadRange(this Road road,  int number, int minLenght, int maxLenght)
        {
            var rnd = new Random(Guid.NewGuid().ToByteArray().Sum(x=>x));
            var roads = new List<Road>();
            for (int i = 0; i < number; i++)
            {
                roads.Add(new Road("E"+Guid.NewGuid().ToString().Substring(0, 3), rnd.Next(minLenght, maxLenght)));
            }
            return roads;
        }
        public static Road CreateRandomRoad(this Road road, int minLenght, int maxLenght)
        {
            var rnd = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
            road.Number = "E" + Guid.NewGuid().ToString().Substring(0, 3);
            road.Lenght = rnd.Next(minLenght, maxLenght);
            return road;
        }
    }
}
