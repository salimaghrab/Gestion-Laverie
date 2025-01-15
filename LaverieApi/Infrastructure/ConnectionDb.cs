using MySql.Data.MySqlClient;

namespace LaverieApi.Infrastructure
{
    public class ConnectionDb
    {

        private static ConnectionDb _Instance;
        private readonly string _connectionString;
        private MySqlConnection _con;

        private ConnectionDb()
        {
            _connectionString = "Server=localhost; Port=3306; Database=LaveriesDb; Uid=root; Pwd=;";
            _con = new MySqlConnection(_connectionString);
        }
        public static ConnectionDb GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new ConnectionDb();
            }
            return _Instance;
        }

        public MySqlConnection GetConnection() => _con;
    }
}
