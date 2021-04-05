using Backend.Service.Models;
using System.Collections.Generic;

namespace Backend.Service.Interfaces
{
    public interface ICountryService
    {
        List<CountryDto> GetCountriesFromDataSourceOne();
    }
}
