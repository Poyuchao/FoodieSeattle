//// OLD CODE - Delete after 'Create' and 'Update' are confirmed to be operating properly

//// Import the System.Text.Json namespace for working with JSON data.
//using System.Text.Json;
//// Import the System.Text.Json.Serialization namespace for working with JSON serialization attribute.
//using System.Text.Json.Serialization;

//namespace ContosoCrafts.WebSite.Models
//{
//    public class RestaurantModel
//    {
//        // Declare a public property Id of type string to represent the ID of the restaurant.
//        public string Id { get; set; }
//        // Declare a public property Maker of type string to represent the maker of the restaurant.
//        public string Maker { get; set; }
//        // Use the JsonPropertyName attribute to map the Image property to the "img" field in the JSON data.
//        [JsonPropertyName("img")]
//        // Declare a public property Image of type string to represent the image URL of the restaurant.   
//        public string Image { get; set; }
//        // Declare a public property Url of type string to represent the URL of the restaurant.        
//        public string Url { get; set; }
//        // Declare a public property Title of type string to represent the title of the restaurant.
//        public string Title { get; set; }
//        // Declare a public property Description of type string to represent the description of the restaurant.
//        public string Description { get; set; }
//        // Declare a public property Ratings of type int[] to represent the ratings of the restaurant.
//        public int[] Ratings { get; set; }
//        // Override the ToString method to serialize the restaurant to a JSON string using the JsonSerializer.
//        public override string ToString() => JsonSerializer.Serialize<RestaurantModel>(this);

//        public string Type { get; set; } // Add this property to the class

//    }
//}



// NEW CODE

// Import the System.Text.Json namespace for working with JSON data.
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
// Import the System.Text.Json.Serialization namespace for working with JSON serialization attribute.
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class RestaurantModel
    {
        // Declare a public property Id of type string to represent the ID of the restaurant.
        public string Id { get; set; }
        // Declare a public property Maker of type string to represent the maker of the restaurant.
        public string Maker { get; set; }
        // Use the JsonPropertyName attribute to map the Image property to the "img" field in the JSON data.
        [JsonPropertyName("img")]

        // Declare and validate a public property Image of type string to represent the image URL of the restaurant.
        [RegularExpression(@"(https?:\/\/.*)", ErrorMessage = "Please enter a url starting with \"https://\". ")]
        public string Image { get; set; } = "Default";

        // Declare and validate a public property Url of type string to represent the URL of the restaurant.
        [RegularExpression(@"(https?:\/\/.*)", ErrorMessage = "Please enter a url starting with \"https://\". ")]
        public string Url { get; set; }

        // Declare and validate a public property Title of type string to represent the title of the restaurant.
        [Required(ErrorMessage = "Please enter a restaurant name.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Please enter a valid restaurant name.")]
        [StringLength(50, ErrorMessage = "A restaurant name cannot exceed 50 characters. ")]
        public string Title { get; set; }

        // Declare and validate a public property Description of type string to represent the description of the restaurant.
        [Required(ErrorMessage = "Please enter a brief description.")]
        [StringLength(500, ErrorMessage = "A Neighborhood description cannot exceed 500 characters. ")]
        //public string Description { get; set; } = "Default";
        public string Description { get; set; }

        // Declare a public property Ratings of type int[] to represent the ratings of the restaurant.
        public int[] Ratings { get; set; }

        // Override the ToString method to serialize the restaurant to a JSON string using the JsonSerializer.
        public override string ToString() => JsonSerializer.Serialize<RestaurantModel>(this);

        // Declare and validate a public property Type of type string to represent the cuisine type of a restaurant
        [Required(ErrorMessage = "Please enter a cuisine type.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Please enter a valid cuisine type.")]
        [StringLength(20, ErrorMessage = "A cuisine type cannot exceed 20 characters. ")]
        public string Type { get; set; }

    }
}