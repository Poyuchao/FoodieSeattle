using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly JsonFileProductService _productService;

        public DeleteModel(JsonFileProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductModel Product { get; set; }

        public void OnGet(string id)
        {
            Product = _productService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.DeleteData(Product.Id);

            return RedirectToPage("/Product/Index");
        }
    }
}
