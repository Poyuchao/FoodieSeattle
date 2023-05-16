using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using FoodieSeattle.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace FoodieSeattle.WebSite.Services
{
    /// <summary>
    /// Mediates communication between a RestaurantController and Restaurants Data.
    /// </summary>
    public class RestaurantService
    {
        /// <summary>
        /// Constructor to inject the hosting environment
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public RestaurantService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // Property to access the hosting environment
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Private method to return the full path of the Restaurants JSON file
        /// </summary>
        /// <returns>A string representing the full path of the JSON file.</returns>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "Restaurants.json"); }
        }


        /// <summary>
        /// Parses a JSON file containing an array of RestaurantModel objects and returns
        /// them as an IEnumerable.
        /// </summary>
        /// <param name="jsonFileReader"></param>
        /// <returns>An IEnumerable of RestaurantModel objects</returns>
        public IEnumerable<RestaurantModel> parseRestaurantResult(StreamReader jsonFileReader)
        {
            return JsonSerializer.Deserialize<RestaurantModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

        }

        /// <summary>
        /// Generate/retrieve a list of Restaurant objects from JSON file.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RestaurantModel> GetRestaurants()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return parseRestaurantResult(jsonFileReader);
            }

        }

        /// <summary>
        /// Returns null if passed invalid id.
        /// Returns a single restaurant corresponding to the id.
        /// </summary>
        /// <param name="id">id of the requested restaurant</param>
        /// <returns>RestaurantModel of the requested restaurant</returns>
        public RestaurantModel GetRestaurantById(string id)
        {
            try
            {
                var data = GetRestaurants().Where(x => x.Id == id);
                RestaurantModel singleRestaurant = data.ElementAt(0);
                return singleRestaurant;
            }
            catch (ArgumentOutOfRangeException)
            {
                // If the id passed is invalid, we return null.
                return null;
            }
        }

        /// <summary>
        /// Get restaurants types funciton
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RestaurantModel> GetRestaurantsByType(string type)
        {
            // Get all restaurants
            var restaurants = GetRestaurants();

            if (!string.IsNullOrEmpty(type))
            {
                restaurants = restaurants.Where(r => r.Type == type);  // Filter by type if type parameter is not null or empty
            }
            return restaurants;
        }

        /// <summary>
        /// Add rating to restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        public bool AddRating(string RestaurantId, int rating)
        {
            if (string.IsNullOrEmpty(RestaurantId))
            {
                return false;
            }

            // List of all Restaurants from the database
            var Restaurants = GetRestaurants();

            var data = Restaurants.FirstOrDefault(x => x.Id.Equals(RestaurantId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(Restaurants);

            return true;
        }


        /// <summary>
        /// Finds restaurant in RestaurantModel, updates the Restaurant with user entered data,
        /// and saves to the data store.
        /// </summary>
        /// <param name="data"></param>
        public RestaurantModel UpdateData(RestaurantModel data)
        {
            // Create new Restaurant model
            var Restaurants = GetRestaurants();
            var RestaurantData = Restaurants.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (RestaurantData == null)
            {
                return null;
            }

            // Populate RestaurantData attributes
            RestaurantData.Title = data.Title;
            RestaurantData.Description = data.Description;
            RestaurantData.Url = data.Url;
            RestaurantData.Image = data.Image;
            RestaurantData.Type = data.Type;
            RestaurantData.Ratings = data.Ratings;

            // Populate restaurant comments
            RestaurantData.Comments = data.Comments;

            SaveData(Restaurants);

            return RestaurantData;
        }


        /// <summary>
        /// Save All Restaurant data to storage
        /// </summary>
        private void SaveData(IEnumerable<RestaurantModel> Restaurants)
        {

            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<RestaurantModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    Restaurants
                );
            }
        }

        /// <summary>
        /// Create a new Restaurant using default values. After creation, the user can
        /// update to set value
        /// </summary>
        /// <returns></returns>
        public RestaurantModel CreateData()
        {
            var data = new RestaurantModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter URL",
                Image = "",
            };

            // Get the current set, and append the new record to it
            var dataSet = GetRestaurants();
            dataSet = dataSet.Append(data);

            SaveData(dataSet);

            return data;
        }


        /// <summary>
        /// Removes the item from the system
        /// </summary>
        /// <returns></returns>
        public RestaurantModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetRestaurants();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = GetRestaurants().Where(m => m.Id.Equals(id) == false);

            SaveData(newDataSet);

            return data;
        }

        /// <summary>
        /// Create a new restaurant object, add user input data to it, and save object in JSON file.
        /// </summary>
        /// <param name="name">name data entered by user</param>
        /// <param name="image">image URL entered by user</param>
        /// <param name="url">restaurant home website URL entered by user</param>
        /// <param name="desc">short description entered by user</param>
        /// <returns>A new RestaurantModel object to be later saved in JSON</returns>
        public RestaurantModel AddData(string name, string neighborhood, string cuisineType,
            string desc, string url, string image)
        {
            // Create a new Restaurant model
            var data = new RestaurantModel()
            {
                // Add user input data to the corresponding field
                Id = name + "-pic",
                Title = name,
                Neighborhood = neighborhood,
                Type = cuisineType,
                Description = desc,
                Url = url,
                Image = image
            };

            // Get the current set, and append the new record to it 
            var dataset = GetRestaurants();
            var newdataset = dataset.Append(data);

            // Save data set in JSON
            SaveData(newdataset);

            return data;
        }

    }

}