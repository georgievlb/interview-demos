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
            var countries1 = await countryService.GetCountriesFromDataSourceOne();

            foreach (var country in countries1)
            {
                Console.WriteLine($"Name: {country.Name}, Population: {country.Population}");
            }

            Console.WriteLine("-----------------------------------------------");

            var countries2 = await countryService.GetCountriesFromDataSourceTwo();

            foreach (var country in countries2)
            {
                Console.WriteLine($"Name: {country.Name}, Population: {country.Population}");
            }
        }
    }
}
