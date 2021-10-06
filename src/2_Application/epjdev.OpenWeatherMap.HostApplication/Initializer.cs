using Microsoft.Extensions.DependencyInjection;

namespace epjdev.OpenWeatherMap.HostApplication
{
    public static class Initializer
    {
        public static void StartHostApplication(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherSourceApplication, WeatherSourceApplicationImpl>();
        }
    }
}
