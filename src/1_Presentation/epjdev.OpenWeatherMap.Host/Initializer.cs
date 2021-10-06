using epjdev.OpenWeatherMap.HostApplication;
using epjdev.OpenWeatherMap.OWMIntegration;
using epjdev.OpenWeatherMap.WeatherInfoCollector;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace epjdev.OpenWeatherMap.Host
{
    public class Initializer
    {
        public static IServiceProvider Start()
        {
            IServiceCollection services = new ServiceCollection();
            services.StartHostApplication();
            services.StartWeatherInfoCollector();
            services.StartOMWIntegration();

            //services.StartLogHelper();

            return services.BuildServiceProvider();
        }
    }
}
