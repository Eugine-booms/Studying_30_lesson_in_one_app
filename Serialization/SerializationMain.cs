using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LessonInOne.Serialization
{
    public static class SerializationMain
    {
        static Random rnd = new Random();
        static List<Group> groups  = new List<Group>();
        static List<Student> students = new List<Student>();
        static public void Init()
        {
            for (int i = 1; i < 10; i++)
            {
                groups.Add(new Group(i * 100, "Группа " + i * 100));
            }
            for (int i = 0; i < 5000; i++)
            {
                var student = new Student(Guid.NewGuid().ToString().Substring(0, 5), rnd.Next(18, 30))
                {
                    Group = groups[i % 9]
                };
                students.Add(student);
            }
        }
        internal static void SerializMainXML()
        {
            Serializator.XMLSerializator<List<Group>>(groups, "group1.XML");
            Serializator.XMLSerializator<List<Student>>(students, "student.XML");
            
            var LoadStudent = Serializator.XMLDeserializator<List<Student>>("student.XML") ??
                                throw new ArgumentNullException("файл student.XML пустой");
            var LoadGroups = Serializator.XMLDeserializator<List<Group>>("group1.XML") ??
                            throw new ArgumentNullException("файл group.XML пустой");
            PrintCollection(LoadGroups, "XML");
            PrintStudent(LoadStudent, "XML");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("XML");
            Console.ReadKey();
        }
        internal static void SerializMainJSON()
        {
            Serializator.JsonSerializator<List<Group>>(groups, "group.json");
            Serializator.JsonSerializator<List<Student>>(students, "student.json");
            var LoadGroups = Serializator.JsonDeserializator<List<Group>>("group.json") ??
                            throw new ArgumentNullException("файл group.json пустой");
            var LoadStudent = Serializator.JsonDeserializator<List<Student>>("student.json") ??
                                throw new ArgumentNullException("файл student.json пустой");
            var LoadStudent1 = new List<Student>();
            PrintCollection(LoadGroups, "json");
            PrintStudent(LoadStudent, "json");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("json");
            Console.ReadKey();
        }
        internal static void SerializMainSOAP()
        {
            //Не умеет работать с List по этому переводим в Array
            Serializator.SoapSerializator(groups.ToArray(), "group.soap");
            Serializator.SoapSerializator(students.ToArray(), "student.soap");
            //Возвращаем тоже Array т.к. не умеет в List/ 
            var LoadGroups = Serializator.SoapDeserializator<Group[] >("group.soap") ??
                            throw new ArgumentNullException("файл group.soap пустой");
            var LoadStudent = Serializator.SoapDeserializator<Student []>("student.soap") ??
                                throw new ArgumentNullException("файл student.soap пустой");
            PrintCollection(LoadGroups, "Soap");
            //Превращаем Array в List т.к. не хотим писать функцию вывода для array KISS 
            PrintStudent(new List<Student>(LoadStudent).OrderBy(stud => stud.Group.Number).ToList(), "Soap");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SOAP");
            Console.ReadKey();
        }
        internal static  void SerializMainBinary()
        {
            Serializator.BinarySerializator(groups, "group.bin");
            Serializator.BinarySerializator(students, "student.bin");
            var LoadGroups = Serializator.BinaryDeserializator<List<Group>>("group.bin")??
                             throw new ArgumentNullException("файл group.bin пустой");
            var LoadStudent = Serializator.BinaryDeserializator<List<Student>>("student.bin")?? 
                                throw new ArgumentNullException("файл student.bin пустой");
            LoadStudent = LoadStudent.OrderBy(s => s.Group.Number).ToList();
            PrintCollection(LoadGroups, "Binary");
            PrintStudent(LoadStudent, "Binary");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Binary");
            Console.ReadKey();
        }
        private static void PrintCollection(IEnumerable collection, string serializationMetodName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(serializationMetodName);
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
        private static void PrintStudent(List<Student> students, string serializationMetodName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(serializationMetodName);
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (var student in students)
            {

                Console.WriteLine($"Группа {student.Group.Number} Студент {student} ");
            }
        }
    }
}
