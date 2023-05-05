using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Restaurant
{
    /// <summary>
    /// Create a Page Model for the 'Create' CRUDi Razor Page. Should add a new restaurant
    /// and its respective attributes RestaurantModel and JSON file. 
    /// </summary>
    public class CreateModel : PageModel
    {
        // Data middle tier
        public RestaurantService _RestaurantService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restaurantService">An instande of the restaurantService to use</param>
        public CreateModel(RestaurantService restaurantService)
        {
            _RestaurantService = restaurantService;
        }

        // The data to show
        public RestaurantModel Restaurant;

        /// <summary>
        /// REST Post request: to create a permanent restaurant object with user input data
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet()
        {
            Restaurant = _RestaurantService.CreateData();

            return RedirectToPage("/Restaurant/Update", new { Id = Restaurant.Id });
        }
    }
}
