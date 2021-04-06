using Backend.Application.Countries.Interfaces;
using Backend.Domain.Countries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Backend.Persistence
{
    public class CountryAggregateManager : SqliteDbManager, ICountryAggregateManager
    {
        public async Task<IEnumerable<CountryAggregate>> GetCountriesAndPopulation()
        {
            const string GetAllCountriesWithPopulationQuery = @"
SELECT co.CountryId, co.CountryName, CAST(SUM(ci.Population) AS INT) AS CountryPopulation
FROM Country co
    INNER JOIN State s
    ON co.CountryId = s.CountryId
        INNER JOIN City ci
        ON s.StateId = ci.StateId
GROUP BY co.CountryName
";

            using (DbConnection conn = GetConnection())
            {
                if (conn == null)
                {
                    Console.WriteLine("Failed to get connection");
                }

                await conn.OpenAsync();

                var command = conn.CreateCommand();
                command.CommandText = GetAllCountriesWithPopulationQuery;

                var reader = await command.ExecuteReaderAsync();

                var countries = new List<CountryAggregate>();

                while (await reader.ReadAsync())
                {
                    countries.Add(new CountryAggregate
                    {
                        Country = new Country() { CountryId = reader.GetInt32(0), CountryName = reader.GetString(1) },
                        Population = reader.GetInt64(2)
                    });
                }

                return countries;
            }

        }
    }
}
