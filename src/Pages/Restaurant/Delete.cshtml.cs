using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System;
using System.Collections.Generic;


namespace ContosoCrafts.WebSite.Pages.Restaurant
{/// <summary>
 /// Manage the Delete of the data for a single record
 /// </summary>
    public class DeleteModel : PageModel
    {
        // Data middletier
        /// <summary>
        /// The "DeleteModel" class is defined, which inherits from the "PageModel" class provided by the Razor Pages framework in ASP.NET Core.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// The "OnGet" method is defined, which takes a string parameter called "id".
        /// This method is called when the page is loaded with a GET request.
        /// The method calls the "GetProducts" method on the "ProductService" property,
        /// which returns a list of "ProductModel" objects.
        /// The "FirstOrDefault" LINQ method is called on the list to find the first object whose "Id" property equals the "id" parameter.
        /// If a matching object is found, it is assigned to the "Product" property.
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }

        /// <summary>
        /// The "OnPost" method is defined, which is called when the form on the page is submitted with a POST request. The method first checks if the model state is valid.
        /// If it is not, the method returns the current page to show validation errors.
        /// If the model state is valid, the "DeleteData" method on the "ProductService" property is called with the "Id" property of the "Product" object as the parameter.
        /// This deletes the data for the specified record.
        /// Finally, the method returns a redirect to the "RestaurantIndex" page of the restaurant data.
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.DeleteData(Product.Id);

            return RedirectToPage("/Restaurant/Index");
        }
    }
}
