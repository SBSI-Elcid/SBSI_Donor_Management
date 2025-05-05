using System.Data;

namespace BBIS.Application.ConnectionProvider
{
    public interface IConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}
