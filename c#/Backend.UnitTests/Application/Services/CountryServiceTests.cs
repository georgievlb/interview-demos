using Backend.Application.Interfaces;
using Backend.Application.Services;
using Backend.Common;
using Backend.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.UnitTests.ApplicationTests.Services
{
    [TestFixture]
    public class CountryServiceTests
    {
        private IDbManager<CountryAggregate> dbManager;
        private ICountryNameMappingService countryNameMappingService;
        private IStatService statService;
        private ICountryService countryService;

        [SetUp]
        public void SetUpDependencies()
        {
            dbManager = Mock.Of<IDbManager<CountryAggregate>>();
            countryNameMappingService = Mock.Of<ICountryNameMappingService>();
            statService = Mock.Of<IStatService>();

            countryService = new CountryService(dbManager, countryNameMappingService, statService);

            const string query = Queries.GetAllCountriesWithPopulation;

            Mock.Get(dbManager)
                .Setup(d => d.ExecuteQuery(query))
                .Returns(Task.FromResult(CreateDatasetOne()));

            Mock.Get(statService)
                .Setup(s => s.GetCountryPopulationsAsync())
                .Returns(Task.FromResult(CreateDatasetTwo()));

            Mock.Get(countryNameMappingService)
                .Setup(c => c.MapCountryNameToIso3166(It.IsAny<string>()))
                .Returns((string s) => s);
        }

        [Test]
        public void ShouldCreateInstance()
        {
            Assert.IsNotNull(countryService);
        }

        [Test]
        public async Task ShouldAggregateData()
        {
            // Arrange
            const int NumberOfAggregatedRecords = 10;

            // Act
            var aggregatedData = await countryService.GetCountriesAggregatedData();

            // Assert
            Assert.AreEqual(NumberOfAggregatedRecords, aggregatedData.Count);
        }

        [Test]
        public async Task ShouldPreferDataSourceOne_WhenAggregatingData()
        {
            var populationFromDataSourceOne = CreateDatasetOne().First();
            var populationFromDataSourceTwo = CreateDatasetTwo().First();

            var aggregateData = await countryService.GetCountriesAggregatedData();

            Assert.AreEqual(populationFromDataSourceOne.Population, aggregateData.FirstOrDefault().Population);
            Assert.AreNotEqual(populationFromDataSourceTwo.Item2, aggregateData.FirstOrDefault().Population);
        }

        private static IEnumerable<CountryAggregate> CreateDatasetOne()
        {
            return new List<CountryAggregate>()
            {
                new CountryAggregate { Country = new Country() { CountryName = "Albania" }, Population = 1234500 },
                new CountryAggregate { Country = new Country() { CountryName = "Bulgaria" }, Population = 2344510 },
                new CountryAggregate { Country = new Country() { CountryName = "Cypress" }, Population = 324510 },
                new CountryAggregate { Country = new Country() { CountryName = "Denmark" }, Population = 4344510 },
                new CountryAggregate { Country = new Country() { CountryName = "Finland" }, Population = 4111110 },
                new CountryAggregate { Country = new Country() { CountryName = "Hungary" }, Population = 9344510 },
                new CountryAggregate { Country = new Country() { CountryName = "Japan" }, Population = 14934450 }
            };
        }

        private static List<Tuple<string, int>> CreateDatasetTwo()
        {
            return new List<Tuple<string, int>>
            {
                Tuple.Create("Albania", 544310),
                Tuple.Create("Bulgaria", 889800),
                Tuple.Create("Cypress", 324510),
                Tuple.Create("Denmark", 4344510),
                Tuple.Create("Germany", 8944510),
                Tuple.Create("Italy", 7344510 ),
                Tuple.Create("Japan", 6345671 ),
                Tuple.Create("Kuwait", 56744510 )
            };
        }
    }
}
