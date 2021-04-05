using Backend.Persistence.Interfaces;
using System;
using System.Data.Common;
using System.Data.SQLite;

namespace Backend.Persistence
{
    public class SqliteDbManager : IDbManager
    {
        public DbConnection GetConnection()
        {
            try
            {
                return new SQLiteConnection("Data Source=Persistence\\citystatecountry.db;Version=3;FailIfMissing=True");

            }
            catch(SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
