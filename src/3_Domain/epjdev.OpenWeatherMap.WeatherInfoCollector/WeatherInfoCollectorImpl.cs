using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.OWMIntegration.Weather;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace epjdev.OpenWeatherMap.WeatherInfoCollector
{
    internal class WeatherInfoCollectorImpl : IWeatherInfoCollector
    {
        IOWMCurrentWeather _currentWeather;

        public WeatherInfoCollectorImpl(IOWMCurrentWeather currentWeather)
        {
            _currentWeather = currentWeather;
        }

        public IEnumerable<string> Collect(string[] cityNames)
        {
            for (int i = 0; i < cityNames.Length; i++)
            {
                string weather = _currentWeather.GetWeatherByCity(cityNames[i]);
                yield return weather;
            }
        }

        public string[] ValidateResults(string[] cityWeathers)
        {
            List<string> validResults = new List<string>();

            for (int i = 0; i < cityWeathers.Length; i++)
            {
                try
                {
                    JObject jsonObject = JObject.Parse(cityWeathers[i]);
                    int code = jsonObject.SelectToken("$.cod").Value<Int32>();

                    if (code == 200)
                        validResults.Add(cityWeathers[i]);
                }
                catch
                {
                    continue;
                }
            }

            return validResults.ToArray();
        }

        public SimplifiedWeatherInfo[] Simplify(string[] weathers)
        {
            SimplifiedWeatherInfo[] simplifiedWeathers = new SimplifiedWeatherInfo[weathers.Length];

            for (int i = 0; i < weathers.Length; i++)
                simplifiedWeathers[i] = SimplifyWeather(weathers[i]);

            return simplifiedWeathers;
        }

        private SimplifiedWeatherInfo SimplifyWeather(string weather)
        {
            JObject jsonObject = JObject.Parse(weather);

            SimplifiedWeatherInfo simplifiedWeather = new SimplifiedWeatherInfo();
            simplifiedWeather.CityId = jsonObject.SelectToken("$.sys.id").Value<Int64>();
            simplifiedWeather.CityName = jsonObject.SelectToken("$.name").Value<string>();
            simplifiedWeather.Temperature = jsonObject.SelectToken("$.main.temp").Value<string>();
            simplifiedWeather.Condition = jsonObject.SelectToken("$.weather[0].description").Value<string>();
            simplifiedWeather.Date = DateTime.Now;

            return simplifiedWeather;
        }

        public List<StoreWeatherInfo> ConvertWeatherToStore(SimplifiedWeatherInfo[] weathers)
        {
            List<StoreWeatherInfo> storeWeatherInfos = new List<StoreWeatherInfo>();

            foreach (SimplifiedWeatherInfo simplifiedWeather in weathers)
            {
                StoreWeatherInfo storeWeatherInfo = new StoreWeatherInfo();
                storeWeatherInfo.CityId = simplifiedWeather.CityId;
                storeWeatherInfo.CityName = simplifiedWeather.CityName;

                WeatherInfo weatherInfo = CreateWeatherInfoFromSimplified(simplifiedWeather);

                storeWeatherInfo.WeatherInfos.Add(weatherInfo);

                storeWeatherInfos.Add(storeWeatherInfo);
            }

            return storeWeatherInfos;
        }

        private WeatherInfo CreateWeatherInfoFromSimplified(SimplifiedWeatherInfo simplifiedWeather)
        {
            WeatherInfo weatherInfo = new WeatherInfo();
            weatherInfo.Date = simplifiedWeather.Date;
            weatherInfo.Temperature = simplifiedWeather.Temperature;
            weatherInfo.Condition = simplifiedWeather.Condition;

            return weatherInfo;
        }
    }
}
