using System.Linq;

using NUnit.Framework;

using ContosoCrafts.WebSite.Pages.Restaurant;


namespace UnitTests.Pages.Product.Create
{
    /// <summary>
    /// Unit tests for create page
    /// </summary>
    public class CreateTests
    {
        // CreateModel object
        #region TestSetup
        public static CreateModel pageModel;

        /// <summary>
        /// Initialize CreateModel field
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Initialize pageModel
            pageModel = new CreateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Tests that when OnGet is called, the Create page will add a valid
        /// item and the total item count in the datastore increases by the correct
        /// amount
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Restaurants()
        {
            // Arrange
            // Get the total number of current items in the datastore
            var oldCount = TestHelper.ProductService.GetProducts().Count();

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(oldCount + 1, TestHelper.ProductService.GetProducts().Count());
        }
        #endregion OnGet
    }
}