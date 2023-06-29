using MySqlConnector;
using System.Data;

namespace GDash.DB
{
    internal class Maria : IConnection
    {

        private readonly string user = "renan";
        private readonly string password = "admin";
        private readonly string host = "localhost";
        private readonly string database = "gdash";


        public IDbCommand GetCommand()
        {
            return new MySqlCommand();
        }

        public IDbConnection GetConnection()
        {
          
            return new MySqlConnection($"server={host};userid={user};password={password};database={database}");
        }
    }
}
