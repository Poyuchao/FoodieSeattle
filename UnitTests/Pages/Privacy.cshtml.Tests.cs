using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Unit tests for the Privacy Page.
    /// </summary>
    public class PrivacyTests
    {
        #region TestSetup
        // PrivacyModel object.
        public static PrivacyModel pageModel;

        /// <summary>
        /// Initialize the mock logger and set up PrivacyModel with PageContext
        /// and TempData
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Initialize MockLoggerDirect
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();

            // Initialize PageModel
            pageModel = new PrivacyModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test that when OnGet is called, Privacy PageModel returns a
        /// valid Id. 
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet
    }
}