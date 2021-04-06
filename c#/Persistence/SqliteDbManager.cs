using Backend.Application.Interfaces;
using Backend.Common;
using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Backend.Persistence
{
    public class SqliteDbManager : IDbManager<Country>
    {
        public DbConnection GetConnection()
        {
            try
            {
                ConnectionStringSettings settings =  ConfigurationManager.ConnectionStrings[Constants.connectionString];

                return new SQLiteConnection(settings.ConnectionString);

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Connection string was not found. The error was: {ex.Message}");
                return null;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Country>> ExecuteQuery(string query)
        {
            using (DbConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    Console.WriteLine("Failed to get connection");
                }

                await conn.OpenAsync();

                var command = conn.CreateCommand();
                command.CommandText = query;

                var reader = await command.ExecuteReaderAsync();

                var countries = new List<Country>();

                while (await reader.ReadAsync())
                {
                    countries.Add(new Country
                    {
                        Name = reader.GetString(0),
                        Population = reader.GetInt64(1)
                    });
                }

                return countries;
            }
        }
    }
}
