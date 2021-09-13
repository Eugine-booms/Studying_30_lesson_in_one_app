using LessonInOne.SqlAndEntitiFramework;
using System;
using System.Linq;

namespace LessonInOne
{

    internal class Lesson19SqlAndEntityFramework : LessonBase
    {
        //private DbMusicContext dbContext;
        public Lesson19SqlAndEntityFramework()
        {
            Init();
        }

        public override void LessonMain()
        {
            

            using (var control= new DbMusicContext())
            {
                var dbController = new DbController(control);
                //var t= control.SaveChangesAsync();
                //dbController.AddSong("You Can't always get what you want ", 377, "Let It Bleed", 1969, "The rolling Stones");
                //dbController.AddSong("Снег идет", 313, "Троды плудов", 1994, "Несчастный случай");
                //dbController.AddSong("Dream On", 300, "Aerosmith's Greatest Hits", 1980, "Aerosmith");
                //dbController.AddSong("Shape of my heart", 232, "Sting Greatest Hits", 2000, "Sting");
                PrintConsole("В нашей базе данных следующие песни", System.ConsoleColor.DarkCyan);
                PrintConsole("Название         |  Продолжительность |  Альбом год   |  Группа  ");
                var groups= dbController.DbContext.Groups.Any(x => x.Name == "Снег");
                var albums = control.Albums.Select(x=>x);
                var songs = control.Songs.Select(x => x);

                foreach (var song in songs)
                {
                    PrintConsole($"{song} \t{song.Albom}\t {song.Albom.Group}", System.ConsoleColor.DarkBlue);
                }

                while (true)
                {
                    PrintMenu();
                    var key = MenuQuation("\n Выбери число");
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            var songStr = SongNameQuestion();
                            if (dbController.FindeSong(songStr) != default)
                                PrintConsole("Такая песня уже есть");
                            dbController.AddSongFuleQuastions(songStr);
                            break;
                        case ConsoleKey.D2:

                            break;
                        case ConsoleKey.D3:
                            var song1 = dbController.FindeSong(SongNameQuestion());
                            dbController.deleteSong(song1);
                            break;

                        default:
                            break;
                    }

                }

            }
        }

        private string SongNameQuestion()
        {
            PrintConsole("Введите название песни", ConsoleColor.DarkGreen);
            return Console.ReadLine();
        }

        private void PrintMenu()
        {
            PrintConsole("1 - Добавить песню \n2-Изменить песню \n3 - Удалить песню \n");
        }
        private ConsoleKeyInfo MenuQuation(string questionString)
        {
            while (true)
            {
                Console.WriteLine(questionString);
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.D3)
                    return key;
            }
        }


        protected override void Init()
        {
            ;
        }
    }
}