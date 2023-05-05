// Import the System.Text.Json namespace for working with JSON data.
using System.Text.Json;
// Import the System.Text.Json.Serialization namespace for working with JSON serialization attribute.
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        // Declare a public property Id of type string to represent the ID of the product.
        public string Id { get; set; }
        // Declare a public property Maker of type string to represent the maker of the product.
        public string Maker { get; set; }
        // Use the JsonPropertyName attribute to map the Image property to the "img" field in the JSON data.
        [JsonPropertyName("img")]
        // Declare a public property Image of type string to represent the image URL of the product.   
        public string Image { get; set; }
        // Declare a public property Url of type string to represent the URL of the product.        
        public string Url { get; set; }
        // Declare a public property Title of type string to represent the title of the product.
        public string Title { get; set; }
        // Declare a public property Description of type string to represent the description of the product.
        public string Description { get; set; }
        // Declare a public property Ratings of type int[] to represent the ratings of the product.
        public int[] Ratings { get; set; }
        // Override the ToString method to serialize the product to a JSON string using the JsonSerializer.
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}