using Microsoft.Extensions.DependencyInjection;

namespace epjdev.OpenWeatherMap.WeatherConsultApplication
{
    public static class Initializer
    {
        public static void StartWeatherConsultApplication(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherConsultApplication, WeatherConsultApplicationImpl>();
        }
    }
}
