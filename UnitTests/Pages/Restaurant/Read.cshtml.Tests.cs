using System.Linq;
using FoodieSeattle.WebSite.Models;
using NUnit.Framework;
using FoodieSeattle.WebSite.Services;
using Moq;
using FoodieSeattle.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Pages.Restaurant.Read
{

    /// <summary>
    /// Unit testing for Read page
    /// </summary>
    public class ReadTests
    {
        // Set up ReadModel
        private ReadModel callReadModel;

        // Set up RestaurantModel List
        private List<RestaurantModel> expectedRestaurants;

        // Global valid existing Id property for use in tests
        private const string ExistingId = "kashiba-pic";

        /// <summary>
        /// Sets up the necessary objects and data for ReadTests. It creates a ReadModel
        /// instance and initializes an expectedRestaurants list
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Create ReadModel instance and expected restaurant list
            callReadModel = new ReadModel(TestHelper.RestaurantServiceObject);
            expectedRestaurants = TestHelper.RestaurantServiceObject.GetRestaurants().ToList();
        }

        /// <summary>
        /// Tests the "OnGet" method of the ReadModel class by checking if it returns
        /// the expected restaurant title for a valid restaurant ID.
        /// </summary>
        [Test]
        public void OnGet_Return_Same_Data()
        {
            //arrange
            var expectedRestaurant = TestHelper.RestaurantServiceObject.GetRestaurants().First().Title;

            //Act
            callReadModel.OnGet(ExistingId);
            var result = callReadModel.Restaurant.Title;

            //Assert
            Assert.AreEqual(expectedRestaurant, result);//match the result read unit


        }

        /// <summary>
        /// Tests that the Password property of a ReadModel instance can be set and
        /// retrieved correctly.
        /// </summary>
        [Test]
        public void PasswordProperty_SetGet_ReturnsCorrectValue()
        {
            // Arrange
       
            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            var expectedPassword = "test123";

            // Act
            model.Password = expectedPassword;
            var actualPassword = model.Password;

            // Assert
            Assert.AreEqual(expectedPassword, actualPassword);
        }

        /// <summary>
        /// Tests that the PasswordEntered property of a ReadModel instance can be set
        /// and retrieved correctly.
        /// </summary>
        [Test]
        public void PasswordEnterProperty_SetGet_ReturnsCorrectValue()
        {
            // Arrange

            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            var expectedPasswordEntered = true;

            // Act
            model.PasswordEntered = expectedPasswordEntered;
            var actualPasswordEntered = model.PasswordEntered;

            // Assert
            Assert.AreEqual(expectedPasswordEntered, actualPasswordEntered);
        }

        /// <summary>
        /// Tests that the OnPost method of a ReadModel instance returns a
        /// PageResult when a valid password is provided.
        /// </summary>
        [Test]
        public void OnPost_ValidPassword_ReturnsPageResult()
        {
            // Arrange
           
            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            model.Password = "6666";
            model.PageContext = new PageContext
            {
                RouteData = new Microsoft.AspNetCore.Routing.RouteData()
            };
            model.PageContext.RouteData.Values.Add("id", ExistingId); // Set the id value to a valid value for your test

            // Act
            var result = model.OnPost();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsTrue(model.PasswordEntered);
        }

        /// <summary>
        /// Tests that the OnPost method of a ReadModel instance returns a
        /// PageResult with an error when an incorrect password is provided.
        /// </summary>
        [Test]
        public void OnPost_IncorrectPassword_ReturnsPageResultWithError()
        {
            // Arrange
            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            model.Password = "1234"; // Use an incorrect password
            model.PageContext = new PageContext
            {
                RouteData = new Microsoft.AspNetCore.Routing.RouteData()
            };
            model.PageContext.RouteData.Values.Add("id", ExistingId);

            // Act
            var result = model.OnPost();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(model.PasswordEntered);           
        }
    }
}
