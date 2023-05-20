//These lines import the necessary namespaces for this class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodieSeattle.WebSite.Pages.Restaurant
{
    /// <summary>
    /// Read Page Model for the Read.cshtml Page, should return a restaurant's data to display
    /// </summary>
    public class ReadModel : PageModel
    {
        // Data middle tier.
        public RestaurantService restaurantService { get; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="restaurantService">Instance of the data service we will use</param>
        public ReadModel(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // This is a public property of type RestaurantModel named Restaurant. This property will hold the data to display on the page.
        public RestaurantModel Restaurant;
        // Add a public property for the password
        [BindProperty]
        public string Password { get; set; }

        // Define the variable to keep track of password status
        public bool PasswordEntered { get; set; }

        // Flag for the view
        public bool IsPasswordInvalid { get; set; } = false;

        /// <summary>
        /// REST Get request.
        /// </summary>
        /// <param name="id">The unique id of the restaurant to show</param>
        public void OnGet(string id) => Restaurant = restaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(id));

        /// <summary>
        /// Processes the OnPost request for the ReadModel instance, setting the PasswordEntered
        /// flag and retrieving the corresponding RestaurantModel object if the correct password
        /// is provided. Or it adds a validation error to the ModelState if password is incorrect.
        /// </summary>
        /// <returns>An IActionResult representing the page result, either with the corresponding RestaurantModel object or with a validation error message.</returns>
        public IActionResult OnPost()
        {
            if (Password == "6666")
            {
                PasswordEntered = true;

                //retrieves the RestaurantModel object whose Id property matches the value of the "id" route parameter in the URL.
                Restaurant = restaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(RouteData.Values["id"]));

                return Page();
            }
            else
            {
                ModelState.AddModelError("Password", "Incorrect password.");
                Restaurant = restaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(RouteData.Values["id"]));
                IsPasswordInvalid = true;
                return Page();
            }
        }
    }
}
