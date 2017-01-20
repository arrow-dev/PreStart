using SQLite;

namespace PreStart.Models
{
    public class LocalData
    {
        public SQLiteAsyncConnection Database;
        public LocalData(string dbPath)
        {
            Database=new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<Location>().Wait();
        }
    }
}
