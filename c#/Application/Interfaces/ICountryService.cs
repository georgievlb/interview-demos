using Backend.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetCountriesAggregatedData();
    }
}
