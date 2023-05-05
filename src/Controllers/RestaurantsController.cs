//This System.Collections.Generic contains generic collection classes and interfaces.
using System.Collections.Generic;
// Import the Microsoft.AspNetCore.Mvc namespace for creating web applications using the MVC pattern.
using Microsoft.AspNetCore.Mvc;
// Import the namespace for the models used by the ProductsController.
using ContosoCrafts.WebSite.Models;
// Import the namespace for the services used by the ProductsController.
using ContosoCrafts.WebSite.Services;
//Declare new namespace for the ProductsController..
namespace ContosoCrafts.WebSite.Controllers
{
    // Indicate that this controller should use the default behavior for API controllers.
    [ApiController]
    // Specify the route template for this controller
    [Route("[controller]")]
    // Declare the ProductsController class, which is derived from ControllerBase.
    public class ProductsController : ControllerBase
    {
        // Define the constructor for the ProductsController.
        public ProductsController(JsonFileProductService productService)
        {
            // Assign the productService parameter to the ProductService property.
            ProductService = productService;
        }
        // Declare a public property ProductService of type JsonFileProductService.
        public JsonFileProductService ProductService { get; }
        // Indicate that this method should handle HTTP GET requests.
        [HttpGet]
        // Define the Get method.
        public IEnumerable<ProductModel> Get()
        {
            // Call the GetProducts method of the ProductService and return the result.
            return ProductService.GetProducts();
        }
        // Indicate that this method should handle HTTP PATCH requests.
        [HttpPatch]
        // Define the Patch method and specify that the request body should be deserialized into a RatingRequest object.
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            // Call the AddRating method of the ProductService to add the rating to the product.
            ProductService.AddRating(request.ProductId, request.Rating);
            // Return an HTTP 200 OK status code.
            return Ok();
        }
        // Define the RatingRequest class, which has two properties.
        public class RatingRequest
        {
            // Declare a public property ProductId of type string.
            public string ProductId { get; set; }

            // Declare a public property Rating of type int.
            public int Rating { get; set; }
        }
    }
}