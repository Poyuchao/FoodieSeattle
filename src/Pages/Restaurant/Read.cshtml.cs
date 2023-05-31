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
        /// Function calculates and returns the average rating of a restaurant
        /// </summary>
        /// <returns></returns>
        public double CalculateAverageRating()
        {
            //The way I thougt to avoid the rating array to be 0 otherwise when it is NULL, the website will display error message
            if (Restaurant.Ratings == null)
            {
                return 0.0;
            }

            int[] restaurantRatings = Restaurant.Ratings;

            if (restaurantRatings.Length == 0)
            {
                return 0.0;
            }

            return restaurantRatings.Average();
        }

        /// <summary>
        /// Function returns the actual vote count
        /// </summary>
        /// <returns></returns>
        public double getVoteNumber()
        {
            if (Restaurant.Ratings == null)
            {
                return 0.0;
            }

            int[] restaurantRatings = Restaurant.Ratings;

            if (restaurantRatings.Length == 0)
            {
                return 0.0;
            }

            return restaurantRatings.Count();
        }

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
        public IActionResult OnPostUnlock()

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

        /// <summary>
        /// REST Post request for comments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns>Page</returns>
        public IActionResult OnPostAddComment(string id, string comment)
        {
            Restaurant = restaurantService.GetRestaurantById(id);

            if (!string.IsNullOrEmpty(comment))
            {
                // Call for the Restaurant comment to be saved
                restaurantService.AddComment(id, comment);
            }

            return RedirectToPage("Read", new { id = id });
        }
    }
}
