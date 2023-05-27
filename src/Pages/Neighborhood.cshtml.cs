using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FoodieSeattle.WebSite.Services;
using FoodieSeattle.WebSite.Models;

namespace FoodieSeattle.WebSite.Pages
{
	public class NeighborhoodModel : PageModel
    {
        // Gets the RestaurantService
        public RestaurantService restaurantService { get; }

        // This will hold the selected Neighborhood type
        public string SelectedNeighborhood { get; set; }

        // This will hold the restaurants of the selected Neighborhood type
        public IEnumerable<RestaurantModel> Restaurants { get; set; } 

        // logger for NeighborhoodsModel
        private ILogger<NeighborhoodModel> _logger;

        /// <summary>
        /// NeighborhoodModel Constructor. Will set _logger equal to passed in logger param,
        /// this restaurantService to restaurantService.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restaurantService"></param>
        public NeighborhoodModel(ILogger<NeighborhoodModel> logger, RestaurantService restaurantService)
        {
            _logger = logger;
            this.restaurantService = restaurantService;
        }

        /// <summary>
        /// OnGet function. Sets the SelectedNeighborhood, and returns a sorted, group
        /// of restaurants with no duplicate based on the SelectedNeighborhood.
        /// </summary>
        /// <param name="Neighborhood"></param>
        public void OnGet(string Neighborhood)
        {
            SelectedNeighborhood = Neighborhood;

            // Fetch the restaurants of the selected Neighborhood type
            Restaurants = restaurantService.GetRestaurants().Where(r => r.Neighborhood == Neighborhood).OrderBy(c => c.Title).ToList();
        }
    }
}
