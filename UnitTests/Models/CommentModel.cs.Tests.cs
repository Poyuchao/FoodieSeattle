using System;
using FoodieSeattle.WebSite.Models;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FoodieSeattle.WebSite.Tests.Models
{
    /// <summary>
    /// Unit tests for CommentModel
    /// </summary>
    public class CommentModelTests
    {

        #region CommentModel

        /// <summary>
        /// Test verifies that 'Id' property of CommentModel is properly generating
        /// unique Ids for new comments.Two comment instances will be created to
        /// compare, comment1 and comment2. The test passes if the 'Id' values of comment1
        /// and comment2 are not equal, indicating that each instance has a unique ID
        /// identifier.
        /// </summary>
        [Test]
        public void CommentModel_Id_Should_Be_Unique()
        {
            // Arrange
            var comment1 = new CommentModel();
            var comment2 = new CommentModel();

            // Act

            // Assert
            Assert.AreNotEqual(comment1.Id, comment2.Id);
        }

        /// <summary>
        /// Test verifies that the 'Comment' property of the CommentModel class can be
        /// properly set and retrieved. It creates an instance of CommentModel, initializes
        /// an expected comment value, and assigns it to the 'Comment' property. The assigned
        /// value and the retrieved value from the 'Comment' property must be equal.
        /// </summary>
        [Test]
        public void CommentModel_Comment_Should_Set_And_Get_Comment_Value()
        {
            // Arrange
            var comment = new CommentModel();
            var expectedComment = "Test comment.";

            // Act
            comment.Comment = expectedComment;

            // Assert
            Assert.AreEqual(expectedComment, comment.Comment);
        }

        /// <summary>
        /// Test verifies that the 'set' portion of the 'Id' property in CommentModel generates
        /// a unique value using an instance of CommentModel. It will create an instance of
        /// CommentModel and retrieves the initial 'Id' value. It will then set a custom value
        /// for 'Id' property and retrieves the updated 'Id' value. The original and updated
        /// 'Id' will be compared and test will assert that they are unique.
        /// </summary>
        [Test]
        public void CommentModel_Id_Set_Should_Generate_Unique_Value()
        {
            // Arrange
            var comment = new CommentModel();

            // Act
            var firstId = comment.Id;
            comment.Id = "CustomId";
            var secondId = comment.Id;

            // Assert
            Assert.AreNotEqual(firstId, secondId);
            Assert.AreEqual("CustomId", secondId);
        }

        #endregion CommentModel
    }
}
