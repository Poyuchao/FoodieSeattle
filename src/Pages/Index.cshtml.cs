using System.Collections.Generic;

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

        //This is the constructor for the IndexModel class. It takes an instance of ILogger<IndexModel> and JsonFileProductService as parameters.
        //It assigns the ILogger<IndexModel> to the _logger field,
        //and assigns the JsonFileProductService to the ProductService property.
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }
        //These are public properties named ProductService and Products.
        //ProductService is of type JsonFileProductService, and Products is of type IEnumerable<ProductModel>.
        //Products will hold the list of products to display on the page.
        public JsonFileProductService ProductService { get; }
        public IEnumerable<ProductModel> Products { get; private set; }
        //This is the OnGet method for the IndexModel class.
        //It retrieves the list of products using the ProductService property and assigns them to the
        //Products property for display on the page.
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}