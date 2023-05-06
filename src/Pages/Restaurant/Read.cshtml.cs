
//These lines import the necessary namespaces for this class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Restaurant
{
    /// <summary>
    /// Read Page Model for the Read.cshtml Page, should return a restaurant's data to display
    /// </summary>
    public class ReadModel : PageModel
    {
        // Data middle tier.
        public RestaurantService _RestaurantService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="restaurantService">Instance of the data service we will use</param>
        public ReadModel(RestaurantService restaurantService)
        {
            _RestaurantService = restaurantService;
        }

        //This is a public property of type RestaurantModel named Restaurant. This property will hold the data to display on the page.
        public RestaurantModel Restaurant;
        //Add a public property for the password
        [BindProperty]
        public string Password { get; set; }

        // Define the variable to keep track of password status
        public bool PasswordEntered { get; set; }
        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>

        /// <summary>
        /// REST Get request.
        /// </summary>
        /// <param name="id">The unique id of the restaurant to show</param>
        public void OnGet(string id) => Restaurant = _RestaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(id));

        public IActionResult OnPost()
        {
            if (Password == "6666")
            {
                PasswordEntered = true;
                //retrieves the ProductModel object whose Id property matches the value of the "id" route parameter in the URL.
                Restaurant = _RestaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(RouteData.Values["id"]));
                return Page();
            }
            else
            {
                ModelState.AddModelError("Password", "Incorrect password.");
                Restaurant = _RestaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(RouteData.Values["id"]));
                return Page();
            }
        }
    }
}
