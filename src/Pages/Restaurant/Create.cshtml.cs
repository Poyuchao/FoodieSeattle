using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Restaurant
{
    /// <summary>
    /// Create a Page Model for the 'Create' CRUDi Razor Page. Should add a new restaurant
    /// and its respective attributes ProductModel and JSON file. 
    /// </summary>
    public class CreateModel : PageModel
    {
        // Data middle tier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService">An instande of the productService to use</param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show
        public ProductModel Product;

        /// <summary>
        /// REST Post request: to create a permanent restaurant object with user input data
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet()
        {
            Product = ProductService.CreateData();

            return RedirectToPage("/Restaurant/Update", new { Id = Product.Id });
        }
    }
}
