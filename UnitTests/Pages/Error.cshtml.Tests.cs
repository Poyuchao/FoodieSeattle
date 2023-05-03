using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Error
{
    public class ErrorTests
    {
        #region TestSetup


        /// <summary>
        /// The ErrorModel instance used for testing.
        /// </summary>
        public static ErrorModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            /// <summary>
            /// Create a mock logger for the ErrorModel instance.
            /// </summary>
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            /// <summary>
            /// Set up the ErrorModel instance with a PageContext and TempData.
            /// </summary>

            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test the OnGet method when a valid Activity is set.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange

            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }
        /// <summary>
        /// Test the OnGet method when an invalid Activity is set (i.e. null).
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }
        #endregion OnGet
    }
}