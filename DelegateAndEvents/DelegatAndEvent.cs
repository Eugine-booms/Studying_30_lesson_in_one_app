using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.DelegateAndEvents
{
    public class DelegatAndEvent : LessonBase
    {
        Lesson mathematika;
        public override void LessonMain()
        {
            Init();
            DelegatExample();
            DelegatTemplateExample();
            EventExample();
            EventDelegatExample();
        }
        /// <summary>
        /// шаблоны делегатов
        /// </summary>
        private void DelegatTemplateExample()
        {
            PrintConsole("DelegatTemplateExample", ConsoleColor.DarkRed);
            //шаблоны делегатов
            Predicate<int> predicate; //delegate bool name (int data);
            Action<string> consoleStringPrinter; //delegate void name(string data);
            Func<int, int, int> func = Multiplication;  //delegate int name(int x, int y);
            PrintConsole("Объявление делегата через Шаблоны ", ConsoleColor.DarkGreen);
            func += Multiplication;  //короткое объявление делегата сразу с возвращаемым значением
            func += Sum;
            func(4, 8);
            consoleStringPrinter = ConsolePrinter<string>; //короткое объявление делегата сразу без возвращаемого значением
            consoleStringPrinter("Это я Action<string> ConsoleStringPrinter = ConsolePrinter<string>;");
            predicate = ConsolePrinterWhithBool<int>;
            PrintConsole("А это я Predicate<int> predicate= ConsolePrinterWhithBool<int>; ");
            predicate(5);

        }

        //public delegate тип_возвращаемого_значения имя_делегата(тип_аргумента аргумент)
        private delegate int MyDelegate(int x, int y);
        private delegate void ConsoleIntPrinter(int x);

        private int Sum(int x, int y)
        {
            PrintConsole(x + " " + y + " Метод Summator результат=" + (x + y), ConsoleColor.DarkBlue);
            return x + y;
        }
        private int Multiplication(int x, int y)
        {
            PrintConsole(x + " " + y + " Метод Multiplication результат=" + (x * y), ConsoleColor.Green);
            return x * y;
        }
        private void ConsolePrinter<T>(T data)
        {
            Console.WriteLine(data);
        }
        private bool ConsolePrinterWhithBool<T>(T data)
        {
            Console.WriteLine(data);
            return true;
        }

        /// <summary>
        /// создание, добавление делегатов по сигнатуре
        /// </summary>
        private void DelegatExample()
        {

            PrintConsole("DelegatExample", ConsoleColor.DarkRed);

            MyDelegate myDelegate = new MyDelegate(Multiplication);
            //mydelegate = Sum;                       //при объявлении можно так если поставить = то все заменится на сумм
            myDelegate += Sum;                     //в дальнейшем только так
            MyDelegate myDelegate1 = Sum;
            MyDelegate myDelegate2 = myDelegate + myDelegate1;
            PrintConsole("MyDelegate myDelegate = new MyDelegate(Multiplication); \n MyDelegate myDelegate1 = Sum; " +
                            "\n MyDelegate myDelegate2 = myDelegate + myDelegate1; \n myDelegate2(3,2) Результат ");
            myDelegate2(3, 2);
            //вызов пустого делегата приведет к ошибке
            myDelegate1 -= Sum;
            ///Нужно делать проверку
            if (myDelegate1 !=null)
            {
                myDelegate1(5, 5);
            }
            ///Укороченая запись предыдущего 
            myDelegate1?.Invoke(5, 5);
            //////
            ///получения списка функций в делегате
            var invocationList = myDelegate.GetInvocationList();
            myDelegate(5, 3);
            myDelegate?.Invoke(5, 6); //аналогично предыдущей строчке 
        }

        private void EventDelegatExample()
        {
            PrintConsole("EventDelegatExample", ConsoleColor.DarkRed);
            mathematika.Started += (sender, e) =>
              {
                  PrintConsole("Обработка события с помощью делегата \n начало в " + e.TimeOfDay, ConsoleColor.Yellow);
              };
            mathematika.RingIsCalling+=()=> Console.WriteLine($"Звенит звонок прошло{mathematika.Begin - DateTime.Now} времени ");
            mathematika.Start();
            //не работает
            mathematika.Started -= (sender, e) => 
            {
                PrintConsole("Обработка события с помощью делегата \n начало в " + e.TimeOfDay, ConsoleColor.Yellow);
            };
            mathematika.Start();
            mathematika.CallRing();
        }

        private void EventExample()
        {

            PrintConsole("EventExample", ConsoleColor.DarkRed);
            mathematika.Started += Mathematika_Started;
            mathematika.Start();
            mathematika.Started -= Mathematika_Started;
            mathematika.RingIsCalling += () => Console.WriteLine("Повторим звонок прошло " + (mathematika.Begin-mathematika.End) +"времени");
            mathematika.CallRing();
            mathematika.Bang += (s, e) =>
            {
                if (s is Lesson)
                    global::System.Console.WriteLine($"{s} слышит стрельбу с  {e.Start} выстрелов {e.ShootCount}");
            };
            mathematika.BangBang();

        }

        protected override void Init()
        {
            mathematika = new Lesson("Математика");
        }

        private void Mathematika_Started(object sender, DateTime e)
        {
            PrintConsole("Я метод обработки события \nУрок математики начался в " + e.TimeOfDay, ConsoleColor.Green);
        }
    }
}
