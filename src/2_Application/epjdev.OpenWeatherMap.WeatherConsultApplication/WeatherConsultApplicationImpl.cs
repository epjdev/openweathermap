using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.WeatherInfoStorer;
using System;

namespace epjdev.OpenWeatherMap.WeatherConsultApplication
{
    internal class WeatherConsultApplicationImpl : IWeatherConsultApplication
    {
        IWeatherInfoStoreManager _weatherInfoStorer;

        public WeatherConsultApplicationImpl(IWeatherInfoStoreManager weatherInfoStorer)
        {
            _weatherInfoStorer = weatherInfoStorer;
        }

        public ResultWeatherInfo Consult(string cityName, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _weatherInfoStorer.Get(cityName, startDate, endDate);
            }
            catch (Exception exception)
            {
                string message = string.Format(MessageConstants.SucessGenericError, exception.ToString());
                return new ResultWeatherInfo() { Message = message };
            }
        }
    }
}