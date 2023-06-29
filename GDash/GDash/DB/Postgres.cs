using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GDash.DB
{
    public class Postgres : IConnection
    {

        private readonly string user = "postgres";
        private readonly string password = "admin";
        private readonly string host = "localhost";
        private readonly string port = "5555";
        private readonly string database = "postgres";

        
        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection($"User ID={user};Password={password};Host={host};Port={port};Database={database};");

        }

        
        public IDbCommand GetCommand()
        {
            return new NpgsqlCommand();
        }

        
    }
}
