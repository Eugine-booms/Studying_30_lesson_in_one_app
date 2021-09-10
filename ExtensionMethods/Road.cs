using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.ExtensionMethods
{
    /// <summary>
    /// sealed - Запечатанный класс от которого нельзя наследоваться.
    /// </summary>
    public sealed class Road
    {
        public Road()
        {
        }

        public Road(string number, int lenght)
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Lenght = lenght;
        }
        public string Number { get; set; }
        public int Lenght { get; set; }
        public override string ToString()
        {
            return $"Дорога {Number} - {Lenght} Км";
        }

    }
}
