using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.SqlAndEntitiFramework
{
    /// <summary>
    /// Создание пользовательского контекста, 
    /// в файле App.config прописываем name - совпадает со строкой переданной в базовый конструктор 
    /// 
    /// <connectionStrings>
    /// <add name = "DBConnection"
    ///      providerName="System.Data.SqlClient"
    ///      connectionString="Data Source=.\SQLEXPRESS; Initial Catalog=Vector;Integrated Security=True; "/>
    /// </connectionStrings>
    /// далее в диспетчере пакетов три команды 
    /// enable-migrations
    /// add-migration migration_name
    /// update-database
    /// </summary>
    //TODO : поэкспериментировать с несколькими базами и разными строками подключения например тестовой и боевой
    public class DbMusicContext : DbContext
    {
        public DbMusicContext() : base("DbConnection")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }


    }
}
