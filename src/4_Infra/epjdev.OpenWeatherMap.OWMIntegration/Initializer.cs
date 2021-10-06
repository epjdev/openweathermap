using epjdev.OpenWeatherMap.OWMIntegration.Weather;
using Microsoft.Extensions.DependencyInjection;

namespace epjdev.OpenWeatherMap.OWMIntegration
{
    public static class Initializer
    {
        public static void StartOMWIntegration(this IServiceCollection services)
        {
            services.AddSingleton<IOWMCurrentWeather, OMWCurrentWeatherImpl>();
        }
    }
}
