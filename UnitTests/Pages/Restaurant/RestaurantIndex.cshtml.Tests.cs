﻿// Import the System.Linq namespace for LINQ extension methods
using System.Linq;
// Import the NUnit.Framework namespace for NUnit testing
using NUnit.Framework;
// Import the Restaurant page model
using ContosoCrafts.WebSite.Pages.Restaurant;
// Import the NUnit.Framework.Internal namespace for NUnit testing
using NUnit.Framework.Internal;
// Import the ProductModel class
using ContosoCrafts.WebSite.Models;
// Import the System.Collections.Generic namespace for collections
using System.Collections.Generic;




namespace UnitTests.Pages.RestaurantIndex
{
    public class RestaurantIndexTest
    {
        // Declare a private field for the Restaurant page model
        private RestaurantIndexModel IndexModel;
        // Declare a private field for the expected list of products
        private List<ProductModel> expectedProducts;

        [SetUp]
        // Declare a setup method to instantiate the Restaurant page model and initialize the expected list of products
        public void SetUp()
        {
            // Instantiate the Restaurant page model with a test ProductService dependency
            IndexModel = new RestaurantIndexModel(TestHelper.ProductService);
            // Initialize the expected list of products to the result of calling ToList() on the products returned by the test ProductService
            expectedProducts = TestHelper.ProductService.GetProducts().ToList();
        }

        [Test]
        public void OnGet_ReturnsAllRestaurantNums()
        {
            // Arrange
            // Get the expected number of products by calling Count() on the products returned by the test ProductService
            var expectedRestaurantNums = TestHelper.ProductService.GetProducts().Count();

            // Act
            // Call the OnGet() method of the Restaurant page model to get the actual products
            IndexModel.OnGet();
            // Get the actual number of products by calling Count() on the Products property of the Restaurant page model
            var actualRestaurantNums = IndexModel.Products.Count();

            // Assert
            // Compare the expected number of products to the actual number of products and assert that they are equal
            Assert.AreEqual(expectedProducts.Count, actualRestaurantNums);
        }
    }

}

