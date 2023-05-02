
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Pages.Restaurant;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Update
{
    /// <summary>
    /// Unit tests for the Update page.
    /// </summary>
    public class UpdateTests
    {
        // UpdateModel object.
        #region TestSetup
        public static UpdateModel pageModel;

        /// <summary>
        /// Initialize UpdateModel with a ProductService object. 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Initialize pageModel
            pageModel = new UpdateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Test that when OnGet is called, a valid Model State should return true 
        /// and a known Id should match the same item's title.
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
            // Set up test restaurant attributes
            string Id = "kashiba-pic";
            string Title = "Sushi Kashiba";

            // Act
            pageModel.OnGet(Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(Title, pageModel.Product.Title);
        }
        #endregion OnGet

        /// <summary>
        /// A unit test method that tests the OnPost() method of a page model when
        /// valid data is provided. The test verifies that the OnPost() method redirects
        /// to the Index page and that the ModelState is valid.
        /// </summary>
        #region OnPost
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange
            // Set up testing ProductModel object with sample data
            pageModel.Product = new ProductModel
            {
                Id = "mamnoon-pic",
                Title = "Mamnoon",
                Description = "description",
                Url = "url",
                Image = "image"
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
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion OnPost
    }
}