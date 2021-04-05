using System.Data.Common;

namespace Backend.Persistence.Interfaces
{
    public interface IDbManager
    {
        DbConnection getConnection();
    }
}
