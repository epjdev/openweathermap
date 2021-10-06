using epjdev.OpenWeatherMap.Model;
using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.WeatherInfoCollector
{
    public interface IWeatherInfoCollector
    {
        IEnumerable<string> Collect(string[] cityNames);

        string[] ValidateResults(string[] cityWeathers);

        SimplifiedWeatherInfo[] Simplify(string[] weathers);

        List<StoreWeatherInfo> ConvertWeatherToStore(SimplifiedWeatherInfo[] weathers);
    }
}
