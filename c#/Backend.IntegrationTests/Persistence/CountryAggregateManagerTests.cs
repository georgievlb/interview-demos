using Backend.Application.Countries.Interfaces;
using Backend.Persistence;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.IntegrationTests.Persistence
{
    public class CountryAggregateManagerTests
    {
        private ICountryAggregateManager dbManager;

        [SetUp]
        public void SetUpDependencies()
        {
            dbManager = new CountryAggregateManager();
        }

        [Test]
        public async Task ShouldExecuteQuery()
        {
            var countries = await dbManager.GetCountriesAndPopulation();

            Assert.IsNotEmpty(countries);
        }

        [Test]
        public async Task ShouldGetAllCountriesPopulation()
        {
            const int CurrentNumberOfCountriesInTestDb = 16;

            var countries = (await dbManager.GetCountriesAndPopulation())
                .ToList();

            Assert.AreEqual(CurrentNumberOfCountriesInTestDb, countries.Count);
        }
    }
}
