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
        //
        public RestaurantService restaurantService { get; }

        // This will hold the selected Neighborhood type
        public string SelectedNeighborhood { get; set; }

        // This will hold the restaurants of the selected Neighborhood type
        public IEnumerable<RestaurantModel> Restaurants { get; set; } 

        // logger for NeighborhoodsModel
        private ILogger<NeighborhoodModel> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restaurantService"></param>
        public NeighborhoodModel(ILogger<NeighborhoodModel> logger, RestaurantService restaurantService)
        {
            _logger = logger;
            this.restaurantService = restaurantService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Neighborhood"></param>
        public void OnGet(string Neighborhood)
        {
            SelectedNeighborhood = Neighborhood;

            // Fetch the restaurants of the selected Neighborhood type
            Restaurants = restaurantService.GetRestaurants().Where(r => r.Neighborhood == Neighborhood).ToList();
        }
    }
}
