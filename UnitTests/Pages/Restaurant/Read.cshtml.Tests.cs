using System.Linq;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using Moq;
using ContosoCrafts.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NUnit.Framework.Internal;

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
            callReadModel = new ReadModel(TestHelper._RestaurantService);
            expectedRestaurants = TestHelper._RestaurantService.GetRestaurants().ToList();
        }

        /// <summary>
        /// Tests the "OnGet" method of the ReadModel class by checking if it returns
        /// the expected restaurant title for a valid restaurant ID.
        /// </summary>
        [Test]
        public void OnGet_Retuen_Same_Data()
        {
            //arrange
            var expectedRestaurant = TestHelper._RestaurantService.GetRestaurants().First().Title;

            //Act
             callReadModel.OnGet(ExistingId);
            var result = callReadModel.Restaurant.Title;

            //Assert
            Assert.AreEqual(expectedRestaurant, result);//match the result read unit

            
        }



    }
}
