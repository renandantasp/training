using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDash.DB
{
    internal class Postgres : IConnection
    {
        NpgConnection connection;
        NpgsqlCommand command;
        public Postgres() { }
    }
}
