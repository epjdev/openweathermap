using epjdev.OpenWeatherMap.Model;
using System;

namespace epjdev.OpenWeatherMap.Test.Infra.Cache
{
    public class WeatherInfoStorage_OM
    {
        public static StoreWeatherInfo PortoAlegreSample()
        {
            StoreWeatherInfo storeWeatherInfo = new StoreWeatherInfo()
            {
                CityId = 3452925,
                CityName = "Porto Alegre"
            };

            WeatherInfo weatherInfo = new WeatherInfo()
            {
                Date = new DateTime(2021, 10, 05, 20, 21, 35),
                Temperature = "15.93",
                Condition = "céu limpo"
            };

            storeWeatherInfo.WeatherInfos.Add(weatherInfo);

            return storeWeatherInfo;
        }
    }
}
