using System.Collections.Generic;
using FoodieSeattle.WebSite.Controllers;
using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using static FoodieSeattle.WebSite.Controllers.RestaurantsController;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Unit test for RestaurantController.
    /// </summary>
    class RestaurantControllerTests
    {
        // Global NeighborhodService to use for all test cases. 
        RestaurantService restaurantService;

        /// <summary>
        /// Stores the TestHelper's Restaurant service. 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            restaurantService = TestHelper.RestaurantServiceObject;
        }

        #region Constructor

        /// <summary>
        /// Tests that a RestaurantController constructor creates a new RestaurantController
        /// instance when called when valid input parameter is provided.
        /// </summary>
        [Test]
        public void RestaurantController_Valid_New_Controller_Not_Null_Should_Return_True()
        {
            //Arrange

            //Act
            var controller = new RestaurantsController(restaurantService);

            //Assert
            Assert.NotNull(controller);
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Tests that instance of RestaurantController object has
        /// a Get function that returns a non-null IEnumerable of
        /// a RestaurantModel
        /// </summary>
        [Test]
        public void Test_Get_Should_Return_Restaurants()
        {
            // Arrange
            var controller = new RestaurantsController(restaurantService);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<RestaurantModel>>(result);
        }

        #endregion Get

        #region Patch

        /// <summary>
        /// Tests Patch method of the RestaurantsController adds a rating
        /// to a known restaurant and returns an HTTP 200 OK response.
        /// </summary>
        [Test]
        public void Patch_AddsRatingAndReturnsOk()
        {
            // Arrange
            var controller = new RestaurantsController(restaurantService);
            var knownId = "bangrok-pic";
            var rating = 5;
            var ratingRequest = new RatingRequest { RestaurantId = knownId, Rating = rating}; 

            // Act
            var result = controller.Patch(ratingRequest) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        #endregion Patch
    }



}