using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;

namespace FoodieSeattle.WebSite.Pages.Restaurant
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="restaurantService"></param>
        public IndexModel(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // Data Service
        public RestaurantService restaurantService { get; }

        // Collection of the Data
        public IEnumerable<RestaurantModel> Restaurants { get; private set; }

        /// <summary>
        /// REST OnGet
        /// Return all the data
        /// </summary>
        public void OnGet()
        {
            Restaurants = restaurantService.GetRestaurants();
        }
    }
}