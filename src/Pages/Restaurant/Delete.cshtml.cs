using FoodieSeattle.WebSite.Models;
using FoodieSeattle.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System;
using System.Collections.Generic;


namespace FoodieSeattle.WebSite.Pages.Restaurant
{/// <summary>
 /// Manage the Delete of the data for a single record
 /// </summary>
    public class DeleteModel : PageModel
    {
        // Data middletier
        /// <summary>
        /// The "DeleteModel" class is defined, which inherits from the "PageModel" class provided by the Razor Pages framework in ASP.NET Core.
        /// </summary>
        public RestaurantService restaurantService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restaurantService"></param>
        public DeleteModel(RestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public RestaurantModel Restaurant { get; set; }

        /// <summary>
        /// The "OnGet" method is defined, which takes a string parameter called "id".
        /// This method is called when the page is loaded with a GET request.
        /// The method calls the "GetRestaurants" method on the "restaurantService" property,
        /// which returns a list of "RestaurantModel" objects.
        /// The "FirstOrDefault" LINQ method is called on the list to find the first object whose "Id" property equals the "id" parameter.
        /// If a matching object is found, it is assigned to the "Restaurant" property.
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Restaurant/Index");
            }

            Restaurant = restaurantService.GetRestaurants().FirstOrDefault(m => m.Id.Equals(id));

            if (Restaurant == null)
            {
                return RedirectToPage("/Restaurant/Index");
            }

            return Page();
        }

        /// <summary>
        /// The "OnPost" method is defined, which is called when the form on the page is submitted with a POST request. The method first checks if the model state is valid.
        /// If it is not, the method returns the current page to show validation errors.
        /// If the model state is valid, the "DeleteData" method on the "restaurantService" property is called with the "Id" property of the "Restaurant" object as the parameter.
        /// This deletes the data for the specified record.
        /// Finally, the method returns a redirect to the Restaurant/Index page of the restaurant data.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Delete all JSON data and image files associated with this restaurant.
            if (Restaurant != null)
            {
                restaurantService.DeleteData(Restaurant.Id);
            }

            return RedirectToPage("/Restaurant/Index");
        }
    }
}
