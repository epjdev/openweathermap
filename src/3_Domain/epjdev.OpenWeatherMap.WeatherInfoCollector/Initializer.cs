using Microsoft.Extensions.DependencyInjection;

namespace epjdev.OpenWeatherMap.WeatherInfoCollector
{
    public static class Initializer
    {
        public static void StartWeatherInfoCollector(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherInfoCollector, WeatherInfoCollectorImpl>();
        }
    }
}
