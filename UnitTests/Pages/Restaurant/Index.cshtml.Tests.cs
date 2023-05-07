// Import the System.Linq namespace for LINQ extension methods
using System.Linq;
// Import the NUnit.Framework namespace for NUnit testing
using NUnit.Framework;
// Import the Restaurant page model
using ContosoCrafts.WebSite.Pages.Restaurant;
// Import the NUnit.Framework.Internal namespace for NUnit testing
using NUnit.Framework.Internal;
// Import the RestaurantModel class
using ContosoCrafts.WebSite.Models;
// Import the System.Collections.Generic namespace for collections
using System.Collections.Generic;


namespace UnitTests.Pages.Restaurant.Index
{
    /// <summary>
    /// Unit testing for the Restaurant/Index page
    /// </summary>
    public class IndexTests
    {
        // Declare a private field for the Restaurant page model
        private IndexModel IndexModel;
        // Declare a private field for the expected list of restaurants
        private List<RestaurantModel> expectedRestaurants;

        [SetUp]
        /// <summary>
        /// Sets up the necessary objects and data for the test suite or class. Creates
        /// an instance of the Restaurant/IndexModel page model with a test RestaurantService
        /// dependency using the TestHelper class. It also initializes an expectedRestaurants list.
        /// </summary>
        public void SetUp()
        {
            // Instantiate the Restaurant page model with a test RestaurantService dependency
            IndexModel = new IndexModel(TestHelper.RestaurantServiceObject);
            // Initialize the expected list of restaurants to the result of calling ToList()
            // on the restaurants returned by the test RestaurantService
            expectedRestaurants = TestHelper.RestaurantServiceObject.GetRestaurants().ToList();
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
            // Get the expected number of restaurants by calling Count() on the restaurants
            // returned by the test RestaurantService
            var expectedRestaurantNums = TestHelper.RestaurantServiceObject.GetRestaurants().Count();

            // Act
            // Call the OnGet() method of the Restaurant page model to get the actual restaurants
            IndexModel.OnGet();
            // Get the actual number of restaurants by calling Count() on the Restaurants
            // property of the Restaurant page model
            var actualRestaurantNums = IndexModel.Restaurants.Count();

            // Assert
            // Compare the expected number of restaurants to the actual number of restaurants
            // and assert that they are equal
            Assert.AreEqual(expectedRestaurants.Count, actualRestaurantNums);
        }
    }

}


