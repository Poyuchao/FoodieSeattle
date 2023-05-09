using FoodieSeattle.WebSite.Models;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Unit tests for RestaurantModel
    /// </summary>
    public class RestaurantModelTests
    {
        #region TestSetup

        // Global valid Mock Id property for use in tests
        private const string MockId = "mamnoon-pic";

        // Global valid Mock title property for use in tests
        private const string MockTitle = "mamnoon";

        // Global valic Mock neighborhood property for use in tests
        private const string MockNeighborhood = "Capitol Hill";

        // Global valid Mock food type property for use in tests
        private const string MockType = "Lebanese";

        // Global valid mock description property for use in tests
        private const string MockDescription = "description";

        // Global valid mock Url property for use in tests
        private const string MockUrl = "https://www.nadimama.com/mamnoon";

        // Global valid mock Image property for use in tests
        private const string MockImage = "https://mamnoontogo.net/wp-content/uploads/2021/10/mamnoon.png";

        #endregion TestSetup

        #region ToString

        /// <summary>
        /// Tests that ToString method of a RestaurantModel instance returns a
        /// non-empty JSON string representation that contains the expected data.
        /// </summary>
        /// </summary>
        [Test]
        public void ToString_ReturnsJson()
        {
            // Arrange
            var restaurant = new RestaurantModel
            {
                Id = MockId,
                Title = MockTitle,
                Neighborhood = MockNeighborhood,
                Type = MockType,
                Url = MockUrl,
                Image = MockImage
            };

            // Act
            var jsonString = restaurant.ToString();

            // Assert
            Assert.IsNotEmpty(jsonString);
            Assert.IsTrue(jsonString.Contains(restaurant.Id));
            Assert.IsTrue(jsonString.Contains(restaurant.Title));
            Assert.IsTrue(jsonString.Contains(restaurant.Neighborhood));
            Assert.IsTrue(jsonString.Contains(restaurant.Type));
            Assert.IsTrue(jsonString.Contains(restaurant.Url));
            Assert.IsTrue(jsonString.Contains(restaurant.Image));
        }

        #endregion ToString


    }
}