using System;
using System.Collections;
using System.Collections.Generic;

namespace LessonInOne.Indexer_Enumerator
{

    public class FibonacciSequense : IEnumerable <double>
    {
        public IEnumerator<double> GetEnumerator()
        {
            double privious = 1;
            double curent = 1;
            yield return 1;
            yield return 1;
            while (true)
            {
                var newValue = curent + privious;
                privious = curent;
                curent = newValue;
                yield return curent;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
