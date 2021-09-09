using System;
using System.Linq;
using System.Runtime.Serialization;

namespace LessonInOne.Serialization
{
    [DataContract, Serializable]
    public class Group
    {
        [NonSerialized]
        private Random rnd = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public string Name { get; set; }
        public Group()
        {
            Number = rnd.Next(100, 1000);
            Name = "Группа" + rnd;
        }
        public Group(int number, string name)
        {
            
            Number = number;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
