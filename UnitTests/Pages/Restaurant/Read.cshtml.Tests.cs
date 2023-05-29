﻿using System.Linq;
using FoodieSeattle.WebSite.Models;
using NUnit.Framework;
using FoodieSeattle.WebSite.Services;
using Moq;
using FoodieSeattle.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodieSeattle.WebSite.Controllers;

namespace UnitTests.Pages.Restaurant.Read
{

    /// <summary>
    /// Unit testing for Read page
    /// </summary>
    public class ReadTests
    {
        #region SetUp
        // Set up ReadModel
        private ReadModel callReadModel;

        // Set up RestaurantModel List
        private List<RestaurantModel> expectedRestaurants;

        // Global valid existing Id property for use in tests
        private const string ExistingId = "kashiba-pic";

        /// <summary>
        /// Sets up the necessary objects and data for ReadTests. It creates a ReadModel
        /// instance and initializes an expectedRestaurants list
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Create ReadModel instance and expected restaurant list
            callReadModel = new ReadModel(TestHelper.RestaurantServiceObject);
            expectedRestaurants = TestHelper.RestaurantServiceObject.GetRestaurants().ToList();
        }

        #endregion SetUp

        #region OnGet

        /// <summary>
        /// Tests the "OnGet" method of the ReadModel class by checking if it returns
        /// the expected restaurant title for a valid restaurant ID.
        /// </summary>
        [Test]
        public void OnGet_Return_Same_Data()
        {
            //arrange
            var expectedRestaurant = TestHelper.RestaurantServiceObject.GetRestaurants().First().Title;

            //Act
            callReadModel.OnGet(ExistingId);
            var result = callReadModel.Restaurant.Title;

            //Assert
            Assert.AreEqual(expectedRestaurant, result);//match the result read unit


        }

        #endregion OnGet

        #region Password

        /// <summary>
        /// Tests that the Password property of a ReadModel instance can be set and
        /// retrieved correctly.
        /// </summary>
        [Test]
        public void PasswordProperty_SetGet_ReturnsCorrectValue()
        {
            // Arrange
       
            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            var expectedPassword = "test123";

            // Act
            model.Password = expectedPassword;
            var actualPassword = model.Password;

            // Assert
            Assert.AreEqual(expectedPassword, actualPassword);
        }

        /// <summary>
        /// Tests that the PasswordEntered property of a ReadModel instance can be set
        /// and retrieved correctly.
        /// </summary>
        [Test]
        public void PasswordEnterProperty_SetGet_ReturnsCorrectValue()
        {
            // Arrange

            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            var expectedPasswordEntered = true;

            // Act
            model.PasswordEntered = expectedPasswordEntered;
            var actualPasswordEntered = model.PasswordEntered;

            // Assert
            Assert.AreEqual(expectedPasswordEntered, actualPasswordEntered);
        }

        #endregion Password

        #region OnPostUnlock

        /// <summary>
        /// Tests that the OnPostUnlock method of a ReadModel instance returns a
        /// PageResult when a valid password is provided.
        /// </summary>
        [Test]
        public void OnPostUnlock_ValidPassword_ReturnsPageResult()
        {
            // Arrange
           
            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            model.Password = "6666";
            model.PageContext = new PageContext
            {
                RouteData = new Microsoft.AspNetCore.Routing.RouteData()
            };
            model.PageContext.RouteData.Values.Add("id", ExistingId); // Set the id value to a valid value for your test

            // Act
            var result = model.OnPostUnlock();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsTrue(model.PasswordEntered);
            Assert.IsFalse(model.IsPasswordInvalid);
        }

        /// <summary>
        /// Tests that the OnPost method of a ReadModel instance returns a
        /// PageResult with an error when an incorrect password is provided.
        /// </summary>
        [Test]
        public void OnPostUnlock_IncorrectPassword_ReturnsPageResultWithError()
        {
            // Arrange
            var model = new ReadModel(TestHelper.RestaurantServiceObject);
            model.Password = "1234"; // Use an incorrect password
            model.PageContext = new PageContext
            {
                RouteData = new Microsoft.AspNetCore.Routing.RouteData()
            };
            model.PageContext.RouteData.Values.Add("id", ExistingId);

            // Act
            var result = model.OnPostUnlock();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsFalse(model.PasswordEntered);
            Assert.IsTrue(model.IsPasswordInvalid);
        }

        #endregion OnPostUnlock

        #region OnPostAddComment

        /// <summary>
        /// Tests the OnPostAddComment function of the Read page,
        /// valid comment should return the page
        /// </summary>
        [Test]
        public void OnPostAddComment_Valid_Comment_Should_RedirectToPageResult()
        {

            // Arrange
            // test restaurant id
            var id = "tacos-chukis-pic";

            // test restaurant comment
            var comment = "Fantastic tacos!";


            // Act
            var result = callReadModel.OnPostAddComment(id, comment);

            // Assert
            Assert.AreEqual(true, callReadModel.ModelState.IsValid);
            Assert.AreEqual(typeof(RedirectToPageResult), result.GetType());
        }

        /// <summary>
        /// Tests the OnPostAddComment function of the Read page, valid comment
        /// should create a comment object
        /// </summary>
        [Test]
        public void OnPostAddComment_Valid_Comment_Should_Create_New_Comment_Object()
        {

            // Arrange
            // test restaurant id
            var id = "tacos-chukis-pic";

            // test restauratn comment
            var comment = "Fantastic tacos!";

            // Act
            // Result of OnPostAddComment
            var result = callReadModel.OnPostAddComment(id, comment);

            // Get product from database
            var restaurant = TestHelper.RestaurantServiceObject.GetRestaurants().First(p => p.Id == id);

            // Get new comment from product
            var newComment = restaurant.Comments[0];

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(comment, newComment.Comment);
        }

        #endregion OnPostAddComment

        #region GetAverageRate
        [Test]
        public void CalculateAverageRating_ReturnsZero_WhenRatingsIsEmpty()
        {
            // Arrange
            var readModel = new ReadModel(null); 
            readModel.Restaurant = new RestaurantModel { Ratings = new int[0] };

            // Act
            var averageRating = readModel.CalculateAverageRating();

            // Assert
            Assert.AreEqual(0.0, averageRating);
        }

        [Test]
        public void CalculateAverageRating_ReturnsZero_WhenRatingsIsNull()
        {
            // Arrange
            var readModel = new ReadModel(null); 
            readModel.Restaurant = new RestaurantModel { Ratings = null };

            // Act
            var averageRating = readModel.CalculateAverageRating();

            // Assert
            Assert.AreEqual(0.0, averageRating);
        }
        [Test]
        public void CalculateAverageRating_ReturnsCorrectAverageRating()
        {
            // Arrange
            var readModel = new ReadModel(null); 
            readModel.Restaurant = new RestaurantModel { Ratings = new int[] { 1,2,3,4,5 } };

            // Act
            var averageRating = readModel.CalculateAverageRating();

            // Assert
            Assert.AreEqual(3.0, averageRating);
        }

        #endregion GetAverageRate


        #region getVoteNumber
        [Test]
        public void GetVoteNum_correct_voteNum()
        {
            //Arrange
            var readModel = new ReadModel(null);
            readModel.Restaurant = new RestaurantModel { Ratings = new int[] { 1, 2, 3, 4, 5 } };

            // Act
            var voteNum = readModel.getVoteNumber();

            //Assert
            Assert.AreEqual(5, voteNum);


        }

        [Test]
        public void getVoteNum_ReturnsZero_WhenVoteIsNull()
        {
            // Arrange
            var readModel = new ReadModel(null);
            readModel.Restaurant = new RestaurantModel { Ratings = null };

            // Act
            var averageRating = readModel.getVoteNumber();

            // Assert
            Assert.AreEqual(0, averageRating);
        }



        [Test]
        public void getVoteNum_ReturnsZero_WhenVoteIsZero()
        {
            // Arrange
            var readModel = new ReadModel(null);
            readModel.Restaurant = new RestaurantModel { Ratings = new int[0] };

            // Act
            var averageRating = readModel.getVoteNumber();

            // Assert
            Assert.AreEqual(0, averageRating);
        }


        #endregion getVoteNumber

    }
}
