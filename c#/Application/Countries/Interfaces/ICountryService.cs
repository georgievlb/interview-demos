using Backend.Application.Countries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Application.Countries.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryAggregateModel>> GetCountriesAggregatedData();
    }
}
