using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDash.DB
{
    internal class Maria : IConnection
    {

        private readonly string user = "renan";
        private readonly string password = "admin";
        private readonly string host = "localhost";
        private readonly string port = "3306";
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
