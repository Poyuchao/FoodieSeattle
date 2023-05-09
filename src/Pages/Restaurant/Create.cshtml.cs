using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;

namespace FoodieSeattle.WebSite.Pages.Restaurant
{
    /// <summary>
    /// Create Page Model for the Create Razor Page: adds a new Restaurant to RestaurantModel and JSON file.
    /// </summary>
    public class CreateModel : PageModel
    {
        // Data middle tier.
        public RestaurantService restaurantService { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="restaurantService">An instance of the data service to use</param>
        public CreateModel(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // The data to show
        public RestaurantModel restaurant;

        /// <summary>
        /// REST Post request: Creates a permanent Restaurant object with user input data. 
        /// </summary>
        /// <returns>Redirect to index page</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Restaurant/Index");
            }

            // Get user input from the form: name, image link, short description, uploaded files.
            var name = Request.Form["Restaurant.Title"];
            var neighborhood = Request.Form["Restaurant.Neighborhood"];
            var cuisineType = Request.Form["Restaurant.Type"];
            var description = Request.Form["Restaurant.Description"];
            var url = Request.Form["Restaurant.Url"];
            var imageURL = Request.Form["Restaurant.Image"];


            // Create a new Restaurant Model object WITH user input
            restaurant = restaurantService.AddData(name, neighborhood, cuisineType, description, url, imageURL);

            // Redirect to Index page with reference to the new restaurant
            return RedirectToPage("/Restaurant/Index");
        }
    }
}
