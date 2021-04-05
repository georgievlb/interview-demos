using Backend.Persistence.Interfaces;
using Backend.Service.Interfaces;
using Backend.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class CountryService : ICountryService
    {
        private readonly IDbManager dbManager;
        private readonly ICountryNameMappingService countryNameMappingService;
        private readonly IStatService statService;

        public CountryService(IDbManager dbManager, ICountryNameMappingService countryNameMappingService, IStatService statService)
        {
            this.dbManager = dbManager;
            this.countryNameMappingService = countryNameMappingService;
            this.statService = statService;
        }

        public async Task<List<CountryDto>> GetCountriesFromDataSourceOne()
        {
            var countries = new List<CountryDto>();

            using (DbConnection conn = dbManager.GetConnection())
            {

                if (conn == null)
                {
                    Console.WriteLine("Failed to get connection");
                }

                await conn.OpenAsync();

                var command = conn.CreateCommand();

                command.CommandText = @"
SELECT co.CountryName, CAST(SUM(ci.Population) AS INT) AS CountryPopulation
FROM Country co
	INNER JOIN State s
	ON co.CountryId = s.CountryId
		INNER JOIN City ci
		ON s.StateId = ci.StateId
GROUP BY co.CountryName
";
                var rdr = await command.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    countries.Add(new CountryDto
                    {
                        Name = countryNameMappingService.MapCountryNameToIso3166(rdr.GetString(0)),
                        Population = rdr.GetInt64(1) 
                    });
                }
            }

            return countries;
        }

        public async Task<List<CountryDto>> GetCountriesFromDataSourceTwo()
        {
            var countriesRaw = await statService.GetCountryPopulationsAsync();

            return countriesRaw
                .Select((Tuple<string, int> c) =>
                {
                    var name = countryNameMappingService.MapCountryNameToIso3166(c.Item1);
                    var population = c.Item2;

                    return new CountryDto { Name = name, Population = population };
                })
                .ToList();
        }

    }
}
