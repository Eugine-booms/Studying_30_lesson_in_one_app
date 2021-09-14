using System.Collections.Generic;

namespace LessonInOne.SqlAndEntitiFramework
{
    public interface IDbController
    {
        DbMusicContext DbContext { get; }

        void AddSong(Song song);
        int DeleteSong(Song song);
        void PrintSongsList();
        List<Song> FindeSameSongs(string Name);
        Song FullSongDataQuestions(string songStr);
        void ChangeSong(Song song);
        Song FindeSongs(string addingSongName);
    }
}