using epjdev.OpenWeatherMap.Model;
using System;
using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.WeatherInfoStorer
{
    public interface IWeatherInfoStoreManager
    {
        void Save(List<StoreWeatherInfo> weatherInfos);

        ResultWeatherInfo Get(string cityName, DateTime startDate, DateTime endDate);
    }
}
