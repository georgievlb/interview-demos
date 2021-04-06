using System.Data.Common;

namespace Backend.Application.Countries.Interfaces
{
    public interface IDbManager
    {
        DbConnection GetConnection();
    }
}
