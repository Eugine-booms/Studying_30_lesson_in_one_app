using System;
using System.Collections.Generic;

namespace LessonInOne.SqlAndEntitiFramework
{
    public class Group
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        
        public virtual ICollection<Album> Alboms { get; set; }
        public override string ToString()
        {
            return Name;
        }

    }
}