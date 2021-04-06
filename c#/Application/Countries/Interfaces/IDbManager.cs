using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Backend.Application.Countries.Interfaces
{
    public interface IDbManager<T> where T : class
    {
        DbConnection GetConnection();

        Task<IEnumerable<T>> ExecuteQuery(string query);
    }
}
