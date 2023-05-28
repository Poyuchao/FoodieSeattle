using NUnit.Framework;
using FoodieSeattle.WebSite.Components;
using FoodieSeattle.WebSite.Services;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Bunit;

namespace UnitTests.Components
{
    ///<summary>
    /// Unit tests for testing NeighborhoodList.razor component
    ///</summary>
    public class NeighborhoodListTests : BunitTestContext
    {
        #region Default

        ///<summary>
        /// Test that one of the restaurants stored in restaurants.json is returned when
        /// selecting Capitol Hill neighborhood
        ///</summary>
        [Test]
        public void NeighborhoodList_Capitol_Hill_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);

            // Act
            var page = RenderComponent<NeighborhoodList>(parameters => parameters.Add(p => p.Restaurants,
                TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("Capitol Hill")));

            // Get the Cards returned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("Ba Bar"));
        }

        #endregion Default

        #region SelectNeighborhood

        ///<summary>
        /// Test that content is returned when selecting the More Info button for the Ballard neighborhood
        ///</summary>
        [Test]
        public void SelectNeighborhood_Valid_ID_Gracia_should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);

            // Get the ID of the restaurant to select
            var id = "MoreInfoButton_gracia-pic";

            // Render the page
            var page = RenderComponent<NeighborhoodList>(parameters=>parameters.Add(p=>p.Restaurants,
                TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("Ballard")));

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("Gracia is a Mexican restaurant"));
        }

        #endregion SelectNeighbohood

        #region SubmitRating

        ///<summary>
        /// Test that a ballard restaurant with no votes is updated properly after the first vote
        ///</summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);

            // Get the ID of the restaurant to select
            var id = "MoreInfoButton_un-bien-pic";

            // Render the page
            var page = RenderComponent<NeighborhoodList>(parameters => parameters.Add(p => p.Restaurants,
                TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("Ballard")));

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
            var preVoteCountSpan = starButtonList[1];

            // Get the Vote Count
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

            // Get the Vote Count
            var postVoteCountSpan = starButtonList[1];

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("Be the first to vote!"));
            Assert.AreEqual(true, postVoteCountString.Contains("1 Vote"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }

        ///<summary>
        /// Test that a restaurant in the Belltown neighborhood with votes updates a new vote properly
        ///</summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count_And_Leave_Star_Check_Remaining()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);

            // Get the ID of the restaurant to select
            var id = "MoreInfoButton_shiros-pic";

            // Render the page
            var page = RenderComponent<NeighborhoodList>(parameters => parameters.Add(p => p.Restaurants,
                TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("Belltown")));

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
            var preVoteCountSpan = starButtonList[1];

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
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

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
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

        #region AddComment

        /// <summary>
        /// Tests the functionality of the "Add Comment" button by verifying that it shows
        /// the input field and the "Save Comment" button after being clicked. 
        /// </summary>
        [Test]
        public void AddCommentButton_ShouldShowInputAndSaveCommentButton()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);
            var page = RenderComponent<NeighborhoodList>(parameters => parameters.Add(p => p.Restaurants,
                TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("Belltown")));
            var id = "MoreInfoButton_kashiba-pic";

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Act
            page.Find("#AddComment").Click();

            // Assert
            Assert.IsNotNull(page.Find("input[type=text]"));
            Assert.IsNotNull(page.Find("button.btn.btn-success"));
        }

        /// <summary>
        /// Tests the functionality of saving a comment by clicking the Save Comment button.
        /// It verifies that a new comment is added to the list of comments after the Save Comment
        /// button is clicked. 
        /// </summary>
        [Test]
        public void SaveCommentButton_ShouldAddNewCommentToList()
        {
            // Arrange
            Services.AddSingleton<RestaurantService>(TestHelper.RestaurantServiceObject);
            var component = RenderComponent<NeighborhoodList>(parameters => parameters.Add(p => p.Restaurants,
                TestHelper.RestaurantServiceObject.GetRestaurantsByNeighborhood("Belltown")));
            var id = "MoreInfoButton_kashiba-pic";

            // Find the Buttons (more info)
            var buttonList = component.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();
            component.Find("#AddComment").Click();
            var input = component.Find("input[type=text]");
            var saveButton = component.Find("button.btn.btn-success");

            // Act

            // simulate entering a new comment
            input.Change("This is a new comment");

            // simulate Save Comment button click
            saveButton.Click();

            // Assert
            Assert.IsTrue(component.Markup.Contains("This is a new comment"));
        }

        #endregion AddComment
    }

}