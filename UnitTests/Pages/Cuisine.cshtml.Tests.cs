using FoodieSeattle.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;


namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for the Cuisine Page.
    /// </summary>
    public class CuisineTests
    {
        #region TestSetup

        // CuisineModel object.
        private static CuisineModel pageModel;

        /// <summary>
        /// Initialize mock Logger. 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var mockLoggerDirect = Mock.Of<ILogger<CuisineModel>>();

            pageModel = new CuisineModel(mockLoggerDirect, TestHelper.RestaurantServiceObject);
        }

        #endregion TestSetup

        #region OnGet


        /// <summary>
        /// Tests that when OnGet function of cuisine page 
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Restaurants()
        {
            // Arrange
            var neighborhood = "Japanese";
            var expectedCount = 5;

            // Act
            pageModel.OnGet(neighborhood);

            // Assert
            Assert.AreEqual(expectedCount, pageModel.Restaurants.Count());
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet
    }
}