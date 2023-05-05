
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
        public static RestaurantService _RestaurantService;

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
            _RestaurantService = new RestaurantService(MockWebHostEnvironment.Object);

            // Declare JsonFileProductService instance
            RestaurantService restaurantService;

            // Initialize test productService with TestHelper.MockWebHostEnvironment
            restaurantService = new RestaurantService(TestHelper.MockWebHostEnvironment.Object);
        }
    }
}

