using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    public class Program
    {
        public static void Main(string[] args) //This is the Main method for the Program class. It takes an array of strings named args as a parameter.
                                               //It creates an instance of a host using the CreateHostBuilder method, builds the host, and runs it.
        {
            CreateHostBuilder(args).Build().Run();
        }
        //This is the CreateHostBuilder method for the Program class. It takes an array of strings named args as a parameter. It creates a default host builder using Host.CreateDefaultBuilder,
        //and then configures the web host using webBuilder.UseStartup<Startup>(),
        //which sets the startup class for the application to Startup.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
