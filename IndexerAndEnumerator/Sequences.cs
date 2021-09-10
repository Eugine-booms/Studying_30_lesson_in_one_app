using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.Indexer_Enumerator
{
   public class Sequences
    {
        static public IEnumerable<double> Fibonacci => new FibonacciSequense();
    }
}
