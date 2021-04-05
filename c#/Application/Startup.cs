using Backend.Application.Countries.Interfaces;
using NLog;
using System;
using System.Threading.Tasks;

namespace Backend.Application
{
    public class Startup
    {
        private readonly ICountryService countryService;
        private readonly ILogger logger;

        public Startup(ICountryService countryService, ILogger logger)
        {
            this.countryService = countryService;
            this.logger = logger;
        }

        public async Task Run()
        {
            try
            {
                var countries = await countryService.GetCountriesAggregatedData();
                logger.Info($"Called: {nameof(ICountryService)} with method {nameof(countryService.GetCountriesAggregatedData)}.");

                foreach (var country in countries)
                {
                    Console.WriteLine($"Name: {country.Name}, Population: {country.Population}");
                }

            }
            catch (Exception ex)
            {
                logger.Error($"Message: {ex.Message}\n{ex.StackTrace}\n");
                Console.WriteLine($"An error has occured. Please check the application logs.");

                return;
            }
        }
    }
}
