using Bunit;
using NUnit.Framework;

using FoodieSeattle.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using FoodieSeattle.WebSite.Services;
using FoodieSeattle.WebSite.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework.Internal;
using System.Collections;
using System.Threading.Channels;


namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for RestaurantList Razor Page.
    /// </summary>
    public class RestaurantListTests : BunitTestContext
    {
        #region TestSetup

        /// <summary>
        /// Initialize tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        /// <summary>
        /// Verifies that the default restaurant list page displays the expected content.
        /// </summary>
        [Test]
        public void RestaurantList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);

            // Act
            var page = RenderComponent<RestaurantList>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("Ba Bar"));
        }

        #region SelectRestaurant

        /// <summary>
        /// Verifies that selecting a valid restaurant ID displays the expected content
        /// on the page.
        /// </summary>
        [Test]
        public void SelectRestaurant_Valid_ID_sushi_kashiba_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);
            var id = "MoreInfoButton_kashiba-pic";


            var page = RenderComponent<RestaurantList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("https://s3.amazonaws.com/hoth.bizango/images/693886/ChefKashibaBehindSushiBar-fixed_feature.jpg"));
        }

        #endregion SelectRestaurant

        #region SubmitRating

        /// <summary>
        /// This test tests that the SubmitRating will change the vote as
        /// well as the Star checked. Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed
        /// The test needs to open the page, then open the popup on the card. Then
        /// record the state of the count and star check status. Then check a star
        /// Then check again the state of the cound and star check status
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);
            var id = "dough-zone-pic";

            var page = RenderComponent<RestaurantList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count
            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            //Assert.AreEqual(true, preVoteCountString.Contains("Be the first to vote!"));
            Assert.AreEqual(true, preVoteCountString.Contains("Be the first to vote"));
            Assert.AreEqual(true, postVoteCountString.Contains("1 Vote"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }

        // ACTIVATE TEST ONCE RATINGS SYSTEM IS FIXED

        /// <summary>
        /// This test tests that the SubmitRating will change the vote as well as the Star checked
        /// Because the star check is a calculation of the ratings, using a record that has no stars
        /// and checking one makes it clear what was changed. The test needs to open the page,
        /// then open the popup on the card. Then record the state of the count and star check status.
        /// Then check a star. Then check again the state of the cound and star check status
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count_And_Leave_Star_Check_Remaining()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);
            var id = "MoreInfoButton_kashiba-pic";

            var page = RenderComponent<RestaurantList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the Last star item from the list, it should one that is checked
            var starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));


            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("6 Votes"));
            Assert.AreEqual(true, postVoteCountString.Contains("7 Votes"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }
        #endregion SubmitRating

    }
}
