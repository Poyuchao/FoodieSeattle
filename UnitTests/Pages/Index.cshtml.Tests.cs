using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;


namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for the Products Index Page.
    /// </summary>
    public class IndexTests
    {
        #region TestSetup

        // IndexModel object.
        private static IndexModel _pageModel;

        /// <summary>
        /// Initialize mock Logger. 
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var mockLoggerDirect = Mock.Of<ILogger<IndexModel>>();

            _pageModel = new IndexModel(mockLoggerDirect, TestHelper.ProductService);
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests that when OnGet is called, the Index PageModel is valid. 
        /// </summary>
        [Test]
        public void OnGet_Valid_ModelState_IsValid_Should_Return_True()
        {
            // Arrange

            // Act
            _pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, _pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// Tests that when OnGet is called, products are returned. 
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            _pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, _pageModel.Products.ToList().Any());
        }

        #endregion OnGet
    }
}