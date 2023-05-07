﻿
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Moq;

using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Internal;

namespace UnitTests
{
    /// <summary>
    /// Test helper to hold web start settings including the following:
    /// HttpClient, Action Context, View Data and Temp Data services
    /// </summary>
    public static class TestHelper
    {
        // Mock instance IWebHostEnvironment interface
        public static Mock<IWebHostEnvironment> MockWebHostEnvironment;

        // Test instance of IUrlHelperFactory to build URLs
        public static IUrlHelperFactory UrlHelperFactory;

        // Test instance of HttpContext class
        public static DefaultHttpContext HttpContextDefault;

        // Test instance of IWebHostEnvironment
        public static IWebHostEnvironment WebHostEnvironment;

        // Test instance of a ModelStateDictionary
        public static ModelStateDictionary ModelState;

        // Test instance of ActionContext
        public static ActionContext ActionContext;

        // Acting as an empty model for testing purposes
        public static EmptyModelMetadataProvider ModelMetadataProvider;

        // Test ViewDataDictionary object
        public static ViewDataDictionary ViewData;

        // Test TempDataDictionary object
        public static TempDataDictionary TempData;

        // Test PageContext object
        public static PageContext PageContext;

        // Test instance of RestaurantService
        public static RestaurantService RestaurantServiceObject;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static TestHelper()
        {
            // Initialize and setup MockWebHost Environment object
            MockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            MockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            MockWebHostEnvironment.Setup(m => m.WebRootPath).Returns(TestFixture.DataWebRootPath);
            MockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(TestFixture.DataContentRootPath);

            // Initialize and set TraceIdentifier property for HttpContextDefault
            HttpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
            };
            HttpContextDefault.HttpContext.TraceIdentifier = "trace";

            // Initialize ModelStateDictionary object
            ModelState = new ModelStateDictionary();

            // Initialize ActionContext
            ActionContext = new ActionContext(HttpContextDefault, HttpContextDefault.GetRouteData(), new PageActionDescriptor(), ModelState);

            // Initialize test Model and associated data
            ModelMetadataProvider = new EmptyModelMetadataProvider();
            ViewData = new ViewDataDictionary(ModelMetadataProvider, ModelState);
            TempData = new TempDataDictionary(HttpContextDefault, Mock.Of<ITempDataProvider>());


            // Initialize PageContext object and setup data
            PageContext = new PageContext(ActionContext)
            {
                ViewData = ViewData,
                HttpContext = HttpContextDefault
            };

            // Initialize test service with MockWebHostEnvironment
            RestaurantServiceObject = new RestaurantService(MockWebHostEnvironment.Object);

            // Declare RestaurantService instance
            RestaurantService restaurantService;

            // Initialize test restaurantService with TestHelper.MockWebHostEnvironment
            restaurantService = new RestaurantService(TestHelper.MockWebHostEnvironment.Object);
        }

        /// <summary>
        /// Initializes a new PageContext object with test data for use in unit tests.
        /// This method creates a new DefaultHttpContext object, initializes a ModelStateDictionary object,
        /// sets up an ActionContext with the default route data, and initializes ViewData and TempData
        /// dictionaries. It then creates a new PageContext object with the ActionContext, ViewData, and
        /// HttpContext properties set, and finaally returns the new object.
        /// </summary>
        /// <returns>A new PageContext object with test data.</returns>
        public static PageContext InitiatePageContext()
        {
            // Initialize and set TraceIdentifier propertiy for HttpContextDefault. 
            HttpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
            };
            HttpContextDefault.HttpContext.TraceIdentifier = "trace";

            // Initialize ModelStateDictionary object. 
            ModelState = new ModelStateDictionary();

            // Initialize ActionContext. 
            ActionContext = new ActionContext(HttpContextDefault, HttpContextDefault.GetRouteData(), new PageActionDescriptor(), ModelState);

            // Initialize test Model and associated test data. 
            ModelMetadataProvider = new EmptyModelMetadataProvider();
            ViewData = new ViewDataDictionary(ModelMetadataProvider, ModelState);
            TempData = new TempDataDictionary(HttpContextDefault, Mock.Of<ITempDataProvider>());

            // Initialize PageContext object and set ViewData and HttpContext properties. 
            return new PageContext(ActionContext)
            {
                ViewData = ViewData,
                HttpContext = HttpContextDefault
            };
        }
    }
}

