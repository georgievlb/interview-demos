using Backend.Application.Interfaces;
using Backend.Application.Models;
using Backend.Common;
using Backend.Domain;
using Backend.Persistence;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.IntegrationTests.Persistence
{
    public class SqliteDbManagerTests
    {
        private IDbManager<CountryAggregate> dbManager;

        [SetUp]
        public void SetUpDependencies()
        {
            dbManager = new SqliteDbManager();
        }

        [Test]
        public async Task ShouldExecuteQuery()
        {
            const string query = Queries.GetAllCountriesWithPopulation;

            var countries = await dbManager.ExecuteQuery(query);

            Assert.IsNotEmpty(countries);
        }

        [Test]
        public async Task ShouldGetAllCountriesPopulation()
        {
            const string Query = Queries.GetAllCountriesWithPopulation;
            const int CurrentNumberOfCountriesInTestDb = 16;

            var countries = (await dbManager.ExecuteQuery(Query))
                .Select(c => new CountryAggregateModel()
                {
                    Name = c.Country.CountryName,
                    Population = c.Population
                })
                .ToList();

            Assert.AreEqual(CurrentNumberOfCountriesInTestDb, countries.Count);
        }
    }
}
