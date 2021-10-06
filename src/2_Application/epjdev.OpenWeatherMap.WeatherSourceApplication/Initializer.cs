using Microsoft.Extensions.DependencyInjection;

namespace epjdev.OpenWeatherMap.WeatherSourceApplication
{
    public static class Initializer
    {
        public static void StartWeatherSourceApplication(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherSourceApplication, WeatherSourceApplicationImpl>();
        }
    }
}
