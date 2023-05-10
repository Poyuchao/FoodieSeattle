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
        private const string MockDescription = "Ba Bar is a Vietnamese-inspired restaurant with locations in Capitol Hill and South Lake Union, and offers a blend of traditional Vietnamese cuisine and modern fusion dishes. The restaurant\u0027s menu features a wide range of dishes, including pho, banh mi, rice and noodle dishes, and small plates for sharing. Ba Bar is known for its use of fresh, locally-sourced ingredients and its unique flavor combinations, which are inspired by the owner\u0027s travels in Vietnam and other parts of Southeast Asia. The interior of the restaurant is chic and modern, with a lively and casual atmosphere. Ba Bar is a great choice for those looking for creative and flavorful Vietnamese cuisine in Seattle at an affordable price.";

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

        #region description
        [Test]
        public void TestDescriptionProperty()
        {
            // Arrange
            var restaurant = new RestaurantModel
            {
                Description = MockDescription
            };
            var expectedDescription = MockDescription;

            // Act
            restaurant.Description = expectedDescription;
            var actualDescription = restaurant.Description;

            // Assert
            Assert.AreEqual(expectedDescription, actualDescription);
        }


        #endregion description
    }
}