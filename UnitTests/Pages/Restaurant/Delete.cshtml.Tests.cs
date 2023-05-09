using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitTests.Pages.Restaurant.Delete
{
    /// <summary>
    /// Manage the deletion of data for a single record
    /// </summary>
    public class DeleteTests
    {
        // Global existing valid title property for use in tests
        private const string title = "Sushi Kashiba";

        // Global existing valid Id property for use in tests
        private const string Id = "kashiba-pic";

        // Global ErrorAttribute property for invalid model state
        private const string ErrorAttribute = "bogus";

        // Global Error to test invalid model state
        private const string Error = "bogus error";

        // Represents test setup region for DeleteModel pageModel
        #region TestSetup
        public static DeleteModel pageModel;

        /// <summary>
        /// Initializes the test environment by creating a new DeleteModel
        /// instance with a given RestaurantService.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Create new DeleteModel instance with RestaurantService
            pageModel = new DeleteModel(TestHelper.RestaurantServiceObject)
            {
                PageContext = TestHelper.InitiatePageContext()
            };
        }

        #endregion TestSetup


        /// <summary>
        /// Tests whether the OnGet() method of a PageModel object returns valid
        /// restaurants given a valid restaurant ID.
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Restaurants()
        {
            // Arrange

            // Act
            // Call the OnGet() method of the PageModel object with a valid restaurant ID.
            pageModel.OnGet(Id);

            // Assert
            // Check that the ModelState of the PageModel object is valid and that the
            // Name of the returned Restaurant matches the expected value.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(title, pageModel.Restaurant.Title);
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
                Id = "MockId",
                Title = "MockTitle",
                Neighborhood = "MockNeighborhood",
                Type = "MockType",
                Description = "MockDescription",
                Url = "MockUrl",
                Image = "MockImage"
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
        /// This function tests the behavior of the "OnGet" method in the Delete CRUDi Razor Page,
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
                Neighborhood = null,
                Type = null,
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
        /// Tests the OnPostAsync method of a page model to ensure that it
        /// correctly deletes a restaurant and redirects to the Index page upon completion.
        /// </summary>
        #region OnPostAsync
        [Test]
        public void OnPostAsync_Valid_Should_Return_Restaurants()
        {
            // Arrange

            // First Create the restaurant to delete
            pageModel.Restaurant = TestHelper.RestaurantServiceObject.CreateData();
            pageModel.Restaurant.Title = "Example to Delete";
            TestHelper.RestaurantServiceObject.UpdateData(pageModel.Restaurant);

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));

            // Confirm the item is deleted
            Assert.AreEqual(null, TestHelper.RestaurantServiceObject.GetRestaurants().FirstOrDefault(m => m.Id.Equals(pageModel.Restaurant.Id)));
        }

        /// <summary>
        /// This function tests the OnPostAsync method of a page model to ensure that it
        /// returns the current page when the model state is invalid.
        /// </summary>
        [Test]
        public void OnPostAsync_InValid_Model_NotValid_Return_Page()
        {
            // Arrange
            pageModel.Restaurant = new RestaurantModel
            {
                Id = ErrorAttribute,
                Title = ErrorAttribute,
                Description = ErrorAttribute,
                Url = ErrorAttribute,
                Image = ErrorAttribute
            };

            // Force an invalid error state
            pageModel.ModelState.AddModelError(ErrorAttribute, Error);

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion OnPostAsync
    }
}