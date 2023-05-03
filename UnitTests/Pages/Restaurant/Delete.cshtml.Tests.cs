using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Reflection;
using System.Reflection.Metadata;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Manage the deletion of data for a single record
    /// </summary>
    public class DeleteModel : PageModel
    {
        // Data middletier
        private readonly JsonFileProductService _productService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public DeleteModel(JsonFileProductService productService)
        {
            _productService = productService;
        }

        // Data that needs to be shown, bind it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// REST GET request
        /// Loads the data
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Product = _productService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }

        /// <summary>
        /// Handles the POST request for deleting a product.
        /// @return IActionResult - a response to the client's request.
        /// If model state, invalid, function returns to current page.
        /// Otherwise, it calls ProductService to delete the data for the given product ID.
        /// Then, it redirects the client to the RestaurantIndex page.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.DeleteData(Product.Id);

            return RedirectToPage("/Restaurant/RestaurantIndex");
        }
    }
}
