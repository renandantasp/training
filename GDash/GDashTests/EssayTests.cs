using GDash.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDashTests
{
    [TestClass]
    public class EssayTests
    {
        [TestMethod]
        public void Essay_EmptyConstructor()
        {
            // Arrange
            string expectedId = string.Empty;
            string expectedUserId = string.Empty;
            string expectedImage = string.Empty;
            string expectedEssayTitle = string.Empty;
            string expectedEssayText = string.Empty;


            // Act
            Essay essay = new Essay();

            // Assert

            Assert.AreEqual(expectedId, essay.Id);
            Assert.AreEqual(expectedUserId, essay.UserId);
            Assert.AreEqual(expectedImage, essay.Image);
            Assert.AreEqual(expectedEssayTitle, essay.EssayTitle);
            Assert.AreEqual(expectedEssayText, essay.EssayText);

        }

        [TestMethod]
        public void Essay_FilledConstructor()
        {
            // Arrange
            string expectedId = string.Empty;
            string expectedUserId = string.Empty;
            string expectedImage = string.Empty;
            string expectedEssayTitle = string.Empty;
            string expectedEssayText = string.Empty;


            // Act
            Essay essay = new Essay(expectedId, expectedUserId, expectedImage,
                                    expectedEssayTitle, expectedEssayText);

            // Assert

            Assert.AreEqual(expectedId, essay.Id);
            Assert.AreEqual(expectedUserId, essay.UserId);
            Assert.AreEqual(expectedImage, essay.Image);
            Assert.AreEqual(expectedEssayTitle, essay.EssayTitle);
            Assert.AreEqual(expectedEssayText, essay.EssayText);
        }

        [TestMethod]
        public void Clone_ReturnsClone()
        {
            // Arrange

            Essay original = new Essay();

            // Act
            Essay copy = original.Clone();

            // Assert
            Assert.IsFalse(copy.Equals(original));
        }

        [TestMethod]
        public void GetObject_ReturnsSame()
        {
            // Arrange

            Essay original = new Essay();

            // Act
            IModel copy = original.GetObject();

            // Assert
            Assert.IsTrue(copy.Equals(original));
        }
    }
}

