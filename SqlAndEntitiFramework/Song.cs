namespace LessonInOne.SqlAndEntitiFramework
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Duration { get; set; }
        public int AlbomId { get; set; }
        public Album Albom { get; set; }



        public override string ToString()
        {
            return $"{Name} - {((int)Duration/60).ToString()}:{(Duration%60).ToString()}";
        }

    }
}