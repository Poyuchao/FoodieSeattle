using FoodieSeattle.WebSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodieSeattle.WebSite
{
    //This is the constructor for the Startup class. It takes an IConfiguration instance as a parameter, which is used to configure the application.
    //It also defines a public property named Configuration of type IConfiguration, which will be used to access the configuration data.
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //This is the ConfigureServices method for the Startup class. It takes an IServiceCollection instance as
        //a parameter and is used to add services to the dependency injection container. In this method, the AddRazorPages, AddServerSideBlazor, AddHttpClient, AddControllers, a
        //nd AddTransient<RestaurantService> methods are called to register the services for the application.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddTransient<RestaurantService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //This is the Configure method for the Startup class. It takes an IApplicationBuilder instance and an IWebHostEnvironment instance as parameters,
        //which are used to configure the HTTP request pipeline. In this method, middleware is added to the pipeline to handle development or production exceptions,
        //HTTPS redirection, static files, routing, authorization, Razor pages, controllers, and Blazor.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

                // endpoints.MapGet("/restaurants", (context) => 
                // {
                //     var restaurants = app.ApplicationServices.GetService<RestaurantService>().GetRestaurants();
                //     var json = JsonSerializer.Serialize<IEnumerable<Restaurnat>>(restaurants);
                //     return context.Response.WriteAsync(json);
                // });
            });
        }
    }
}
