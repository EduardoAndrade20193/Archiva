using MySql.Data.MySqlClient;


namespace Archiva.Models
{
    public class DbConexion
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbConexion(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MySQLConnection");
        }

        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
