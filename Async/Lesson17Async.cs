using LessonInOne.Indexer_Enumerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LessonInOne.Async
{
    public class Lesson17Async : LessonBase
    {
        /// <summary>
        /// локер для блокировки одновременного доступа к файлу
        /// </summary>
        public static object locker = new object();
        public override void LessonMain()
        {
            ThreadUsingExample();
            AsyncAndTaskExample();
            BigFileSaveExample();
            Console.ReadLine();
        }
        private void BigFileSaveExample()
        {
            PrintConsole("Мы начали формировать строку!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!", ConsoleColor.Yellow);
            var str = string.Join(", ", Sequences.Fibonacci.Take(50000000).Select(x => x.ToString()));
            PrintConsole("Строка сформирована, Милорд!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!", ConsoleColor.Yellow);
            // FileSave("fibonacci.txt", str);
            FileSaveAsync("fibonacci.txt", str);
            FileSaveSerialisationAsync("fibonacci.dat", str);
        }

        async Task FileSaveSerialisationAsync(string path, string data)
        {
            await Task.Run(() => FileSaveSerialisation(path, data));
        }

        async Task FileSaveAsync(string path, string data)
        {
            await Task.Run(() => FileSave(path, data));
        }
        private void FileSave(string path, string data)
        {
            lock (locker)
            {
                PrintConsole("Мы начали запись в файл !!!!!!!!!!!!!!!!!!!!", ConsoleColor.White);
                using (var streamWriter = new StreamWriter(path, false, Encoding.UTF8))
                {
                    streamWriter.WriteLine(data);
                }
                PrintConsole("Мы закончили запись в файл !!!!!!!!!!!!!!!!!!!!", ConsoleColor.White);
            }
        }
        private void FileSaveSerialisation(string path, string data)
        {
            lock (locker)
            {
                PrintConsole("Мы начали сериализацию в  файл !!!!!!!!!!!!!!!!!!!!", ConsoleColor.White);
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    var binaryFormater = new BinaryFormatter();
                    binaryFormater.Serialize(fileStream, data);
                }
                PrintConsole("Мы закончили сериализацию в  файл !!!!!!!!!!!!!!!!!!!!", ConsoleColor.White);
            }
        }

        private void AsyncAndTaskExample()
        {
            DoWorkAsync();
            var i = DoWorkWhitParamAsync(1, 10);
            var j = DoWorkWhitParamAsync(2, 15, false);

            if (i.Result == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I did it!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            if (j.Result == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("I did it again!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        async Task<int> DoWorkWhitParamAsync(int delay, int number)
        {
            Console.WriteLine(" DoWorkWhitParamAsync start");
            await Task.Run(() => DoWorkWhitParam((delay: delay, number: number)));
            Console.WriteLine(" DoWorkWhitParamAsync stop");
            return 1;
        }
        async Task DoWorkAsync()
        {
            Console.WriteLine(" DoWorkAsync start");
            await Task.Run(() => DoWork());
            Console.WriteLine(" DoWorkAsync stop");
        }
        private void ThreadUsingExample()
        {
            Thread doWorkThread = new Thread(new ThreadStart(DoWork));
            doWorkThread.Start();
            Thread doWorkWhithParametrThead = new Thread(new ParameterizedThreadStart(DoWorkWhitParam));
            doWorkWhithParametrThead.Start((delay: 1, number: 10));
            var mainTread = Sequences.Fibonacci;

        }
        async Task<int> DoWorkWhitParamAsync(int dalay, int number, bool param)
        {
            Console.WriteLine("DoWorkWhitParamNew start");
            await Task.Run(() => DoWorkWhitParam(dalay, number));
            Console.WriteLine("DoWorkWhitParamNew stop");
            return 1;
        }
        private void DoWorkWhitParam(int dalay, int number)
        {
            var fibonacci = Sequences.Fibonacci;
            var i = 0;
            foreach (var item in fibonacci)
            {
                Thread.Sleep(dalay * 100);
                Console.WriteLine($"DoWorkWhitParamNew Фибоначи {i++} - {item}");
                if (i == number)
                {
                    break;
                }
            }
        }
        private void DoWorkWhitParam(object param)
        {
            var tuple = (ValueTuple<int, int>)param;
            Console.WriteLine("DoWorkWhitParam start");
            var fibonacci = Sequences.Fibonacci;
            var i = 0;
            foreach (var item in fibonacci)
            {
                Thread.Sleep(tuple.Item1 * 100);
                Console.WriteLine($"DoWorkWhitParam Фибоначи {i++} - {item}");
                if (i == tuple.Item2)
                {
                    Console.WriteLine("DoWorkWhitParam stop");
                    break;
                }
            }

        }
        private void DoWork()
        {
            Console.WriteLine("DoWork start");
            var fibonacci = Sequences.Fibonacci;
            var i = 0;
            foreach (var item in fibonacci)
            {
                Thread.Sleep(500);
                Console.WriteLine($"DoWork Фибоначи {i++} - {item}");
                if (Console.KeyAvailable)
                {
                    break;
                }
            }
        }
        protected override void Init()
        {
            throw new NotImplementedException();
        }
    }
}
