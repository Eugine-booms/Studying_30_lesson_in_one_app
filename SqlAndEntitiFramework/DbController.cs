using LessonInOne.SqlAndEntitiFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.SqlAndEntitiFramework
{
    public class DbController
    {
        public DbMusicContext DbContext { get; }

        public DbController(DbMusicContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public int AddSong(string name, int duration, string albumName, int year, string groupName)
        {
            var groupId = addGroup(groupName);
            var albumId = addAlbum(albumName, year, groupId);
            if (DbContext.Songs.Any(x => x.Name == name))
            {
                return DbContext.Songs.ToList().SingleOrDefault(x => x.Name == name).Id;
            }
            else
            {
                DbContext.Songs.Add(new Song() { Name = name, Duration = duration, AlbomId = albumId });
                DbContext.SaveChanges();
                return DbContext.Songs.ToList().SingleOrDefault(x => x.Name == name).Id;
            }
        }
        public int addAlbum(string albumName, int year, int groupId)
        {
            if (DbContext.Albums.Any(x => x.Name == albumName))
            {
                return DbContext.Albums.ToList().SingleOrDefault(x => x.Name == albumName).Id;
            }
            else
            {
                DbContext.Albums.Add(new Album() { Name = albumName, GroupId = groupId, YearInt = year, Year = new DateTime(year, 1, 1) });
                DbContext.SaveChanges();
                return DbContext.Albums.ToList().SingleOrDefault(x => x.Name == albumName).Id;
            }
        }

        internal void AddSong(Song song)
        {
            using (DbContext)
            {
                DbContext.Songs.Add(song);
                DbContext.SaveChangesAsync();
            }

        }

        internal Song AddSongFuleQuastions(string songStr)
        {
            Console.WriteLine("Группу введи = ");
            string group = Console.ReadLine();
            Console.WriteLine("Длительность введи = ");
            int.TryParse(Console.ReadLine(), out int duration);
            Console.WriteLine("Альбом введи = ");
            var albumName = Console.ReadLine();
            Console.WriteLine("Год альбома введи = ");
            int.TryParse(Console.ReadLine(), out int year);
            return new Song()
            {
                Name = songStr,
                Duration = duration,
                Albom = new Album()
                {
                    Name = albumName,
                    YearInt = year,
                    Group = new Group()
                    {
                        Name = group
                    }
                }
            };
           
        }

        internal Song FindeSong(string Name)
        {
            using (DbContext)
            {

            var songs = DbContext.Songs.ToList();
            if (songs.Any(x=>x.Name.Contains(Name)))
            {
                return songs.SingleOrDefault(x => x.Name == Name);
            }
            return default;
            }
        }
        public IEnumerable<Song> getSongs()
        {
            var songs = new List<Song>();
            DbContext.Songs.Tol 
        }
        public int addAlbum(string albumName, int year, string groupName)
        {
            var GroupId = addGroup(groupName);
            if (DbContext.Albums.Where(x => x.Name == groupName) == null)
            {
                DbContext.Albums.Add(new Album() { Name = albumName, GroupId = GroupId, YearInt = year, Year = new DateTime(year, 0, 0) });
                DbContext.SaveChanges();
                return DbContext.Albums.ToList().SingleOrDefault(x => x.Name == albumName).Id;
            }
            else return DbContext.Albums.ToList().SingleOrDefault(x => x.Name == albumName).Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>GroupId</returns>
        public int addGroup(string groupName)
        {
            //var ifnull = DbContext.Groups.Any(x => x.Name == groupName);
            if (DbContext.Groups.Any(x => x.Name == groupName))
            {
                return DbContext.Groups.ToList().SingleOrDefault(x => x.Name == groupName).Id;
            }
            else
            {
                DbContext.Groups.Add(new Group() { Name = groupName });
                DbContext.SaveChanges();
                return DbContext.Groups.ToList().SingleOrDefault(x => x.Name == groupName).Id;
            }
        }
        private ConsoleKeyInfo YesOrNoQuestion(string questionString)
        {
            while (true)
            {
                Console.WriteLine(questionString);
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Y|| key.Key == ConsoleKey.N)
                   return key; 
            }
        }
        public int deleteSong(Song song)
        {
            using (DbContext)
            {
                //var song = DbContext.Songs.SingleOrDefault(s => s.Name == Name);

                string questionString = "вы действительно хотите удалить " + song + "группы " + song.Albom.Group + "y\\n ";
                var key = YesOrNoQuestion(questionString);
                switch (key.Key)
                {
                    case ConsoleKey.Y:
                        DbContext.Songs.Remove(song);
                        DbContext.SaveChangesAsync();
                        return -1;
                    case ConsoleKey.N:
                        return song.Id;
                }
                return -1;
            }
        }
    }

}

