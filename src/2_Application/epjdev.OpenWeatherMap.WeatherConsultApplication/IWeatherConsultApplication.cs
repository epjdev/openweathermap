using epjdev.OpenWeatherMap.Model;
using System;

namespace epjdev.OpenWeatherMap.WeatherConsultApplication
{
    public interface IWeatherConsultApplication
    {
        ResultWeatherInfo Consult(string cityNames, DateTime startDate, DateTime endDate);
    }
}
