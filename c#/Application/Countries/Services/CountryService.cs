using Backend.Application.Countries.Interfaces;
using Backend.Application.Countries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Countries.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryAggregateManager dbManager;
        private readonly ICountryNameMappingService countryNameMappingService;
        private readonly IStatService statService;

        public CountryService(ICountryAggregateManager dbManager, ICountryNameMappingService countryNameMappingService, IStatService statService)
        {
            this.dbManager = dbManager;
            this.countryNameMappingService = countryNameMappingService;
            this.statService = statService;
        }

        public async Task<List<CountryAggregateModel>> GetCountriesAggregatedData()
        {
            var datasourceOneDataRaw = await dbManager.GetCountriesAndPopulation();
            var datasourceOneData = datasourceOneDataRaw
                .Select(c => new CountryAggregateModel()
                {
                    Name = countryNameMappingService.MapCountryNameToIso3166(c.Country.CountryName),
                    Population = c.Population
                });

            var datasourceTwoDataRaw = await statService.GetCountryPopulationsAsync();
            var datasourceTwoData = datasourceTwoDataRaw
                .Select((Tuple<string, int> c) =>
                    {
                        var name = countryNameMappingService.MapCountryNameToIso3166(c.Item1);
                        var population = c.Item2;

                        return new CountryAggregateModel()
                        {
                            Name = name,
                            Population = population
                        };
                    }
                 )
                .ToList();

            return datasourceOneData
                .Union(datasourceTwoData)
                .Select(c => new CountryAggregateModel() { Name = c.Name, Population = c.Population })
                .OrderBy(c => c.Name)
                .ToList();
        }

    }
}
