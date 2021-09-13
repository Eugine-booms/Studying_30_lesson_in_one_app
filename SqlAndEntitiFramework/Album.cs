using System;
using System.Collections.Generic;

namespace LessonInOne.SqlAndEntitiFramework
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Year { get; set;}
        public int? YearInt { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public virtual ICollection<Song> Songs { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Year.Year}";
        }


    }
}