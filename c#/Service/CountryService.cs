using Backend.Persistence.Interfaces;
using Backend.Service.Interfaces;
using Backend.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Backend.Service
{
    public class CountryService : ICountryService
    {
        private readonly IDbManager dbManager;
        private readonly ICountryNameMappingService countryNameMappingService;

        public CountryService(IDbManager dbManager, ICountryNameMappingService countryNameMappingService)
        {
            this.dbManager = dbManager;
            this.countryNameMappingService = countryNameMappingService;
        }

        public List<CountryDto> GetCountriesFromDataSourceOne()
        {
            var countries = new List<CountryDto>();

            using (DbConnection conn = dbManager.getConnection())
            {

                if (conn == null)
                {
                    Console.WriteLine("Failed to get connection");
                }

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
                var rdr = command.ExecuteReader();

                while (rdr.Read())
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

    }
}
