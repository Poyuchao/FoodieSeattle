using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Restaurant
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="restaurantService"></param>
        public IndexModel(RestaurantService restaurantService)
        {
            _RestaurantService = restaurantService;
        }

        // Data Service
        public RestaurantService _RestaurantService { get; }

        // Collection of the Data
        public IEnumerable<RestaurantModel> Restaurants { get; private set; }

        /// <summary>
        /// REST OnGet
        /// Return all the data
        /// </summary>
        public void OnGet()
        {
            Restaurants = _RestaurantService.GetRestaurants();
        }
    }
}