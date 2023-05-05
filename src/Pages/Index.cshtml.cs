﻿using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    //This line defines a new class named IndexModel that inherits from the PageModel class.
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; //This line creates a private readonly field named _logger of type ILogger<IndexModel>. This field will be used for logging.

        //This is the constructor for the IndexModel class. It takes an instance of ILogger<IndexModel> and RestaurantService as parameters.
        //It assigns the ILogger<IndexModel> to the _logger field,
        //and assigns the RestaurantService to the RestaurantService property.
        public IndexModel(ILogger<IndexModel> logger,
            RestaurantService restaurantService)
        {
            _logger = logger;
            RestaurantService = restaurantService;
        }
        //These are public properties named RestaurantService and Restaurants.
        //RestaurantService is of type RestaurantService, and Restaurants is of type IEnumerable<RestaurantModel>.
        //Restaurants will hold the list of Restaurants to display on the page.
        public RestaurantService RestaurantService { get; }
        public IEnumerable<RestaurantModel> Restaurants { get; private set; }
        //This is the OnGet method for the IndexModel class.
        //It retrieves the list of Restaurants using the RestaurantService property and assigns them to the
        //Restaurants property for display on the page.
        public void OnGet()
        {
            Restaurants = RestaurantService.GetRestaurants();
        }
    }
}