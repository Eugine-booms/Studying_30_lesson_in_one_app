using LessonInOne.SqlAndEntitiFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.SqlAndEntitiFramework
{
    public class DbController : IDbController
    {
        public DbMusicContext DbContext { get; }
        public DbController(DbMusicContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _ = this.GetGroup();
            _ = this.GetAlbum();
            _ = this.GetSongs();
        }
        public void AddSong(Song song) => AddSong(song.Name, song.Duration, song.Albom.Name, song.Albom.YearInt, song.Albom.Group.Name);
        public void PrintSongsList()
        {
            var songs = GetSongs();
            LessonBase.PrintLineConsole("\n В нашей базе данных следующие песни", System.ConsoleColor.DarkCyan);
            LessonBase.PrintLineConsole("Название  - Продолжительность              | \t Альбом - год   \t    | \t           Группа               | ");
            foreach (var song in songs)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(" {0,40}  | {1,30} |  {2,30} |", song.ToString(), song.Albom.ToString(), song.Albom.Group.ToString());
                Console.ResetColor();
            }
        }
        public Song FullSongDataQuestions(string songStr) //корявая фигня
        {
            #region 
            Console.Write("Группу введи = ");
            string group = Console.ReadLine();
            Console.Write("Длительность введи = ");
            int.TryParse(Console.ReadLine(), out int duration);
            Console.Write("Альбом введи = ");
            var albumName = Console.ReadLine();
            Console.Write("Год альбома введи = ");
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
            #endregion
            #region Testing


            //for testing
            /*
            return new Song()
            {
                Name = songStr,
                Duration = 375,
                Albom = new Album()
                {
                    Name = "Mafia 2 OST",
                    YearInt = 2010,
                    Group = new Group()
                    {
                        Name = "Louis Prima"
                    }
                }
            };*/
            #endregion
        }
        public List<Song> FindeSameSongs(string Name)
        {
            return DbContext.Songs.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();
        }
        //public int DeleteSong(Song song)
        //{
        //    string questionString = "\n вы действительно хотите удалить " + song + " группы " + song.Albom.Group + " y/n ";
        //    var key = YesOrNoQuestion(questionString);
        //    switch (key.Key)
        //    {
        //        case ConsoleKey.Y:
        //            DbContext.Songs.Remove(song);
        //            DbContext.SaveChangesAsync();
        //            return -1;
        //        case ConsoleKey.N:
        //            return song.Id;
        //    }
        //    return -1;
        //}

            public int ChangeSong(Song song, bool delete = false)
            {
                string del = string.Empty;
                if (delete)
                    del = "удалить";
                else
                    del = "изменить";
                string questionString = "\n вы действительно хотите " + del + " " + song + " группы " + song.Albom.Group + " y/n ";
                var key = YesOrNoQuestion(questionString);
                switch (key.Key)
                {
                    case ConsoleKey.Y:
                        if (delete)
                        {
                            DbContext.Songs.Remove(song);
                            DbContext.SaveChangesAsync();
                        }
                        else
                        {
                            var s = DbContext.Songs.Single(x => x.Id == song.Id);
                            Console.Write("Введи новое имя = ");
                            s.Name = Console.ReadLine();
                            Console.Write("Введи новую длину  = ");
                            int.TryParse(Console.ReadLine(), out int duration);
                            s.Duration = duration;
                            DbContext.SaveChangesAsync();
                        }
                        return -1;
                case ConsoleKey.N:
                    return song.Id;
            }
            return -1;
        }
        public Song FindeSongs(string FindingSongName)
        {
            return DbContext.Songs.SingleOrDefault(x => x.Name.ToLower().Equals(FindingSongName.ToLower()));
        }
        private IEnumerable<Song> GetSongs()
        {
            return DbContext.Songs.ToList(); ;
        }
        private IEnumerable<Group> GetGroup()
        {
            return DbContext.Groups.ToList();
        }
        private IEnumerable<Album> GetAlbum()
        {
            return DbContext.Albums.ToList();
        }
        private ConsoleKeyInfo YesOrNoQuestion(string questionString)
        {
            while (true)
            {
                Console.Write(questionString);
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Y || key.Key == ConsoleKey.N)
                    return key;
            }
        }
        private int AddSong(string name, int? duration, string albumName, int? year, string groupName)
        {
            var groupId = AddGroup(groupName);
            var albumId = AddAlbum(albumName, year, groupId);
            if (DbContext.Songs.Any(x => x.Name == name))
            {
                return DbContext.Songs.SingleOrDefault(x => x.Name == name)
                                      .Id;
            }
            else
            {
                DbContext.Songs.Add(new Song()
                {
                    Name = name,
                    Duration = duration,
                    AlbomId = albumId
                });
                DbContext.SaveChanges();
                return DbContext.Songs.SingleOrDefault(x => x.Name == name)
                                      .Id;
            }
        }
        private int AddGroup(string groupName)
        {

            if (DbContext.Groups.Any(x => x.Name == groupName))
            {
                return DbContext.Groups.SingleOrDefault(x => x.Name == groupName)
                                       .Id;
            }
            else
            {
                DbContext.Groups.Add(new Group() { Name = groupName });
                DbContext.SaveChanges();
                return DbContext.Groups.SingleOrDefault(x => x.Name == groupName)
                                       .Id;
            }
        }
        private int AddAlbum(string albumName, int? year, int groupId)
        {
            if (DbContext.Albums.Any(x => x.Name == albumName))
            {
                return DbContext.Albums.SingleOrDefault(x => x.Name == albumName)
                                       .Id;
            }
            else
            {
                DbContext.Albums.Add(new Album()
                {
                    Name = albumName,
                    GroupId = groupId,
                    YearInt = year,
                    Year = new DateTime(year ?? default(int), 1, 1)
                });
                DbContext.SaveChanges();
                return DbContext.Albums.SingleOrDefault(x => x.Name == albumName)
                                       .Id;
            }
        }
        private int AddAlbum(string albumName, int year, string groupName)
        {
            var GroupId = AddGroup(groupName);
            if (DbContext.Albums.Where(x => x.Name == groupName) == null)
            {
                DbContext.Albums.Add(
                    new Album()
                    {
                        Name = albumName,
                        GroupId = GroupId,
                        YearInt = year,
                        Year = new DateTime(year, 0, 0)
                    }
                );
                DbContext.SaveChanges();
                return DbContext.Albums.SingleOrDefault(x => x.Name == albumName)
                                       .Id;
            }
            else return DbContext.Albums.SingleOrDefault(x => x.Name == albumName)
                                        .Id;
        }
    }
}

