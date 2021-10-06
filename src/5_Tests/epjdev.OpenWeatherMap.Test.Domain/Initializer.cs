using epjdev.OpenWeatherMap.OWMIntegration;
using epjdev.OpenWeatherMap.OWMIntegration.Weather;
using epjdev.OpenWeatherMap.Test.Domain.WeatherInfoCollector;
using epjdev.OpenWeatherMap.WeatherInfoCollector;
using epjdev.OpenWeatherMap.WeatherInfoStorer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace epjdev.OpenWeatherMap.Test.Domain
{
    public class Initializer
    {
        public static ServiceProvider Start()
        {
            IServiceCollection serviceCollection = InitializeDefault();
            serviceCollection.StartOMWIntegration();

            return serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider StartSuccess()
        {
            IServiceCollection serviceCollection = InitializeDefault();

            serviceCollection.AddSingleton<IOWMCurrentWeather, StubOMWCurrentWeatherSuccess>();

            return serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider StartOneCityNoConnection()
        {
            IServiceCollection serviceCollection = InitializeDefault();

            serviceCollection.AddSingleton<IOWMCurrentWeather, StubOMWCurrentWeatherOneConsultNoConnection>();

            return serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider StartMethodError()
        {
            IServiceCollection serviceCollection = InitializeDefault();

            serviceCollection.AddSingleton<IOWMCurrentWeather, StubOMWCurrentWeatherMethodError>();

            return serviceCollection.BuildServiceProvider();
        }

        public static ServiceProvider StartInvalidUrl()
        {
            IServiceCollection serviceCollection = InitializeDefault();

            serviceCollection.AddSingleton<IOWMCurrentWeather, StubOMWCurrentWeatherInvalidUrl>();

            return serviceCollection.BuildServiceProvider();
        }

        private static IServiceCollection InitializeDefault()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.StartWeatherInfoCollector();
            serviceCollection.StartWeatherInfoStorer();

            return serviceCollection;
        }
    }
}
