using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.SQL.Contexts
{
    public class ConnectionDBAuthor
    {
        private readonly string _connectionString;

        public ConnectionDBAuthor(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnectionAuthor")!;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
