using System.Linq;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using Moq;
using ContosoCrafts.WebSite.Pages.Restaurant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UnitTests.Pages.Restaurant.Read
{

    /// <summary>
    /// Unit tests for Read page
    /// </summary>

    public class ReadTests
    {
        private ReadModel callReadModel; 

        private List<ProductModel> expectedProducts;

        [SetUp]
        public void SetUp()
        {
            // Create ReadModel instance and expected product list
            callReadModel = new ReadModel(TestHelper.ProductService);
            expectedProducts = TestHelper.ProductService.GetProducts().ToList();
        }

        [Test]

        public void OnGet_Retuen_Same_Data()
        {
            //arrange

            var expectedProduct = TestHelper.ProductService.GetProducts().First().Title;

            //Act
             callReadModel.OnGet("kashiba-pic");
            var result = callReadModel.Product.Title;

            //Assert

            Assert.AreEqual(expectedProduct, result);

            
        }



    }
}
