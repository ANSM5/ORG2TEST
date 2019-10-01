using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class FileHelperTests
    {
        [TestMethod]
        public void TestThatIsCorrectFileTypeReturnsTrueWhenMatchingExtensionIsPassedIn()
        {
            // Arrange
            var fileHelper = new FileHelper.FileHelper();

            var path = @"C:\Temp\TestFile.json";

            var fileType = "json";

            var expected = true;
            
            // Act
            var actual = fileHelper.IsCorrectFileType(path, fileType);

            // Assert
            Assert.IsTrue(expected == actual, "Expected the method to return true as they file types should match");
        }

        [TestMethod]
        public void TestThatIsCorrectFileTypeReturnsFalseWhenNonMatchingExtensionIsPassedIn()
        {
            // Arrange
            var fileHelper = new FileHelper.FileHelper();

            var path = @"C:\Temp\TestFile.json";

            var fileType = "txt";

            var expected = true;

            // Act
            var actual = fileHelper.IsCorrectFileType(path, fileType);

            // Assert
            Assert.IsFalse(expected == actual, "Expected the method to return false as they file types shouldn't match");
        }
    }
}
