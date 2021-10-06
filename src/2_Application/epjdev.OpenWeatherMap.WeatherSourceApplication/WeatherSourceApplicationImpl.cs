using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.WeatherInfoCollector;
using epjdev.OpenWeatherMap.WeatherInfoStorer;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace epjdev.OpenWeatherMap.WeatherSourceApplication
{
    internal class WeatherSourceApplicationImpl : IWeatherSourceApplication
    {
        IWeatherInfoCollector _weatherInfoCollector;
        IWeatherInfoStoreManager _weatherInfoStorer;

        public WeatherSourceApplicationImpl(IWeatherInfoCollector weatherInfoCollector, IWeatherInfoStoreManager weatherInfoStorer)
        {
            _weatherInfoCollector = weatherInfoCollector;
            _weatherInfoStorer = weatherInfoStorer;
        }

        public void Execute()
        {
            while (true)
            {
                IEnumerable<string> cityWeathers = _weatherInfoCollector.Collect(ConsultWeatherInfo.CityNames);

                cityWeathers = _weatherInfoCollector.ValidateResults(cityWeathers.ToArray());

                SimplifiedWeatherInfo[] simplifiedWeathers = _weatherInfoCollector.Simplify(cityWeathers.ToArray());

                List<StoreWeatherInfo> toStoreWeathers = _weatherInfoCollector.ConvertWeatherToStore(simplifiedWeathers);

                _weatherInfoStorer.Save(toStoreWeathers);

                Thread.Sleep(120000);
            }
        }
    }
}
