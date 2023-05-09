using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;


namespace FoodieSeattle.WebSite.Pages.Restaurant
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>

    public class UpdateModel : PageModel
    {
        // Data middletier
        public RestaurantService restaurantService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restaurantService"></param>
        public UpdateModel(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public RestaurantModel Restaurant { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>


        public IActionResult OnGet(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Restaurant/Index");
            }

            //restaurant = restaurantService.GetRestaurantById(id);
            Restaurant = restaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(id));

            if (Restaurant == null)
            {
                return RedirectToPage("/Restaurant/Index");
            }

            return Page();
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
                return RedirectToPage("/Restaurant/Index");
            }


            // If restaurant is not null, update restaurant with user entered data. 
            if (Restaurant != null)
            {
                Restaurant = restaurantService.UpdateData(Restaurant);
            }

            return RedirectToPage("/Restaurant/Index");
        }
    }
}