using MySql.Data.MySqlClient;

namespace WebApplication3
{
    public static class Config
    {
        public static MySqlConnection conn()
        {
            string connection_value = "Server=127.0.0.1;Port=3306;Database=chatTakeTest;User ID=root;Password=root;";

            MySqlConnection conn = new MySqlConnection(connection_value);

            return conn;
        }
    }
}
