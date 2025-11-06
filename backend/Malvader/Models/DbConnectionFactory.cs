using MySql.Data.MySqlClient;

namespace Malvader.Models
{
    public class DbConnectionFactory
    {
        public string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

    }
}
