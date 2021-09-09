using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
  public  class Eat
    {
        public string Name { get; set; }
        public int Energy { get; set; }
        public override string ToString()
        {
            return $"Название - {Name} - энергия {Energy}";
        }
    }
}
