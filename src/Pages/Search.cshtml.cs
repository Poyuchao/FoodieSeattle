using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;

namespace FoodieSeattle.WebSite.Pages
{

    /// <summary>
    /// SearchModel which redirects to the Search page after searching for restaurant
    /// </summary>
    public class SearchModel : PageModel
    {

        // Variable for restaurantService
        private readonly RestaurantService restaurantService;

        /// <summary>
        /// Constructor for SearchModel class.
        /// </summary>
        public SearchModel(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // Data to display
        [BindProperty(SupportsGet = true)]

        /// <summary>
        /// Query to search for
        /// </summary>	
        public string Query { get; set; }

        /// <summary>
        /// The results of the search
        /// </summary>
        public IEnumerable<RestaurantModel> Results { get; private set; }

        /// <summary>
        /// REST Get request for restaurants; can search restaurant name, neighborhood,
        /// or cuisine type
        /// </summary>
        public void OnGet()
        {
            var restaurants = restaurantService.GetRestaurants();

            Results = restaurants.Where(x => x.Title.Contains(Query, StringComparison.OrdinalIgnoreCase)
                || x.Neighborhood.Contains(Query, StringComparison.OrdinalIgnoreCase)
                || x.Type.Contains(Query, StringComparison.OrdinalIgnoreCase))
            .Distinct().ToList();
        }
    }
}