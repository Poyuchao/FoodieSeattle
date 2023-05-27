using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodieSeattle.WebSite.Pages;
using NUnit.Framework;

namespace UnitTests.Pages

{
    /// <summary>
    /// Unit testing for the Search Page
    /// </summary>
    public class SearchTests
    {
        #region TestingSetup

        // Page model for the Search page
        public static SearchModel pageModel;

        /// <summary>
        /// Initializes the pageModel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new SearchModel(TestHelper.RestaurantServiceObject)
            {
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests the OnGet function of the Search page, should return requested restaurant
        /// for a valid search
        /// </summary>
        [Test]
        public void OnGet_Valid_Search_Should_Return_RequestId()
        {

            // Arrange
            pageModel.Query = "Ba Bar";

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, pageModel.Results.ToList().Any());
        }

        #endregion OnGet
    }
}
