using System.Linq;

using NUnit.Framework;

using ContosoCrafts.WebSite.Pages.Restaurant;


namespace UnitTests.Pages.Product.Create
{
    public class CreateTests
    {
        #region TestSetup
        public static CreateModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new CreateModel(TestHelper.ProductService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange
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