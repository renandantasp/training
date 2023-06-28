using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GDash.DB
{
    public class NpgConnection 
    {
        public readonly IDbConnection connection;
        private readonly string user = "postgres";
        private readonly string password = "admin";
        private readonly string host = "localhost";
        private readonly string port = "5555";
        private readonly string database = "postgres";

        public NpgConnection()
        {
            this.connection = new NpgsqlConnection($"User ID={user};Password={password};Host={host};Port={port};Database={database};");

        }

    }
}
