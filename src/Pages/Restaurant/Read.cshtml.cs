
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
    //This line defines a new class named ReadModel that inherits from the PageModel class.
    public class ReadModel : PageModel
    {
        //This line creates a public property named ProductService of type JsonFileProductService. This property will be used to retrieve the products to display.
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        /// //This is the constructor for the ReadModel class. It takes an instance of JsonFileProductService as a parameter and assigns it to the ProductService property.
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show
        //This is a public property of type ProductModel named Product. This property will hold the data to display on the page.
        public ProductModel Product;

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>



        //This is the OnGet method for the ReadModel class. It takes an id parameter and uses it to retrieve a single product from the JsonFileProductService.
        //It then assigns the product to the Product property for display on the page.
        //This method uses LINQ to filter the list of products to find the one with the matching id
        public void OnGet(string id) => Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
    }
}
