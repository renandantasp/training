using GDash.DB;
using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDashTests
{
    [TestClass]
    public class MariaTests
    {
        [TestMethod]
        public void GetConnection_ValidConnection()
        {
            Maria maria = new Maria();
            MySqlConnection expected = new MySqlConnection("server=localhost;userid=renan;password=admin;database=gdash");
            IDbConnection actual = maria.GetConnection();
            Assert.IsNotNull(expected.ConnectionString, actual.ConnectionString);
        }

        [TestMethod]
        public void GetCommand_ValidCommand()
        {
            Maria maria = new Maria();

            IDbCommand cmd = maria.GetCommand();

            Assert.IsNotNull(cmd);
        }
    }
}
