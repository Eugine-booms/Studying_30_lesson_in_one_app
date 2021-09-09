using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.Indexer_Enumerator
{
    public class Parking : IEnumerable
    {
        private List<Car> _cars = new List<Car>();
        private const int MAX_CARS = 100;
        public string Name { get; set; }
        //пример индексатора по номеру
        /// <summary>
        /// Индексатор по номеру автомобиля типа стринг
        /// главная фича - this вместо имени метода и обязательный сеттр ;
        /// </summary>
        /// <param name="number"> Номер автомобиля</param>
        /// <returns></returns>
        public Car this[string number]
        {
            get
            {
                return _cars.FirstOrDefault(c => c.Number == number);
            }
        }
        /// <summary>
        /// Индексатор по номеру индекса с проверкой на OutOfRangeExeption
        /// главная фича - this вместо имени метода и обязательный сеттр. 
        /// </summary>
        /// <param name="i">Indexer </param>
        /// <returns></returns>
        public Car this[int i]
        {
            get
            {
                if (i < Count)
                {
                    return _cars[i];
                }
                else
                    return null;
            }
            set
            {
                if (i < Count)
                {

                    _cars[i] = value;
                }
            }
        }
        public override string ToString()
        {
            var result = string.Empty;
            var indexer  = 0; 
            foreach (var item in _cars)
            {
                result += $"{++indexer} { item }, ";
            }
            return result;
        }

        public int Count => _cars.Count;
        public int Add(Car car)
        {
            if (car == null)
            {
                throw new ArgumentException(nameof(car), "car is null");
            }
            if (_cars.Count < MAX_CARS)
            {
                _cars.Add(car);
                return Count - 1;
            }
            else
            {
                return -1;     
            }
        }
        /// <summary>
        /// Выпускает машину с парковки по номеру машины
        /// </summary>
        /// <param name="number"> Номер машины</param>
        /// <returns></returns>
        public int GoOut(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number), "Number is null or empty");
            }
            var car = _cars.FirstOrDefault(c => c.Number == number);
            if (car != null)
            {
                _cars.Remove(car);
                return Count;
            }else
            {
                return -1;
            }
            
        }
        /// <summary>
        /// Нумератор класаа Parking через нумератор List<Car>
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var car in _cars)
            {
                yield return car;
            }

        }
        /// <summary>
        /// Создали свой Нумератор возвращающий марки всех машин
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetNames()
        {
            foreach (var car in _cars)
            {
                yield return car.Name;
            }
        }
        /// <summary>
        /// Создали свой Нумератор возвращающий номера всех машин
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetNumbers()
        {
            var curent = _cars[0].Number;
            for (int i = 0; i < _cars.Count; i++)
            {
                curent = _cars[i].Number;
                yield return curent;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Реализуем интерфейс IEnumerable c помощью yeld return через индексатор
        /// возвращаем через yield return все элементы последовательно
        /// </summary>
        /// <returns></returns>
        //public IEnumerator GetEnumerator()
        //{
        //   // var current = _cars[0];
        //    int i = 0;
        //    while (_cars.Count>i)
        //    {
        //        yield return _cars[i++];
        //    }
        //}
       
    }
}
