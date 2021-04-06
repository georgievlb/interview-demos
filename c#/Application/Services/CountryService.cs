using Backend.Application.Interfaces;
using Backend.Application.Models;
using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IDbManager<Country> dbManager;
        private readonly ICountryNameMappingService countryNameMappingService;
        private readonly IStatService statService;

        private const string query = @"
SELECT co.CountryName, CAST(SUM(ci.Population) AS INT) AS CountryPopulation
FROM Country co
    INNER JOIN State s
    ON co.CountryId = s.CountryId
        INNER JOIN City ci
        ON s.StateId = ci.StateId
GROUP BY co.CountryName
";

        public CountryService(IDbManager<Country> dbManager, ICountryNameMappingService countryNameMappingService, IStatService statService)
        {
            this.dbManager = dbManager;
            this.countryNameMappingService = countryNameMappingService;
            this.statService = statService;
        }

        public async Task<List<CountryModel>> GetCountriesAggregatedData()
        {
            var datasourceOneDataRaw = await dbManager.ExecuteQuery(query);
            var datasourceOneData = datasourceOneDataRaw
                .Select(c => new Country() { Name = countryNameMappingService.MapCountryNameToIso3166(c.Name), Population = c.Population });

            var datasourceTwoDataRaw = await statService.GetCountryPopulationsAsync();
            var datasourceTwoData = datasourceTwoDataRaw
                .Select((Tuple<string, int> c) =>
                {
                    var name = countryNameMappingService.MapCountryNameToIso3166(c.Item1);
                    var population = c.Item2;

                    return new Country{ Name = name, Population = population };
                })
                .ToList();

            return datasourceOneData
                .Union(datasourceTwoData)
                .Select(c => new CountryModel() { Name = c.Name, Population = c.Population })
                .OrderBy(c => c.Name)
                .ToList();
        }

    }
}
