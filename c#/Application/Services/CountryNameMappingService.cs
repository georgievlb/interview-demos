using Backend.Application.Interfaces;
using Backend.Common;
using System.Collections.Generic;

namespace Backend.Application.Services
{
    public class CountryNameMappingService : ICountryNameMappingService
    {
        private Dictionary<string, string> countryNames;

        public CountryNameMappingService()
        {
            SeedCountryNames();
        }

        public string MapCountryNameToIso3166(string countryName)
        {
            string isoValue;

            if (countryNames.TryGetValue(countryName.ToLowerInvariant(), out isoValue))
            {
                return isoValue;
            }

            return countryName;
        }

        private void SeedCountryNames()
        {
            if (countryNames == null)
            {
                countryNames = new Dictionary<string, string>();
            }

            // USA
            countryNames.Add("united states", CountriesIso3166.USA);
            countryNames.Add("the united states", CountriesIso3166.USA);
            countryNames.Add("united states of america", CountriesIso3166.USA);
            countryNames.Add("the united states of america", CountriesIso3166.USA);
            countryNames.Add("us", CountriesIso3166.USA);
            countryNames.Add("usa", CountriesIso3166.USA);
            countryNames.Add("u.s.a.", CountriesIso3166.USA);
            countryNames.Add("u.s.", CountriesIso3166.USA);
            // UK
            countryNames.Add("u.k.", CountriesIso3166.UK);
            countryNames.Add("the u.k.", CountriesIso3166.UK);
            countryNames.Add("uk", CountriesIso3166.UK);
            countryNames.Add("the uk", CountriesIso3166.UK);
            countryNames.Add("united kingdom", CountriesIso3166.UK);
            countryNames.Add("the united kingdom", CountriesIso3166.UK);
            countryNames.Add("the united kingdom of great britain", CountriesIso3166.UK);
            countryNames.Add("the united kingdom of great britain and northern ireland", CountriesIso3166.UK);

        }
    }
}
