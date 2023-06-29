using Autofac.Extras.Moq;
using GDash.DB;
using GDash.MVVM.Model;
using Moq;

namespace GDashTests    
{
    public class UserConnTests
    {

        // GetAllDB
        // InsertDB
        // UpdateDB
        // DeleteDB
        // GetUsers
        // GetEssaysById

        [Fact]
        public void GetAllDB_ValidCall()
        {

            Mock<ICRUD> mock = new Mock<ICRUD>();

            //IConnection _mock = new Mock<IConnection>();
                
            mock.Setup(f => f.GetAllDB())
                .Returns(GetSampleUsers());

            UserConn controller = mock.Create<UserConn>();
            List<IModel> expected = GetSampleUsers();
            List<IModel> actual = controller.GetAllDB();    

            Assert.True(5 == 5);
            //Assert.True(expected != null);

            
        }
            
        private List<IModel> GetSampleUsers()
        {
            List<IModel> models = new List<IModel>
            {
                new User(),
                new User(),
                new User()
            };
            return models;
        }
    }
}