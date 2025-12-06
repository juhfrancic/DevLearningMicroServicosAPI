using Microsoft.Data.SqlClient;

namespace DevLearning.AuthorAPI.Data
{
    public class ConnectionDBAuthor
    {
        private readonly string _connectionString;

        public ConnectionDBAuthor(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnectionAuthor");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
