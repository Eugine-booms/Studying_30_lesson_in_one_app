using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Lesson
    {
        public DateTime Begin { get; private set;  }
        public string Name { get; set; }
        public event EventHandler<DateTime> Started;
        public Lesson(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public void Start() 
        {
            Begin = DateTime.Now;
            Started?.Invoke(this, Begin);
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
