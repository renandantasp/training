using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDash.DB
{
    internal class Connection
    {
        public readonly NpgsqlConnection connection;
        private readonly string user = "postgres";
        private readonly string password = "admin";
        private readonly string host = "localhost";
        private readonly string port = "5555";
        private readonly string database = "postgres";

        public Connection()
        {
            this.connection = new NpgsqlConnection($"User ID={user};Password={password};Host={host};Port={port};Database={database};");

        }

    }
}
