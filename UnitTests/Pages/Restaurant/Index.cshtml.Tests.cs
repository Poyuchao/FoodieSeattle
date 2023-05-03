// Import the System.Linq namespace for LINQ extension methods
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




namespace UnitTests.Pages.Restaurant.Index
{
    /// <summary>
    /// Unit testing for the Restaurant/Index page
    /// </summary>
    public class Index
    {
        // Declare a private field for the Restaurant page model
        private IndexModel IndexModel;
        // Declare a private field for the expected list of products
        private List<ProductModel> expectedProducts;

        [SetUp]
        /// <summary>
        /// Sets up the necessary objects and data for the test suite or class. Creates
        /// an instance of the Restaurant/IndexModel page model with a test ProductService
        /// dependency using the TestHelper class. It also initializes an expectedProducts list.
        /// </summary>
        public void SetUp()
        {
            // Instantiate the Restaurant page model with a test ProductService dependency
            IndexModel = new IndexModel(TestHelper.ProductService);
            // Initialize the expected list of products to the result of calling ToList()
            // on the products returned by the test ProductService
            expectedProducts = TestHelper.ProductService.GetProducts().ToList();
        }

        /// <summary>
        /// tests the "OnGet" method of the Restaurant/IndexModel page model by checking
        /// if it returns all the expected restaurants in data store (i.e. returns
        /// correct count).
        /// </summary>
                [Test]
        public void OnGet_ReturnsAllRestaurantNums()
        {
            // Arrange
            // Get the expected number of products by calling Count() on the products
            // returned by the test ProductService
            var expectedRestaurantNums = TestHelper.ProductService.GetProducts().Count();

            // Act
            // Call the OnGet() method of the Restaurant page model to get the actual products
            IndexModel.OnGet();
            // Get the actual number of products by calling Count() on the Products
            // property of the Restaurant page model
            var actualRestaurantNums = IndexModel.Products.Count();

            // Assert
            // Compare the expected number of products to the actual number of products
            // and assert that they are equal
            Assert.AreEqual(expectedProducts.Count, actualRestaurantNums);
        }
    }

}


