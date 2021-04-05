using Backend.Application.Countries.Services;
using Backend.Common;
using NUnit.Framework;

namespace Backend.UnitTests.Application.Services
{
    [TestFixture]
    public class CountryNameMappingServiceTests
    {
        private CountryNameMappingService service;

        [SetUp]
        public void SetUpDependencies()
        {
            service = new CountryNameMappingService();
        }

        [Test]
        [TestCase("u.k.", CountriesIso3166.UK)]
        [TestCase("the u.k.", CountriesIso3166.UK)]
        [TestCase("uk", CountriesIso3166.UK)]
        [TestCase("the uk", CountriesIso3166.UK)]
        [TestCase("united kingdom", CountriesIso3166.UK)]
        [TestCase("the united kingdom", CountriesIso3166.UK)]
        [TestCase("the united kingdom of great britain", CountriesIso3166.UK)]
        [TestCase("the united kingdom of great britain and northern ireland", CountriesIso3166.UK)]
        [TestCase("united states", CountriesIso3166.USA)]
        [TestCase("the united states", CountriesIso3166.USA)]
        [TestCase("united states of america", CountriesIso3166.USA)]
        [TestCase("the united states of america", CountriesIso3166.USA)]
        [TestCase("us", CountriesIso3166.USA)]
        [TestCase("usa", CountriesIso3166.USA)]
        [TestCase("u.s.a.", CountriesIso3166.USA)]
        [TestCase("u.s.", CountriesIso3166.USA)]
        public void ShouldMapCountryNameToIso3166ShortName(string countryName, string isoName)
        {
            var mapedName = service.MapCountryNameToIso3166(countryName);

            Assert.AreEqual(isoName, mapedName);
        }
    }
}
