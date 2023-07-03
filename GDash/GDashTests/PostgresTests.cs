using GDash.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Npgsql;
using System.Threading.Tasks;
using Moq;

namespace GDashTests
{
    [TestClass]
    public class PostgresTests
    {
        [TestMethod]
        public void GetConnection_ValidConnection()
        {
            Postgres postgres = new Postgres();
            NpgsqlConnection expected = new NpgsqlConnection("User ID=postgres;Password=admin;Host=localhost;Port=5555;Database=postgres;");

            IDbConnection actual = postgres.GetConnection();
            
            Assert.AreEqual(expected.ConnectionString, actual.ConnectionString);
        }

        [TestMethod]
        public void GetCommand_ValidCommand()
        {
            Postgres postgres = new Postgres();

            IDbCommand cmd = postgres.GetCommand();

            Assert.IsNotNull(cmd);
        }
    }
}
