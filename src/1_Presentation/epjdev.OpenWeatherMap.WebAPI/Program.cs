using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace epjdev.OpenWeatherMap.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).UseServiceProviderFactory(new Microsoft.Extensions.DependencyInjection.DefaultServiceProviderFactory()).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseIISIntegration();
                });
        }
    }
}
