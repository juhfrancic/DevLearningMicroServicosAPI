using Microsoft.Data.SqlClient;

namespace DevLearning.CategoryAPI.Data
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
