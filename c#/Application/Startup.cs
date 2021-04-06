using Backend.Application.Countries.Interfaces;
using System;
using System.Threading.Tasks;

namespace Backend.Application
{
    public class Startup
    {
        private readonly ICountryService countryService;

        public Startup(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        public async Task Run()
        {
            var countries = await countryService.GetCountriesAggregatedData();

            foreach (var country in countries)
            {
                Console.WriteLine($"Name: {country.Name}, Population: {country.Population}");
            }
        }
    }
}
