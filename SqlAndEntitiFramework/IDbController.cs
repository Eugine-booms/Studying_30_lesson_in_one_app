using System.Collections.Generic;

namespace LessonInOne.SqlAndEntitiFramework
{
    public interface IDbController
    {
        DbMusicContext DbContext { get; }

        void AddSong(Song song);
      //  int DeleteSong(Song song);
        void PrintSongsList();
        List<Song> FindeSameSongs(string Name);
        Song FullSongDataQuestions(string songStr);
        int ChangeSong(Song song, bool isDel = false);
        Song FindeSongs(string addingSongName);
    }
}