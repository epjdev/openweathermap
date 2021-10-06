using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.WeatherInfoCollector;
using epjdev.OpenWeatherMap.WeatherInfoStorer;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace epjdev.OpenWeatherMap.HostApplication
{
    internal class WeatherSourceApplicationImpl : IWeatherSourceApplication
    {
        IWeatherInfoCollector _weatherInfoCollector;
        IWeatherInfoStoreManager _weatherInfoStorer;

        string[] cityNames = { "Porto Alegre", "Florianópolis", "Curitiba" };

        public WeatherSourceApplicationImpl(IWeatherInfoCollector weatherInfoCollector, IWeatherInfoStoreManager weatherInfoStorer)
        {
            _weatherInfoCollector = weatherInfoCollector;
            _weatherInfoStorer = weatherInfoStorer;
        }

        public void Execute()
        {
            while (true)
            {
                string[] cityWeathers = _weatherInfoCollector.Collect(cityNames).ToArray();

                SimplifiedWeatherInfo[] simplifiedWeathers = _weatherInfoCollector.Simplify(cityWeathers);

                List<StoreWeatherInfo> filteredWeathers = _weatherInfoCollector.FilterByCity(simplifiedWeathers);

                _weatherInfoStorer.Store(filteredWeathers);

                Thread.Sleep(120000);
            }
        }

        public void Dispose()
        {
            //erase data from file
        }
    }
}
