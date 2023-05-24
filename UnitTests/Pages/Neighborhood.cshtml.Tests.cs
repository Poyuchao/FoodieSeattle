using FoodieSeattle.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;


namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for the Neighborhood Page.
    /// </summary>
    public class NeighborhoodTests
    {
        #region TestSetup

        // NeighborhoodModel object.
        private static NeighborhoodModel pageModel;

        /// <summary>
        /// Initialize mock Logger. 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var mockLoggerDirect = Mock.Of<ILogger<NeighborhoodModel>>();

            pageModel = new NeighborhoodModel(mockLoggerDirect, TestHelper.RestaurantServiceObject);
        }

        #endregion TestSetup

        #region OnGet


        /// <summary>
        /// Tests that when OnGet function of neighborhoods page 
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Restaurants()
        {
            // Arrange
            var neighborhood = "Ballard";
            var expectedCount = 3;

            // Act
            pageModel.OnGet(neighborhood);

            // Assert
            Assert.AreEqual(expectedCount, pageModel.Restaurants.Count());
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet

        #region TestGet

        /// <summary>
        /// Tests the get accessor of the SelectedNeighborhood property.
        /// </summary>
        [Test]
        public void SelectedCuisine_Get_Should_Return_ExpectedValue()
        {
            // Arrange
            var expectedValue = "Ballard";

            // Act
            pageModel.SelectedNeighborhood = expectedValue;
            var actualValue = pageModel.SelectedNeighborhood;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion TestGet
    }
}