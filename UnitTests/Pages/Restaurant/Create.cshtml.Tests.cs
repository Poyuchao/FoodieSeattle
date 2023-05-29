using System.Linq;
using NUnit.Framework;
using FoodieSeattle.WebSite.Pages.Restaurant;
using FoodieSeattle.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace UnitTests.Pages.Restaurant.Create
{
    /// <summary>
    /// Unit tests for create page
    /// </summary>
    public class CreateTests
    {
        #region TestSetup
        // CreateModel object
        public static CreateModel pageModel;

        // Global RestaurantService to use for all test cases. 
        public RestaurantService restaurantService;

        // Global valid Mock Id property for use in tests
        private const string MockId = "mamnoon-pic";

        // Global valid Mock title property for use in tests
        private const string MockTitle = "mamnoon";

        // Global valid Mock neighborhood property for use in tests
        private const string MockNeighborhood = "Capitol Hill";

        // Global valid Mock city property for use in tests
        private const string MockCity = "Seattle";

        // Global valid Mock state property for use in tests
        private const string MockState = "WA";

        // Global valid Mock address property for use in tests
        private const string MockAddress = "2020 6th Ave, Seattle, WA 98121-2507";

        // Global valid Mock food type property for use in tests
        private const string MockType = "Lebanese";

        // Global valid mock description property for use in tests
        private const string MockDescription = "High end Labanese restaurant in Capitol Hill";

        // Global valid mock Url property for use in tests
        private const string MockUrl = "https://www.nadimama.com/mamnoon";

        // Global valid mock Image property for use in tests
        private const string MockImage = "https://mamnoontogo.net/wp-content/uploads/2021/10/mamnoon.png";

        /// <summary>
        /// Initialize CreateModel field
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Initialize pageModel
            pageModel = new CreateModel(TestHelper.RestaurantServiceObject)
            {
                PageContext = TestHelper.InitiatePageContext()
            };
        }

        /// <summary>
        /// Creates a mock form collection with mock data.
        /// </summary>
        /// <returns>A FormCollection object holding a collection of form data</returns>
        private FormCollection GetMockFormCollection()
        {
            // Store mock user input in String arrays to match FormCollection Value format.
            string[] idArray = { MockId };
            string[] nameArray = { MockTitle };
            string[] neighborArray = { MockNeighborhood };
            string[] cityArray = { MockCity };
            string[] stateArray = { MockState };
            string[] addressArray = { MockAddress };
            string[] typeArray = { MockType };
            string[] DescArray = { MockDescription };
            string[] urlArray = { MockUrl };
            string[] imageArray = { MockImage };
            // Create a FormCollection object to hold mock form data.
            var formCol = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "Restaurant.Id", idArray},
                { "Restaurant.Title", nameArray },
                { "Restaurant.Neighborhood", neighborArray },
                { "Restaurant.State", stateArray },
                { "Restaurant.City", cityArray },
                { "Restaurant.Address", addressArray },
                { "Restaurant.CuisineType", typeArray},
                { "Restaurant.Description", DescArray},
                { "Restaurant.Url", urlArray },
                { "Restaurant.Image", imageArray},
            });
            return formCol;
        }

        #endregion TestSetup


        #region OnPost
        /// <summary>
        /// Tests when OnPost is called, creating a new restaurant should return valid ModelState
        /// and redirect to Index page.
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Valid_Model_State_And_Redirect_To_Index()
        {
            // Arrange          
            var formCol = GetMockFormCollection();
            // Link FormCollection object with HTTPContext.
            TestHelper.HttpContextDefault.Request.HttpContext.Request.Form = formCol;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert page is successful.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// Tests when OnPost is called, an invalid Model State should return false and redirect to
        /// the Index page.
        /// </summary>
        [Test]
        public void OnPost_InValid_ModelState_Should_Return_False_and_Redirect_To_Index()
        {
            // Arrange
            // Force an invalid error state
            pageModel.ModelState.AddModelError("InvalidState", "Invalid Restaurant state");

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        #endregion OnPost
    }
}
