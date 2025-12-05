using Microsoft.Data.SqlClient;

namespace DevLearning.API.DataBase
{
    public class ConnectionDB
    {

        private readonly string _connectionString;

        public ConnectionDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
