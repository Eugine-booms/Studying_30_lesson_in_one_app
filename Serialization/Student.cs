using System;
using System.Runtime.Serialization;

namespace ConsoleApp2
{
    [DataContract, Serializable]
   public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public Group Group { get; set; }
        public Student(string name, int age)
        {
            if (age<0||DateTime.Now.Year-age<DateTime.Now.Year-100)
            {
                throw new ArgumentOutOfRangeException(nameof(age), "Не корректный возраст");
            }
            Age = age;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public Student() { }
        public override string ToString()
        {
            return $"{Name} {Age} лет"; 
        }
    }
}
