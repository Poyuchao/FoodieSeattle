using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;


namespace ContosoCrafts.WebSite.Pages.Restaurant
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>

    public class UpdateModel : PageModel
    {
        // Data middletier
        public RestaurantService _RestaurantService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restaurantService"></param>
        public UpdateModel(RestaurantService restaurantService)
        {
            _RestaurantService = restaurantService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public RestaurantModel Restaurant { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Restaurant = _RestaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(id));
        }

        /// <summary>
        /// Handles the onPost request to update a RestauranttModel object.
        /// If the ModelState is invalid, the method returns the current page.
        /// Otherwise, the method updates the data for the RestaurantModel object
        /// using the RestaurantService, and redirects the user to the Restaurant/Index page.
        /// </summary>
        /// <returns>redirect to Index page</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _RestaurantService.UpdateData(Restaurant);

            return RedirectToPage("/Restaurant/Index");
        }
    }
}
