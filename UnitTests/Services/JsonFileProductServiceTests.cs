// Import the System.Linq namespace for LINQ extension methods
using System.Linq;
// Import the NUnit.Framework namespace for NUnit testing
using NUnit.Framework;
// Import the JsonFileProductService page model
using ContosoCrafts.WebSite.Services;
// Import the NUnit.Framework.Internal namespace for NUnit testing
using NUnit.Framework.Internal;
// Import the ProductModel class
using ContosoCrafts.WebSite.Models;
// Import the System.Collections.Generic namespace for collections
using System.Collections.Generic;
using ContosoCrafts.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Unit Test for all JsonFileProductServiceTests.cs blocks
/// </summary>
namespace UnitTests.Services.JsonFileProductServiceTests
{
    
    public class JsonFileProductServiceTests
	{

        #region TestSetup
        /// <summary>
        /// Initializations for all tests to be conducted
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion TestSetup



        #region GetProduct
        ///<summary>
        ///Verifies that the count of all restaurants is equal to fourteen in the JSON file.
        ///</summary>
        public void ProveAllRestaurant_is_equal_to_fourteen_in_JsonFile()
        {
            //Arrange
            var productService = new ProductModel();

            //Act
            var fourteen = 14;
            var result = TestHelper.ProductService.GetProducts().Count();

            //Assert
            Assert.AreEqual(fourteen, result);
        }
        #endregion GetProduct


        #region Addrating
        /// <summary>
        /// Invalid null product in Addrating should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            // Expected result
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }


        /// <summary>
        /// Verifies that adding a rating to a valid product with existing ratings should add a new rating.
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_WithRatings_Should_Add_Rating()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().First(x => x.Ratings != null && x.Ratings.Length > 0);
            var existingRatingsCount = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 2);
            var updatedData = TestHelper.ProductService.GetProducts().First(x => x.Id == data.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(existingRatingsCount + 1, updatedData.Ratings.Length);
            Assert.AreEqual(2, updatedData.Ratings.Last());
        }



        /// <summary>
        /// Verifies that adding a rating to a valid product with no ratings should add a new rating.
        /// </summary>

        [Test]
        public void AddRating_Valid_Product_NoRatings_Should_Add_Rating()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().First(x => x.Ratings == null);

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 4);
            var updatedData = TestHelper.ProductService.GetProducts().First(x => x.Id == data.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, updatedData.Ratings.Length);
            Assert.AreEqual(4, updatedData.Ratings[0]);
        }


        /// <summary>
        /// Verifies that adding a rating to an invalid product not found should return false.
        /// </summary>

        [Test]
        public void AddRating_InValid_Product_NotFound_Should_Return_False()
        {
            // Arrange
            var fakeProductId = "fakeProductId";

            // Act
            var result = TestHelper.ProductService.AddRating(fakeProductId, 3);

            // Assert
            Assert.IsFalse(result);
        }


        /// <summary>
        /// Verifies that adding a rating to an invalid product should return false.
        /// </summary>

        [Test]
        public void AddRating_InValid_()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }


        /// <summary>
        /// Verifies that adding a rating to an invalid product not found should return false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Rating_Below_Zero_Should_Return_False()
        {
            // Arrange

            // Get the first data item
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, -1);

            // Assert
            Assert.AreEqual(false, result);
        }



        /// <summary>
        /// Verifies that adding a valid rating to a valid product should return true.
        /// </summary>

        [Test]
        public void AddRating_Valid_Product_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetProducts().First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetProducts().First();

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
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

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
            var data = new ProductModel
            {
                Id = "abc",
                Title = "McDonald's",
                Description = "McDonald's is a multinational fast-food restaurant chain known for its signature burgers, fries, and shakes. ",
                Url = "https://example.com/new-product",
                Image = "https://example.com/new-product-image.png"
            };
            TestHelper.ProductService.UpdateData(data);

            // Act
            var result = TestHelper.ProductService.UpdateData(data);
            var restaurants = TestHelper.ProductService.GetProducts();
            var mcdonaldsRestaurant = restaurants.FirstOrDefault(p => p.Title == "McDonald's");

            // Assert
            Assert.IsNull(result);
            Assert.IsNull(mcdonaldsRestaurant);
        }

        [Test]
        public void UpdateData_ExistingProduct_Should_UpdateProduct_And_ReturnUpdatedProduct()
        {
            // Arrange
            var data = new ProductModel
            {
               Id= "shiros-pic",
               Title= "Shiro's",
               Description= "Located in Belltown, Shiro’s is an exquisite sushi restaurant founded by Shiro Kashiba. This experience features a similar format to other high end sushi restaurants, where customers can sit at the counter (our recommendation) or reserve a table. Known as one of the best sushi restaurants in the country, Shiro’s specializes in traditional, high quality sushi only using the freshest ingredients available. Décor and ambiance remains simple, allowing diners to focus on the overall experience. Pricing is high, so for this location we recommend for special occasions.",
               Url= "https://www.hackster.io/agent-hawking-1/book-light-dee7e4",
               Image= "https://shiros.com/wp-content/uploads/2017/07/cropped-Shiros_Sushi_Logo_modified_large.png",  
                
            };
            // Act
            var result = TestHelper.ProductService.UpdateData(data);
            var restaurants = TestHelper.ProductService.GetProducts();
            var ShiroRestaurant = restaurants.FirstOrDefault(p => p.Title == "Shiro's");
            // Assert
            Assert.AreEqual(ShiroRestaurant.Title, result.Title);
        }

        #endregion UpdateData


        #region CreateData
        [Test]
        public void CreateData_Should_Return_newAllProductNum_greater_than_oldAllProductsNum()
        {
            // Arrange
            var productService = new ProductModel();

            // Act
            var OldAllProductsNum = TestHelper.ProductService.GetProducts().Count();
            TestHelper.ProductService.CreateData();
            var NewAllProductsNum = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.Greater(NewAllProductsNum, OldAllProductsNum);

        }

        [Test]
        public void CreateData_Should_Add_New_Product_To_ProductList()
        {
            // Arrange
            var productService = new ProductModel();

            // Act
            TestHelper.ProductService.CreateData();
            var products = TestHelper.ProductService.GetProducts();

            // Assert
            Assert.IsTrue(products.Any(p => p.Title == "Enter Title"));
        }


        #endregion CreateData

        

        #region DeleteData
        [Test]
        public void DeleteData_Should_Return_newAllProductNum_less_than_oldAllProductsNum()
        {
            // Arrange
            var productService = new ProductModel();

            // Act
            var OldAllProductsNum = TestHelper.ProductService.GetProducts().Count();
            TestHelper.ProductService.DeleteData("kashiba-pic");
            var NewAllProductsNum = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.Less(NewAllProductsNum, OldAllProductsNum);

        }
        #endregion DeleteData



    }



}

