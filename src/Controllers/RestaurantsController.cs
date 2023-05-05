//This System.Collections.Generic contains generic collection classes and interfaces.
using System.Collections.Generic;
// Import the Microsoft.AspNetCore.Mvc namespace for creating web applications using the MVC pattern.
using Microsoft.AspNetCore.Mvc;
// Import the namespace for the models used by the RestaurantsController.
using ContosoCrafts.WebSite.Models;
// Import the namespace for the services used by the RestaurantsController.
using ContosoCrafts.WebSite.Services;
//Declare new namespace for the RestaurantsController..
namespace ContosoCrafts.WebSite.Controllers
{
    // Indicate that this controller should use the default behavior for API controllers.
    [ApiController]
    // Specify the route template for this controller
    [Route("[controller]")]
    // Declare the RestaurantController class, which is derived from ControllerBase.
    public class RestaurantsController : ControllerBase
    {
        // Define the constructor for the RestaurantsController.
        public RestaurantsController(RestaurantService restaurantService)
        {
            // Assign the restaurantService parameter to the RestaurantService property.
            RestaurantService = restaurantService;
        }
        // Declare a public property RestaurantService of type RestaurantService.
        public RestaurantService RestaurantService { get; }
        // Indicate that this method should handle HTTP GET requests.
        [HttpGet]
        // Define the Get method.
        public IEnumerable<RestaurantModel> Get()
        {
            // Call the GetRestaurants method of the RestaurantService and return the result.
            return RestaurantService.GetRestaurants();
        }
        // Indicate that this method should handle HTTP PATCH requests.
        [HttpPatch]
        // Define the Patch method and specify that the request body should be deserialized into a RatingRequest object.
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            // Call the AddRating method of the RestaurantService to add the rating to the restaurant.
            RestaurantService.AddRating(request.RestaurantId, request.Rating);
            // Return an HTTP 200 OK status code.
            return Ok();
        }
        // Define the RatingRequest class, which has two properties.
        public class RatingRequest
        {
            // Declare a public property RestaurantId of type string.
            public string RestaurantId { get; set; }

            // Declare a public property Rating of type int.
            public int Rating { get; set; }
        }
    }
}