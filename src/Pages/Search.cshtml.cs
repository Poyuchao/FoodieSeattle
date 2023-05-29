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
    /// SearchModel which redirects to the Search page after searching for restaurant,
    /// neighborhood, or cuisine type
    /// </summary>
    public class SearchModel : PageModel
    {

        // Variable for restaurantService
        private readonly RestaurantService restaurantService;

        /// <summary>
        /// Constructor for SearchModel class, takes a RestaurantService
        /// as a parameter
        /// </summary>
        /// <param name="restaurantService"></param>
        public SearchModel(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // This attribute is applied to the property to enable model binding.
        // By setting "SupportsGet" to true, the property can also be populated from the query string 
        // during HTTP GET requests, in addition to the default behavior of populating it during HTTP POST requests.
        [BindProperty(SupportsGet = true)]

        /// <summary>
        /// Property representing search term or condition
        /// </summary>	
        public string Query { get; set; }

        /// <summary>
        // The Results property represents a collection of RestaurantModel objects.
        // It can be retrieved publicly (get), but can only be modified within this class
        // (set is private).
        /// </summary>
        public IEnumerable<RestaurantModel> Results { get; private set; }

        /// <summary>
        /// REST Get request for restaurants; can search restaurant name, neighborhood,
        /// or cuisine type
        /// </summary>
        public void OnGet()
        {
            var restaurants = restaurantService.GetRestaurants();

            Results = (from restaurant in restaurants
                       where restaurant.Title.Contains(Query, StringComparison.OrdinalIgnoreCase)
                           || restaurant.Neighborhood.Contains(Query, StringComparison.OrdinalIgnoreCase)
                           || restaurant.Type.Contains(Query, StringComparison.OrdinalIgnoreCase)
                       orderby restaurant.Title
                       select restaurant).Distinct().ToList();
        }
    }
}