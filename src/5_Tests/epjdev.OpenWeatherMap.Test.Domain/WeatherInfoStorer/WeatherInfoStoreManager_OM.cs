using epjdev.OpenWeatherMap.Model;
using System;
using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.Test.Domain.WeatherInfoStorer
{
    public class WeatherInfoStoreManager_OM
    {
        public static List<StoreWeatherInfo> CollectionSample()
        {
            List<StoreWeatherInfo> storeWeatherInfos = new List<StoreWeatherInfo>();

            storeWeatherInfos.Add(CreateStoreWeatherInfo(2021802, "Porto Alegre", "2021/10/05 20:21:35", "15.93", "céu limpo"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2021802, "Porto Alegre", "2021/10/05 20:23:35", "15.6", "céu limpo"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2021802, "Porto Alegre", "2021/10/05 20:25:35", "14.1", "algumas nuvens"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2021802, "Porto Alegre", "2021/10/05 20:27:35", "13.52", "nublado"));
            
            storeWeatherInfos.Add(CreateStoreWeatherInfo(67576, "Curitiba", "2021/10/05 20:21:35", "12.78", "nublado"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(67576, "Curitiba", "2021/10/05 20:23:35", "12.32", "nuvens dispersas"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(67576, "Curitiba", "2021/10/05 20:25:35", "11.67", "nuvens dispersas"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(67576, "Curitiba", "2021/10/05 20:27:35", "11.24", "céu limpo"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(67576, "Curitiba", "2021/10/05 20:29:35", "11.01", "céu limpo"));

            storeWeatherInfos.Add(CreateStoreWeatherInfo(2012878, "Florianópolis", "2021/10/05 20:21:35", "16.3", "nublado"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2012878, "Florianópolis", "2021/10/05 20:23:35", "15.93", "nublado"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2012878, "Florianópolis", "2021/10/05 20:25:35", "15.3", "nuvens dispersas"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2012878, "Florianópolis", "2021/10/05 20:27:35", "14.98", "céu limpo"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2012878, "Florianópolis", "2021/10/05 20:29:35", "14.6", "céu limpo"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2012878, "Florianópolis", "2021/10/05 20:31:35", "14.29", "céu limpo"));
            storeWeatherInfos.Add(CreateStoreWeatherInfo(2012878, "Florianópolis", "2021/10/05 20:33:35", "13.9", "céu limpo"));

            return storeWeatherInfos;
        }

        private static StoreWeatherInfo CreateStoreWeatherInfo(long cityId, string cityName, string dateTime, string temperature, string condition)
        {
            StoreWeatherInfo storeWeatherInfo = new StoreWeatherInfo()
            {
                CityId = cityId,
                CityName = cityName
            };

            WeatherInfo weatherInfo = new WeatherInfo()
            {
                Date = DateTime.Parse(dateTime),
                Temperature = temperature,
                Condition = condition
            };

            storeWeatherInfo.WeatherInfos.Add(weatherInfo);

            return storeWeatherInfo;
        }
    }
}
