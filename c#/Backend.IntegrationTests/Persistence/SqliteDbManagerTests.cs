using Backend.Application.Interfaces;
using Backend.Common;
using Backend.Domain;
using Backend.Persistence;
using NUnit.Framework;
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
    }
}
