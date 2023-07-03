using GDash.DB;
using GDash.MVVM.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace GDash.Tests
{
    public class UserConnTests
    {
        /*

        [Test]
        public void GetAllDB_ValidCall()
        {
            Mock<IConnection> npg_mock = new Mock<IConnection>();

            npg_mock.Setup(rep => rep.GetConnection()).Returns(It.IsAny<IDbConnection>());
            npg_mock.Setup(rep => rep.GetCommand()).Returns(It.IsAny<IDbCommand>());
            
            UserConn userConn = new UserConn(npg_mock.Object);

            List<IModel> users = userConn.GetAllDB();
            Assert.NotNull(users);

            Assert.Pass();
        }

        [Test]
        public void InsertDB_ValidCall()
        {
            Mock<IConnection> npg_mock = new Mock<IConnection>();

            npg_mock.Setup(rep => rep.GetConnection()).Returns(It.IsAny<IDbConnection>());
            npg_mock.Setup(rep => rep.GetCommand()).Returns(It.IsAny<IDbCommand>());

            UserConn userConn = new UserConn(npg_mock.Object);
            User user = new User();
            userConn.InsertDB(user);

            Assert.Pass();
        }

        [Test]
        public void UpdateDB_ValidCall()
        {
            Mock<IConnection> npg_mock = new Mock<IConnection>();

            npg_mock.Setup(rep => rep.GetConnection()).Returns(It.IsAny<IDbConnection>());
            npg_mock.Setup(rep => rep.GetCommand()).Returns(It.IsAny<IDbCommand>());

            UserConn userConn = new UserConn(npg_mock.Object);
            User user = new User();
            userConn.UpdateDB(user);

            Assert.Pass();
        }

        [Test]
        public void DeleteDB_ValidCall()
        {
            Mock<IConnection> npg_mock = new Mock<IConnection>();

            npg_mock.Setup(rep => rep.GetConnection()).Returns(It.IsAny<IDbConnection>());
            npg_mock.Setup(rep => rep.GetCommand()).Returns(It.IsAny<IDbCommand>());

            UserConn userConn = new UserConn(npg_mock.Object);
            User user = new User();
            userConn.DeleteDB(user);

            Assert.Pass();
        }


        [Test]
        public void GetUsers_ValidCall()
        {
            Mock<IConnection> npg_mock = new Mock<IConnection>();

            npg_mock.Setup(rep => rep.GetConnection()).Returns(It.IsAny<IDbConnection>());
            npg_mock.Setup(rep => rep.GetCommand()).Returns(It.IsAny<IDbCommand>());

            UserConn userConn = new UserConn(npg_mock.Object);
            List<User> users = userConn.GetUsers();


            Assert.Pass();
        }

        [Test]
        public void GetEssaysByID_ValidCall()
        {
            Mock<IConnection> npg_mock = new Mock<IConnection>();

            npg_mock.Setup(rep => rep.GetConnection()).Returns(It.IsAny<IDbConnection>());
            npg_mock.Setup(rep => rep.GetCommand()).Returns(It.IsAny<IDbCommand>());

            UserConn userConn = new UserConn(npg_mock.Object);
            User user = new User();
            string essays = userConn.GetEssaysById(user.Id);


            Assert.Pass();
        }
        */
    }
}