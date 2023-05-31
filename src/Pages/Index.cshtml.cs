using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;

namespace FoodieSeattle.WebSite.Pages
{
    //This line defines a new class named IndexModel that inherits from the PageModel class.
    public class IndexModel : PageModel
    {
        //This line creates a private readonly field named _logger of type ILogger<IndexModel>. This field will be used for logging.
        private readonly ILogger<IndexModel> _logger; 

        //This is the constructor for the IndexModel class. It takes an instance of ILogger<IndexModel> and RestaurantService as parameters.
        //It assigns the ILogger<IndexModel> to the _logger field,
        //and assigns the RestaurantService to the RestaurantService property.
        public IndexModel(ILogger<IndexModel> logger,
            RestaurantService restaurantService)
        {
            _logger = logger;
            _RestaurantService = restaurantService;
        }

        // Data middle tier
        public RestaurantService _RestaurantService { get; }

        // The list of restaurants to display
        public IEnumerable<RestaurantModel> Restaurants { get; private set; }

        /// <summary>
        /// OnGet retrieves the list of Restaurants using the RestaurantService property
        /// and assigns them to the Restaurants property for display on the page.
        /// </summary>
        public void OnGet()
        {
            Restaurants = _RestaurantService.GetRestaurants();
        }
    }
}