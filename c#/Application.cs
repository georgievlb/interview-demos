using Backend.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Backend
{
    public class Application
    {
        private readonly ICountryService countryService;

        public Application(ICountryService countryService)
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
