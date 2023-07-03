using GDash.MVVM.Model;
using System.Collections.Generic;

namespace GDashTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_EmptyConstructor()
        {
            // Arrange
            string expectedId = string.Empty;
            string expectedName = string.Empty;
            string expectedTag = string.Empty;
            string expectedEmail = string.Empty;
            string expectedPassword = string.Empty;
            string expectedProfileImage = string.Empty;
            string expectedBio = string.Empty;


            // Act
            User user = new User();

            // Assert
            Assert.AreEqual(expectedId, user.Id);
            Assert.AreEqual(expectedName, user.Name);
            Assert.AreEqual(expectedTag, user.Tag);
            Assert.AreEqual(expectedEmail, user.Email);
            Assert.AreEqual(expectedPassword, user.Password);
            Assert.AreEqual(expectedProfileImage, user.ProfileImage);
            Assert.AreEqual(expectedBio, user.Bio);

        }

        [TestMethod]
        public void User_FilledConstructor()
        {
            // Arrange
            string expectedId = "0001001";
            string expectedName = "Some Name";
            string expectedTag = "Some Tag";
            string expectedEmail = "Some Email";
            string expectedPassword = "Some Password";
            string expectedProfileImage = "Some Image";
            string expectedBio = "Some Bio";


            // Act
            User user = new User(expectedId, expectedName, expectedTag, expectedEmail,
                                 expectedPassword, expectedProfileImage, expectedBio);

            // Assert
            Assert.AreEqual(expectedId, user.Id);
            Assert.AreEqual(expectedName, user.Name);
            Assert.AreEqual(expectedTag, user.Tag);
            Assert.AreEqual(expectedEmail, user.Email);
            Assert.AreEqual(expectedPassword, user.Password);
            Assert.AreEqual(expectedProfileImage, user.ProfileImage);
            Assert.AreEqual(expectedBio, user.Bio);

        }

        [TestMethod]
        public void Clone_ReturnsClone()
        {
            // Arrange

            User original = new User();

            // Act
            User copy = original.Clone();

            // Assert
            Assert.IsFalse(copy.Equals(original));
        }

        [TestMethod]
        public void GetObject_ReturnsSame()
        {
            // Arrange

            User original = new User();

            // Act
            IModel copy = original.GetObject();

            // Assert
            Assert.IsTrue(copy.Equals(original));
        }
    }
}