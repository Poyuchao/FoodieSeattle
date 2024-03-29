// Import the System.Text.Json namespace for working with JSON data.
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
// Import the System.Text.Json.Serialization namespace for working with JSON serialization attribute.
using System.Text.Json.Serialization;

namespace FoodieSeattle.WebSite.Models
{
    public class RestaurantModel
    {
        // Declare a public property Id of type string to represent the ID of the restaurant.
        public string Id { get; set; }

        // Use the JsonPropertyName attribute to map the Image property to the "img" field in the JSON data.
        // Declare and validate a public property Image of type string to represent the image URL of the restaurant.
        [JsonPropertyName("img")]
        [Required(ErrorMessage = "Please enter a valid URL starting with \"https://\".")]
        [RegularExpression(@"(https?:\/\/.*)", ErrorMessage = "Please enter a url starting with \"https://\". ")]
        public string Image { get; set; } = "Default";

        // Declare and validate a public property Url of type string to represent the URL of the restaurant.
        [Required(ErrorMessage = "Please enter a valid URL starting with \"https://\".")]
        [RegularExpression(@"(https?:\/\/.*)", ErrorMessage = "Please enter a url starting with \"https://\". ")]
        public string Url { get; set; }

        // Declare and validate a public property Title of type string to represent the title of the restaurant.
        [Required(ErrorMessage = "Please enter a restaurant name.")]
        [RegularExpression(@"^[a-z A-Z\s']+$", ErrorMessage = "Please enter a valid restaurant name.")]
        [StringLength(50, ErrorMessage = "A restaurant name cannot exceed 50 characters. ")]
        public string Title { get; set; }

        // Declare and validate a public property Neighborhood of type string to represent the neighborhood of the restaurant.
        [Required(ErrorMessage = "Please enter the restaurant's neighborhood.")]
        [RegularExpression(@"^[a-z A-Z\s']+$", ErrorMessage = "Please enter a valid neighborhood.")]
        [StringLength(30, ErrorMessage = "A neighborhood cannot exceed 30 characters. ")]
        public string Neighborhood { get; set; }

        // Validating City name to allow only alpha characters
        [Required(ErrorMessage = "Please enter a City name.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Please enter a valid City name.")]
        public string City { get; set; } = "Seattle";

        // Validating State name to allow only alpha characters
        [Required(ErrorMessage = "Please enter a State name.")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Please enter a valid State name.")]
        public string State { get; set; } = "WA";

        // Address of restaurant
        [Required(ErrorMessage = "Please enter a valid address")]
        public string Address { get; set; } = "Default";

        // Declare and validate a public property Type of type string to represent the cuisine type of a restaurant
        [Required(ErrorMessage = "Please enter a cuisine type.")]
        [RegularExpression(@"^[a-z A-Z\s']+$", ErrorMessage = "Please enter a valid cuisine type.")]
        [StringLength(20, ErrorMessage = "A cuisine type cannot exceed 20 characters. ")]
        public string Type { get; set; }

        // Declare and validate a public property Description of type string to represent the description of the restaurant.
        [Required(ErrorMessage = "Please enter a brief description.")]
        [StringLength(2000, ErrorMessage = "A Neighborhood description cannot exceed 2000 characters. ")]
        public string Description { get; set; }

        // Declare a public property Ratings of type int[] to represent the ratings of the restaurant.
        public int[] Ratings { get; set; }

        // Override the ToString method to serialize the restaurant to a JSON string using the JsonSerializer.
        public override string ToString() => JsonSerializer.Serialize<RestaurantModel>(this);

        // Store the Comments entered by the users on this restaurant
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}