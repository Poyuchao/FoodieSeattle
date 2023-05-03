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

        // Set up ProductModel List
        private List<ProductModel> expectedProducts;

        // Global valid existing Id property for use in tests
        private const string ExistingId = "kashiba-pic";

        /// <summary>
        /// Sets up the necessary objects and data for ReadTests. It creates a ReadModel
        /// instance and initializes an expectedProducts list
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Create ReadModel instance and expected product list
            callReadModel = new ReadModel(TestHelper.ProductService);
            expectedProducts = TestHelper.ProductService.GetProducts().ToList();
        }

        /// <summary>
        /// Tests the "OnGet" method of the ReadModel class by checking if it returns
        /// the expected product title for a valid product ID.
        /// </summary>
        [Test]
        public void OnGet_Retuen_Same_Data()
        {
            //arrange
            var expectedProduct = TestHelper.ProductService.GetProducts().First().Title;

            //Act
             callReadModel.OnGet(ExistingId);
            var result = callReadModel.Product.Title;

            //Assert
            Assert.AreEqual(expectedProduct, result);//match the result read unit

            
        }



    }
}
