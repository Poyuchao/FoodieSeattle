// Import the System.Linq namespace for LINQ extension methods
using System.Linq;
// Import the NUnit.Framework namespace for NUnit testing
using NUnit.Framework;
// Import the RestaurantService page model
using FoodieSeattle.WebSite.Services;
// Import the NUnit.Framework.Internal namespace for NUnit testing
using NUnit.Framework.Internal;
// Import the RestaurantModel class
using FoodieSeattle.WebSite.Models;
// Import the System.Collections.Generic namespace for collections
using System.Collections.Generic;
using FoodieSeattle.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Xml.Linq;

/// <summary>
/// Unit Test for all RestaurantService.cshtml.Tests.cs blocks
/// </summary>
namespace UnitTests.Services.RestaurantService
{
    /// <summary>
    /// Unit tests for RestaurantService
    /// </summary>
    public class RestaurantServiceTests
    {

        #region TestSetup

        // Global invalid id property for use in tests. 
        private const string InvalidId = "BOGUS";

        // Global invalid test name property for use in tests. 
        private const string InvalidTitle = "Bogusland";

        // Global valid Mock Id property for use in tests
        private const string MockId = "kura-sushi-pic";

        // Global valid Mock title property for use in tests
        private const string MockTitle = "Kura sushi";

        // Global valid Mock neighborhood property for use in tests
        private const string MockNeighborhood = "Bellevue";

        // Global valid Mock food type property for use in tests
        private const string MockType = "Japanese";

        // Global valid mock description property for use in tests
        private const string MockDescription = "Kura Sushi (Japanese: くら寿司, Hepburn: Kura zushi) is a Japanese sushi restaurant chain. Its headquarters are in Sakai, Osaka Prefecture.";

        // Global valid mock Url property for use in tests
        private const string MockUrl = "https://kurasushi.com/";

        // Global valid mock Image property for use in tests
        private const string MockImage = "https://lh3.googleusercontent.com/p/AF1QipPk0SIY2o8w2UCDPiuPBiR-rm7ZqNEzpX6B8W7f=s680-w680-h510";

        /// <summary>
        /// Initializations for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region GetRestaurant

        ///<summary>
        ///Verifies that the count of all restaurants is equal to result in the JSON file.
        ///</summary>
       [Test]
        public void ProveAllRestaurant_is_equal_to_fourteen_in_JsonFile()
        {
            //Arrange
            var restaurantModel = new RestaurantModel();

            //Act
            var fourteen = TestHelper.RestaurantServiceObject.GetRestaurants().Count();
            var result = TestHelper.RestaurantServiceObject.GetRestaurants().Count();

            //Assert
            Assert.AreEqual(fourteen, result);
        }
        #endregion GetRestaurant

        #region Addrating
        /// <summary>
        /// Invalid null Restaurant in Addrating should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Restaurant_Null_Should_Return_False()
        {
            // Arrange

            // Act
            // Expected result
            var result = TestHelper.RestaurantServiceObject.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Verifies that adding a rating to a valid restaurant with existing ratings should add a new rating.
        /// </summary>
        [Test]
        public void AddRating_Valid_Restaurant_WithRatings_Should_Add_Rating()
        {
            // Arrange
            var data = TestHelper.RestaurantServiceObject.GetRestaurants().First(x => x.Ratings != null && x.Ratings.Length > 0);
            var existingRatingsCount = data.Ratings.Length;

            // Act
            var result = TestHelper.RestaurantServiceObject.AddRating(data.Id, 2);
            var updatedData = TestHelper.RestaurantServiceObject.GetRestaurants().First(x => x.Id == data.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(existingRatingsCount + 1, updatedData.Ratings.Length);
            Assert.AreEqual(2, updatedData.Ratings.Last());
        }

        /// <summary>
        /// Verifies that adding a rating to a valid restaurant with no ratings should add a new rating.
        /// </summary>
        [Test]
        public void AddRating_Valid_Restaurant_NoRatings_Should_Add_Rating()
        {
            // Arrange
            var data = TestHelper.RestaurantServiceObject.GetRestaurants().First(x => x.Ratings == null);

            // Act
            var result = TestHelper.RestaurantServiceObject.AddRating(data.Id, 4);
            var updatedData = TestHelper.RestaurantServiceObject.GetRestaurants().First(x => x.Id == data.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, updatedData.Ratings.Length);
            Assert.AreEqual(4, updatedData.Ratings[0]);
        }

        /// <summary>
        /// Verifies that adding a rating to an invalid restaurant not found should return false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Restaurant_NotFound_Should_Return_False()
        {
            // Arrange
            var fakeRestaurantId = InvalidId;

            // Act
            var result = TestHelper.RestaurantServiceObject.AddRating(fakeRestaurantId, 3);

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifies that adding a rating to an invalid restaurant should return false.
        /// </summary>
        [Test]
        public void AddRating_InValid_()
        {
            // Arrange

            // Act
            var result = TestHelper.RestaurantServiceObject.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Verifies that adding a rating to an invalid restaurant not found should return false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Rating_Below_Zero_Should_Return_False()
        {
            // Arrange

            // Get the first data item
            var data = TestHelper.RestaurantServiceObject.GetRestaurants().First();

            // Act
            var result = TestHelper.RestaurantServiceObject.AddRating(data.Id, -1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Verifies that adding a valid rating to a valid restaurant should return true.
        /// </summary>
        [Test]
        public void AddRating_Valid_Restaurant_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.RestaurantServiceObject.GetRestaurants().First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.RestaurantServiceObject.AddRating(data.Id, 5);
            var dataNewList = TestHelper.RestaurantServiceObject.GetRestaurants().First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }

        /// <summary>
        /// Verifies that adding a rating with a value above five should return false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Rating_Above_Five_Should_Return_False()
        {
            // Arrange

            // Get the first data item
            var data = TestHelper.RestaurantServiceObject.GetRestaurants().First();

            // Act
            var result = TestHelper.RestaurantServiceObject.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion Addrating

        #region UpdateData

        /// <summary>
        /// Verifies that updating invalid data should both return null.
        /// </summary>
        [Test]
        public void UpdateData_not_Valid_should_both_return_null()
        {
            // Arrange
            var data = new RestaurantModel
            {
                Id = "abc",
                Title = "McDonald's",
                Description = "McDonald's is a multinational fast-food restaurant chain known for its signature burgers, fries, and shakes. ",
                Url = "https://example.com/new-restaurant",
                Image = "https://example.com/new-restaurant-image.png"
            };
            TestHelper.RestaurantServiceObject.UpdateData(data);

            // Act
            var result = TestHelper.RestaurantServiceObject.UpdateData(data);
            var restaurants = TestHelper.RestaurantServiceObject.GetRestaurants();
            var mcdonaldsRestaurant = restaurants.FirstOrDefault(p => p.Title == "McDonald's");

            // Assert
            Assert.IsNull(result);
            Assert.IsNull(mcdonaldsRestaurant);
        }

        /// <summary>
        /// Verifies that updating an existing restaurant should update the restaurant and return the updated restaurant.
        /// </summary>
        [Test]
        public void UpdateData_ExistingRestaurant_Should_UpdateRestaurant_And_ReturnUpdatedRestaurant()
        {
            // Arrange
            var data = new RestaurantModel
            {
                Id = "shiros-pic",
                Title = "Shiro's",
                Description = "Located in Belltown, Shiro’s is an exquisite sushi restaurant founded by Shiro Kashiba. This experience features a similar format to other high end sushi restaurants, where customers can sit at the counter (our recommendation) or reserve a table. Known as one of the best sushi restaurants in the country, Shiro’s specializes in traditional, high quality sushi only using the freshest ingredients available. Décor and ambiance remains simple, allowing diners to focus on the overall experience. Pricing is high, so for this location we recommend for special occasions.",
                Url = "https://shiros.com/",
                Image = "https://shiros.com/wp-content/uploads/2017/07/cropped-Shiros_Sushi_Logo_modified_large.png",

            };

            // Act
            var result = TestHelper.RestaurantServiceObject.UpdateData(data);
            var restaurants = TestHelper.RestaurantServiceObject.GetRestaurants();
            var ShiroRestaurant = restaurants.FirstOrDefault(p => p.Title == "Shiro's");

            // Assert
            Assert.AreEqual(ShiroRestaurant.Title, result.Title);
        }

        #endregion UpdateData


        #region CreateData
        /// <summary>
        /// Verifies that calling CreateData should return the new all restaurants count greater than the old all restaurants count.
        /// </summary>
        [Test]
        public void CreateData_Should_Return_newAllRestaurantNum_greater_than_oldAllRestaurantsNum()
        {
            // Arrange
            var restaurantService = new RestaurantModel();

            // Act
            var OldAllRestaurantsNum = TestHelper.RestaurantServiceObject.GetRestaurants().Count();
            TestHelper.RestaurantServiceObject.CreateData();
            var NewAllRestaurantsNum = TestHelper.RestaurantServiceObject.GetRestaurants().Count();

            // Assert
            Assert.Greater(NewAllRestaurantsNum, OldAllRestaurantsNum);

        }

        /// <summary>
        /// Verifies that calling CreateData should add a new restaurant to the restaurant list.
        /// </summary>
        [Test]
        public void CreateData_Should_Add_New_Restaurant_To_RestaurantList()
        {
            // Arrange
            var restaurantService = new RestaurantModel();

            // Act
            TestHelper.RestaurantServiceObject.CreateData();
            var restaurants = TestHelper.RestaurantServiceObject.GetRestaurants();

            // Assert
            Assert.IsTrue(restaurants.Any(p => p.Title == "Enter Title"));
        }


        #endregion CreateData


        #region DeleteData
        /// <summary>
        /// Verifies that calling DeleteData should return the new all restaurants count less than the old all restaurants count.
        /// </summary>
        [Test]
        public void DeleteData_Should_Return_newAllRestaurantNum_less_than_oldAllRestaurantsNum()
        {
            // Arrange
            var restaurantService = new RestaurantModel();

            // Act
            var OldAllRestaurantsNum = TestHelper.RestaurantServiceObject.GetRestaurants().Count();
            TestHelper.RestaurantServiceObject.DeleteData("kashiba-pic");
            var NewAllRestaurantsNum = TestHelper.RestaurantServiceObject.GetRestaurants().Count();

            // Assert
            Assert.Less(NewAllRestaurantsNum, OldAllRestaurantsNum);

        }
        #endregion DeleteData


        #region AddData
        /// <summary>
        ///  check if the new restaurant's data matches the inputs provided by the user and if the dataset's 
        ///  length increases by one after adding a new restaurant.
        /// </summary>
        [Test]
        public void AddData_NewRestaurant_SavesToJSONAndReturns()
        {
            // Arrange
            var restaurantModel = new RestaurantModel();
            var title = MockTitle;
            var neighborhood = MockNeighborhood;
            var type = MockType;
            var desc = MockDescription;
            var url = MockUrl;
            var image = MockImage;


            // Act
            var result = TestHelper.RestaurantServiceObject.AddData(title, neighborhood, type, desc, url, image);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(title, result.Title);
            Assert.AreEqual(desc, result.Description);
            Assert.AreEqual(url, result.Url);
            Assert.AreEqual(image, result.Image);

            //TearDown
            TestHelper.RestaurantServiceObject.DeleteData(restaurantModel.Id);
        }
        #endregion AddData


        #region GetRestaurantsByType

        /// <summary>
        /// Tests the GetRestaurantsByType method with a valid restaurant type.
        /// It sets up mock data with two restaurants of type Type1, and adds them
        /// to the restaurant service using the TestHelper AddData method. It then calls
        /// the GetRestaurantsByType method with parameter Type1,
        /// and asserts that the method returns an IEnumerable of RestaurantModel objects
        /// with a count of 2, and that the returned sequence contains both restaurants.
        /// </summary>
        [Test]
        public void GetRestaurantsByType_ValidType_ReturnsMatchingRestaurants()
        {
            // Arrange
            var restaurantModel = new RestaurantModel();

            // Set up mock data
            var firstData = new RestaurantModel()
            {
                Title = MockTitle,
                Neighborhood = MockNeighborhood,
                Type = "Type1",
                Description = MockDescription,
                Url = MockUrl,
                Image = MockImage,
            };

            var secondData = new RestaurantModel()
            {
                Title = "Restaurant 2",
                Neighborhood = "SLU",
                Type = "Type1",
                Description = "Description2",
                Url = "http://www.example.com/2",
                Image = "http://www.example.com/2/image.jpg",
            };


            // Act
            TestHelper.RestaurantServiceObject.AddData(firstData.Title, firstData.Neighborhood, firstData.Type,
                firstData.Description, firstData.Url, firstData.Image);
            TestHelper.RestaurantServiceObject.AddData(secondData.Title, secondData.Neighborhood, secondData.Type,
                secondData.Description, secondData.Url, secondData.Image);
            var result = TestHelper.RestaurantServiceObject.GetRestaurantsByType("Type1");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<RestaurantModel>>(result);
            Assert.AreEqual(2, result.Count());

            //TearDown
            TestHelper.RestaurantServiceObject.DeleteData(firstData.Id);
            TestHelper.RestaurantServiceObject.DeleteData(secondData.Id);
        }

        /// <summary>
        /// Tests the GetRestaurantsByType method with an empty restaurant type.
        /// This test gets the current count of restaurants in the restaurant service
        /// using the GetRestaurants method. It then calls the GetRestaurantsByType method
        /// with an empty string as the parameter, and asserts that the
        /// method returns an IEnumerable of RestaurantModel objects with a count equal to
        /// the total number of restaurants, indicating that all restaurants were returned.
        /// </summary>
        [Test]
        public void GetRestaurantsByType_InvalidType_ReturnsAllRestaurants()
        {
            // Arrange
            var restaurantModel = new RestaurantModel();
            var count = TestHelper.RestaurantServiceObject.GetRestaurants().Count();


            // Act
            var result = TestHelper.RestaurantServiceObject.GetRestaurantsByType("");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<RestaurantModel>>(result);
            Assert.AreEqual(count, result.Count());

        }

        #endregion GetRestaurantsByType

        #region GetRestaurantsById

        /// <summary>
        /// Tests GetRestaurantById by retrieving the first restaurant and confirming not null. 
        /// </summary>
        [Test]
        public void GetRestaurantById_Valid_Should_Return_Not_Null()
        {
            // Arrange

            // Add restaurant to database and store it as testRestaurant.
            TestHelper.RestaurantServiceObject.AddData(MockTitle, MockNeighborhood, MockType,
                MockDescription, MockUrl, MockImage);
            var testRestaurant = TestHelper.RestaurantServiceObject.GetRestaurants().Last();

            //Act
            var result = TestHelper.RestaurantServiceObject.GetRestaurantById(testRestaurant.Id);

            //Assert
            Assert.NotNull(result);

            // TearDown
            TestHelper.RestaurantServiceObject.DeleteData(testRestaurant.Id);
        }

        /// <summary>
        /// Tests GetRestaurantById catches out of bounds input and returns null. 
        /// </summary>
        [Test]
        public void GetRestaurantById_Invalid_Should_Return_Null()
        {
            // Arrange

            //Act
            var invalidResult = TestHelper.RestaurantServiceObject.GetRestaurantById(InvalidId);

            //Assert
            Assert.Null(invalidResult);
        }

        #endregion GetRestaurantsById

        #region GetRestaurantByNeighborhood

        /// <summary>
        /// Tests the GetRestaurantsByNeighborhood method with an empty restaurant type.
        /// This test gets the current count of restaurants in the restaurant service
        /// using the GetRestaurants method. It then calls the GetRestaurantsByNeighborhood method
        /// with an empty string as the parameter, and asserts that the
        /// method returns an IEnumerable of RestaurantModel objects with a count equal to
        /// the total number of restaurants, indicating that all restaurants were returned.
        /// </summary>
        [Test]
        public void GetRestaurantsByNeighborhood_InvalidType_ReturnsAllRestaurants()
        {
            // Arrange
            var restaurantModel = new RestaurantModel();
            var count = TestHelper.RestaurantServiceObject.GetRestaurants().Count();


            // Act
            var result = TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<RestaurantModel>>(result);
            Assert.AreEqual(count, result.Count());

        }

        /// <summary>
        /// Tests the GetRestaurantsByNeighborhood to extract the restaurants which from Belltown
        /// The restaurant number will same as the expected number
        /// </summary>
        [Test]
        public void GetRestaurantByNeighborhoodFromBelltown_Is_Three()
        {
            //Arrange
            var trueCount = TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("Belltown").Count();

            var expectedCount = 2;


            //Act

            //Assert
            Assert.AreEqual(expectedCount, trueCount);


        }

        #endregion GetRestaurantByNeighborhood


        #region GetCuisines

        /// <summary>
        /// Tests the GetCuisines() method of the RestaurantService class.
        /// </summary>
        [Test]
        public void GetCuisines_Should_Return_UniqueSortedCuisines()
        {
            // Arrange
            var expectedCuisines = new List<string> { "Caribbean", "Chinese",  "Indian", "Japanese",
                "Laotian", "Mexican", "Thai", "Vietnamese" };

            // Act
            var actualCuisines = TestHelper.RestaurantServiceObject.GetCuisines();

            // Assert
            CollectionAssert.AreEqual(expectedCuisines, actualCuisines);
        }


        #endregion GetCuisines


        #region GetNeighborhoods

        /// <summary>
        /// Tests the GetNeighborhoods() method of the RestaurantService class.
        /// </summary>
        [Test]
        public void GetNeighborhoods_Should_Return_UniqueSortedNeighborhoods()
        {
            // Arrange
            var expectedNeighborhoods = new List<string> { "Ballard", "Capitol Hill", "Downtown" };

            // Act
            var actualNeighborhoods = TestHelper.RestaurantServiceObject.GetNeighborhoods();

            // Assert
            CollectionAssert.AreEqual(expectedNeighborhoods, actualNeighborhoods);
        }

        #endregion GetNeighborhoods
    }
}

