using System;
using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.Model
{
    public class StoreWeatherInfo
    {
        public long CityId { get; set; }

        public string CityName { get; set; }

        public List<WeatherInfo> WeatherInfos = new List<WeatherInfo>();
    }

    public class WeatherInfo
    {
        public DateTime Date { get; set; }

        public string Temperature { get; set; }

        public string Condition { get; set; }
    }
}
