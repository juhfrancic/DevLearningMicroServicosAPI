using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.SQL.Contexts
{
    public class ConnectionDBCategory
    {
        private readonly string _connectionString;

        public ConnectionDBCategory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnectionCategory")!;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
