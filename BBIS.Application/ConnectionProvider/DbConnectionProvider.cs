using MySqlConnector;
using System.Data;

namespace BBIS.Application.ConnectionProvider
{
    public class DbConnectionProvider : IConnectionProvider
    {
        private readonly string connectionString;

        public DbConnectionProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
