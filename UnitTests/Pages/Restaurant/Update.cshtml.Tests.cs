
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using FoodieSeattle.WebSite.Pages.Restaurant;
using FoodieSeattle.WebSite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace UnitTests.Pages.Restaurant.Update
{
    /// <summary>
    /// Unit tests for the Update page.
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup

        // UpdateModel object.
        public static UpdateModel pageModel;

        // Global valid existing Id property for use in tests
        private const string ExistingId = "kashiba-pic";

        // Global valid existing title property for use in tests
        private const string ExistingTitle = "Sushi Kashiba";

        // Global valid Mock Id property for use in tests
        private const string MockId = "mamnoon-pic";

        // Global valid Mock title property for use in tests
        private const string MockTitle = "mamnoon";

        // Global valid Mock food type property for use in tests
        private const string MockType = "Lebanese";

        // Global valic Mock neighborhood property for use in tests
        private const string MockNeighborhood = "Capitol Hill";

        // Global valic Mock city property for use in tests
        private const string MockCity = "Seattle";

        // Global valic Mock state property for use in tests
        private const string MockState = "WA";

        // Global valic Mock address property for use in tests
        private const string MockAddress = "2020 6th Ave, Seattle, WA 98121-2507";

        // Global valid mock description property for use in tests
        private const string MockDescription = "description";

        // Global valid mock Url property for use in tests
        private const string MockUrl = "url";

        // Global valid mock Image property for use in tests
        private const string MockImage = "image";

        // Global ErrorAttribute property for invalid model state
        private const string ErrorAttribute = "bogus";

        // Global Error to test invalid model state
        private const string Error = "bogus error";

        /// <summary>
        /// Initialize UpdateModel with a RestaurantService object. 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Initialize pageModel
            pageModel = new UpdateModel(TestHelper.RestaurantServiceObject)
            {
                PageContext = TestHelper.InitiatePageContext()
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Test that when OnGet is called, a valid Model State should return true 
        /// and a known Id should match the same item's title.
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Restaurants()
        {
            // Arrange

            // Act
            pageModel.OnGet(ExistingId);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(ExistingTitle, pageModel.Restaurant.Title);
        }

        /// <summary>
        /// Tests when OnGet is called, an invalid ModelState should return false
        /// and redirect to Index page.
        /// </summary>
        [Test]
        public void OnGet_InValid_ModelState_Should_Return_False_And_Redirect_To_Index_Page()
        {
            // Arrange
            pageModel.Restaurant = new RestaurantModel
            {
                Id = MockId,
                Title = ErrorAttribute,
                Type = MockType,
                Neighborhood = MockNeighborhood,
                City = MockCity,
                State = MockState,
                Address = MockAddress,
                Description = MockDescription,
                Url = MockUrl,
                Image = MockImage
            };

            // Force an invalid error state.
            pageModel.ModelState.AddModelError("InvalidState", "Invalid restaurant state");

            // Act
            var result = pageModel.OnGet(pageModel.Restaurant.Id) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// This function tests the behavior of the "OnGet" method in the Update Razor Page,
        /// when the ModelState is null and the RestaurantModel is also null. A valid Model
        /// State with a null restaurant should redirect to the Index page.
        /// </summary>
        [Test]
        public void OnGet_Valid_ModelState_Null_Restaurant_Should_Redirect_To_Index_Page()
        {
            // Arrange
            pageModel.Restaurant = new RestaurantModel
            {
                Id = null,
                Title = null,
                Type = null,
                Neighborhood = null,
                City = null,
                State = null,
                Address = null,
                Description = null,
                Url = null,
                Image = null
            };

            // Act
            var result = pageModel.OnGet(pageModel.Restaurant.Id) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }
        #endregion OnGet

        /// <summary>
        /// A unit test method that tests the OnPost() method of a page model when
        /// valid data is provided. The test verifies that the OnPost() method redirects
        /// to the Index page and that the ModelState is valid.
        /// </summary>
        #region OnPost
        [Test]
        public void OnPost_Valid_Should_Return_Restaurants()
        {
            // Arrange
            // Set up testing RestaurantModel object with sample data
            pageModel.Restaurant = new RestaurantModel
            {
                Id = MockId,
                Title = MockTitle,
                Type = MockType,
                Neighborhood = MockNeighborhood,
                City = MockCity,
                State = MockState,
                Address = MockAddress,
                Description = MockDescription,
                Url = MockUrl,
                Image = MockImage
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// Tests the OnPost() method of a page model when the ModelState is invalid. 
        /// The test verifies that the ModelState is not valid after the OnPost() method
        /// is invoked.
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange

            // Force an invalid error state
            pageModel.ModelState.AddModelError(ErrorAttribute, Error);

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        #endregion OnPost
    }
}