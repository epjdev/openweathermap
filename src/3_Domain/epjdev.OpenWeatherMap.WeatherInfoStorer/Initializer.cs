using Microsoft.Extensions.DependencyInjection;

namespace epjdev.OpenWeatherMap.WeatherInfoStorer
{
    public static class Initializer
    {
        public static void StartWeatherInfoStorer(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherInfoStoreManager, WeatherInfoStoreManagerImpl>();
        }
    }
}
