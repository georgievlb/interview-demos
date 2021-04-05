using Backend.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Service.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetCountriesFromDataSourceOne();

        Task<List<CountryDto>> GetCountriesFromDataSourceTwo();

        Task<List<CountryDto>> GetCountriesAggregatedData();
    }
}
