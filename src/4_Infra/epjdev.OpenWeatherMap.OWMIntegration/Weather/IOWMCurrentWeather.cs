using System;

namespace epjdev.OpenWeatherMap.OWMIntegration.Weather
{
    public interface IOWMCurrentWeather
    {
        string GetWeatherByCity(string cityName);
    }
}
