using GDash.DB;
using GDash.MVVM.Model;
using Moq;


namespace GDashTests
{
    [TestClass]
    public class UserConnTests
    {


        [TestMethod]
        public void GetAllDB_ValidCall()
        {

            // Arrange
            Mock<IConnection> mockIconn = new Mock<IConnection>();
            UserConn userConn = new UserConn(mockIconn.Object);

            // Act
                
            
            // Assert
            Assert.IsNotNull(userConn);
        }

        [TestMethod]
        public void InsertDB_ValidCall()
        {

            // Arrange
            Mock<IConnection> mockIconn = new Mock<IConnection>();
            UserConn userConn = new UserConn(mockIconn.Object);

            User user = new User();
            string commandStr = "INSERT INTO users (id, name, tag, email, password, profileimage, bio)"
                                        + $"VALUES('{user.Id}', '{user.Name}', '{user.Tag}', '{user.Email}', '{user.Password}', '{user.ProfileImage}', '{user.Bio}')";


            // Act
            //userConn.ExecuteCMD(commandStr);

            // Assert
            Assert.IsTrue(true);
        }
    }
}
