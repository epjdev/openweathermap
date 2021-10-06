using System;

namespace epjdev.OpenWeatherMap.Model
{
    public class SimplifiedWeatherInfo
    {
        public long CityId { get; set; }

        public string CityName { get; set; }

        public string Temperature { get; set; }

        public string Condition { get; set; }

        public DateTime Date { get; set; }
    }
}
