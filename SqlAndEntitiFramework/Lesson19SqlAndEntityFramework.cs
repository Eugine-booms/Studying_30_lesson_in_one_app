using LessonInOne.ExtensionMethods;
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
            IDbController dbController = new DbController(new DbMusicContext());
            using (dbController.DbContext)
            {
                var dbController = new DbController(control);
                //var t= control.SaveChangesAsync();
                //dbController.AddSong("You Can't always get what you want ", 377, "Let It Bleed", 1969, "The rolling Stones");
                //dbController.AddSong("Снег идет", 313, "Троды плудов", 1994, "Несчастный случай");
                //dbController.AddSong("Dream On", 300, "Aerosmith's Greatest Hits", 1980, "Aerosmith");
                //dbController.AddSong("Shape of my heart", 232, "Sting Greatest Hits", 2000, "Sting");
                dbController.PrintSongsList();
                while (true)
                {
                    PrintMenu();
                    var key = MenuQuestion("\n Выбери число =");
                    switch (key.Key)
                    {
                        case ConsoleKey.D1: // добавить песню
                            var addingSongName = SongNameQuestion();
                            if (!dbController.FindeSongs(addingSongName).IsNull())
                            {
                                PrintLineConsole("Такая песня уже есть");
                            }
                            else
                            {
                                var song = dbController.FullSongDataQuestions(addingSongName);
                                dbController.AddSong(song);
                            }
                            break;
                        case ConsoleKey.D2: //изменить песню
                            var changSongName = SongNameQuestion();
                            var changSongs = dbController.FindeSameSongs(changSongName);
                            if (changSongs.Count == 1)
                            {
                                dbController.ChangeSong(changSongs.Single());
                                var changSong = dbController.FullSongDataQuestions(changSongName);
                            }
                            else if (changSongs.Count == 0)
                                PrintConsole("Песня не найдена \n");
                            else if (changSongs.Count > 1)
                            {
                                Console.WriteLine($"Найдено {changSongs.Count} песен ");
                                foreach (var song in changSongs)
                                {
                                    Console.WriteLine(song + " - Группа -" + song.Albom.Group);
                                }
                                Console.WriteLine("Будьте точнее");
                            }
                            break;
                        case ConsoleKey.D3: //удалить 
                            DeleteSongDialog(dbController);
                            break;
                        case ConsoleKey.D4: //Вывести лист песен 
                            dbController.PrintSongsList();
                            break;

                        default:
                            break;
                    }

                }
            }
        }

        private void DeleteSongDialog(IDbController dbController)
        {
            var deletingsongName = SongNameQuestion();
            var deletingSongs = dbController.FindeSameSongs(deletingsongName);
            if (deletingSongs.Count > 0)
            {
                foreach (var song in deletingSongs)
                {
                    dbController.ChangeSong(song, true);
                }
            }
            else
                PrintLineConsole("Песня не найдена");
        }

        private string SongNameQuestion()
        {
            PrintConsole("\n Введите название песни = ", ConsoleColor.DarkGreen);
            return Console.ReadLine();
        }

        private void PrintMenu()
        {
            PrintLineConsole("\n1 - Добавить песню \n2 - Изменить песню \n3 - Удалить песню \n4 - Список песен");
        }
        private ConsoleKeyInfo MenuQuestion(string questionString)
        {
            while (true)
            {
                Console.Write(questionString);
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.D4)
                    return key;
            }
        }


        protected override void Init()
        {
            ;
        }
    }
}