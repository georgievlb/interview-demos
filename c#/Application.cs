using Backend.Service.Interfaces;
using System;

namespace Backend
{
    public class Application
    {
        private readonly IStatService statService;
        private readonly ICountryService countryService;

        public Application(IStatService statService, ICountryService countryService)
        {
            this.statService = statService;
            this.countryService = countryService;
        }

        public void Run()
        {
            var countries = countryService.GetCountriesFromDataSourceOne();

            foreach (var country in countries)
            {
                Console.WriteLine($"Name: {country.Name}, Population: {country.Population}");
            }
        }
    }
}
