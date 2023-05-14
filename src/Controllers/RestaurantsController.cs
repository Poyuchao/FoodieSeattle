//This System.Collections.Generic contains generic collection classes and interfaces.
using System.Collections.Generic;
// Import the Microsoft.AspNetCore.Mvc namespace for creating web applications using the MVC pattern.
using Microsoft.AspNetCore.Mvc;
// Import the namespace for the models used by the RestaurantsController.
using FoodieSeattle.WebSite.Models;
// Import the namespace for the services used by the RestaurantsController.
using FoodieSeattle.WebSite.Services;
//Declare new namespace for the RestaurantsController..
namespace FoodieSeattle.WebSite.Controllers
{
    /// <summary>
    /// RestaurantsController for Restaurants Pages and Model
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RestaurantsController : ControllerBase
    {
        // Define the constructor for the RestaurantsController.
        public RestaurantsController(RestaurantService restaurantService)
        {
            // Assign the restaurantService parameter to the RestaurantService property.
            this.restaurantService = restaurantService;
        }

        // Declare a public property RestaurantService of type RestaurantService.
        public RestaurantService restaurantService { get; }

        // Indicate that this method should handle HTTP GET request,
        // define the Get method.
        [HttpGet]
        public IEnumerable<RestaurantModel> Get()
        {
            // Call the GetRestaurants method of the RestaurantService and return the result.
            return restaurantService.GetRestaurants();
        }

        // Indicate that this method should handle HTTP PATCH requests,
        // define the Patch method and specify that the request body should be
        // deserialized into a RatingRequest object.
        [HttpPatch]       
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            // Call the AddRating method of the RestaurantService to add the rating to the restaurant.
            restaurantService.AddRating(request.RestaurantId, request.Rating);
            // Return an HTTP 200 OK status code.
            return Ok();
        }

        /// <summary>
        /// Represents a request object used for submitting ratings for a restaurant.
        /// It contains properties for the restaurant ID and the rating value.
        /// </summary>
        public class RatingRequest
        {
            // Declare a public property RestaurantId of type string.
            public string RestaurantId { get; set; }

            // Declare a public property Rating of type int.
            public int Rating { get; set; }
        }
    }
}