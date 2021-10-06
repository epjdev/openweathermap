using epjdev.OpenWeatherMap.Model;
using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.Cache
{
    public static class WeatherInfoStorage
    {
        static Dictionary<string, StoreWeatherInfo> _storage = new Dictionary<string, StoreWeatherInfo>();

        public static void Add(StoreWeatherInfo storeWeatherInfo)
        {
            if (_storage.ContainsKey(storeWeatherInfo.CityName))
            {
                _storage[storeWeatherInfo.CityName].WeatherInfos.AddRange(storeWeatherInfo.WeatherInfos);
            }
            else
            {
                _storage.Add(storeWeatherInfo.CityName, storeWeatherInfo);
            }
        }

        public static StoreWeatherInfo Get(string cityName)
        {
            if (_storage.ContainsKey(cityName))
            {
                return _storage[cityName];
            }

            return null;
        }

        public static void ClearStorage()
        {
            _storage.Clear();
        }
    }
}
