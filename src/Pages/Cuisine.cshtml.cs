﻿using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FoodieSeattle.WebSite.Services;
using FoodieSeattle.WebSite.Models;

namespace FoodieSeattle.WebSite.Pages
{
    public class CuisineModel : PageModel
    {
        // Get the RestaurantService
        public RestaurantService restaurantService { get; }

        // This will hold the selected cuisine type
        public string SelectedCuisine { get; set; } 

        // This will hold the restaurants of the selected cuisine type
        public IEnumerable<RestaurantModel> Restaurants { get; set; } 

        // logger for CuisinesModel
        private ILogger<CuisineModel> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restaurantService"></param>
        public CuisineModel(ILogger<CuisineModel> logger, RestaurantService restaurantService)
        {
            _logger = logger;
            this.restaurantService = restaurantService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuisine"></param>
        public void OnGet(string cuisine)
        {
            // The specific cuisine we want to filter
            SelectedCuisine = cuisine;

            // Fetch the restaurants of the selected cuisine type
            Restaurants = restaurantService.GetRestaurants().Where(r => r.Type == cuisine).ToList();
        }
    }
}