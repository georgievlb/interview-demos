using Backend.Application.Countries.Interfaces;
using Backend.Persistence;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Backend.IntegrationTests.Persistence
{
    public class CountryAggregateManagerTests
    {
        private ICountryAggregateManager dbManager;

        [OneTimeSetUp]
        public void SetUpBaseDirectory()
        {
            // When executing the integration tests, the current directory changes to C:\Users\<USER>\AppData\Local\Temp
            var assemblyDir = AppDomain.CurrentDomain.BaseDirectory;
            Directory.SetCurrentDirectory(assemblyDir);
            Environment.CurrentDirectory = assemblyDir;
        }

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
