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

        private readonly string user;
        private readonly string password;
        private readonly string host;
        private readonly string port;
        private readonly string database;

        public Postgres()
        {
            user = "postgres";
            password = "admin";
            host = "localhost";
            port = "5555";
            database = "postgres";
    }

        
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
