using Backend.Domain.Countries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Application.Countries.Interfaces
{
    public interface ICountryAggregateManager : IDbManager
    {
        Task<IEnumerable<CountryAggregate>> GetCountriesAndPopulation();
    }
}
