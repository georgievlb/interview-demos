using Backend.Application.Countries.Interfaces;
using Backend.Common;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;

namespace Backend.Persistence
{
    public abstract class SqliteDbManager : IDbManager
    {
        public DbConnection GetConnection()
        {
            try
            {
                ConnectionStringSettings settings =  ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName];

                return new SQLiteConnection(settings.ConnectionString);

            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (SQLiteException)
            {
                throw;
            }
        }
    }
}
