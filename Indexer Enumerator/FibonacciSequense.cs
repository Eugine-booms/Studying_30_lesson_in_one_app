using System;
using System.Collections;
using System.Collections.Generic;

namespace LessonInOne.Indexer_Enumerator
{

    public class FibonacciSequense : IEnumerable <double>
    {

        /// <summary>
        /// Enumerator для создания последовательности фибоначи.
        /// Первые два yield return возвращают первые 2 члена последовательности,
        /// а далее бесконечный цикл 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Стандартная фишка осталась с прошлых версий. 
        /// всегда возвращать енумиратор и не паритьсяю
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
