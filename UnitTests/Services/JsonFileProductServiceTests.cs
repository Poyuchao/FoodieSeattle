using System.Linq;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using Moq;
using ContosoCrafts.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UnitTests.Services
{
	public class JsonFileProductServiceTests
	{
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
            //callJson = new JsonFileProductService(TestHelper.ProductService);
        }

        #endregion TestSetup

        #region AddRating
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act

            //var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            //Assert.AreEqual(false, result);
        }

        #endregion AddRating
    }
}

