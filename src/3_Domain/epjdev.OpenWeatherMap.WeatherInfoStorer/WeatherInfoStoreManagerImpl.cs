using epjdev.OpenWeatherMap.Cache;
using epjdev.OpenWeatherMap.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace epjdev.OpenWeatherMap.WeatherInfoStorer
{
    internal class WeatherInfoStoreManagerImpl : IWeatherInfoStoreManager
    {
        public void Save(List<StoreWeatherInfo> weatherInfos)
        {
            foreach (StoreWeatherInfo storeWeatherInfo in weatherInfos)
                WeatherInfoStorage.Add(storeWeatherInfo);
        }

        public ResultWeatherInfo Get(string cityName, DateTime startDate, DateTime endDate)
        {
            StoreWeatherInfo storeWeatherInfo = WeatherInfoStorage.Get(cityName);

            if (storeWeatherInfo == null)
                return new ResultWeatherInfo() { Message = MessageConstants.SucessNotFound };

            if (!storeWeatherInfo.WeatherInfos.Any(swi => swi.Date >= startDate && swi.Date <= endDate))
                return new ResultWeatherInfo() { Message = MessageConstants.SucessNotFound };

            StoreWeatherInfo filteredWeatherInfo = new StoreWeatherInfo();
            filteredWeatherInfo.CityId = storeWeatherInfo.CityId;
            filteredWeatherInfo.CityName = storeWeatherInfo.CityName;
            filteredWeatherInfo.WeatherInfos.AddRange(storeWeatherInfo.WeatherInfos.FindAll(swi => swi.Date >= startDate && swi.Date <= endDate));

            return new ResultWeatherInfo() { Message = MessageConstants.Sucess, StoreWeatherInfo = filteredWeatherInfo };
        }
    }
}
