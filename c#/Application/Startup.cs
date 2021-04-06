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
            try
            {
                var countries = await countryService.GetCountriesAggregatedData();

                foreach (var country in countries)
                {
                    Console.WriteLine($"Name: {country.Name}, Population: {country.Population}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"The following error occured: {ex.Message} at \n {ex.StackTrace}");
                Console.WriteLine($"{ex.InnerException.Message} at \n {ex.InnerException.StackTrace}");
                return;
            }
        }
    }
}
