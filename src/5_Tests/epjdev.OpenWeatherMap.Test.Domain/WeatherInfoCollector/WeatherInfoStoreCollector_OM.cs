using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.OWMIntegration.Weather;
using System;
using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.Test.Domain.WeatherInfoCollector
{
    public class WeatherInfoStoreCollector_OM
    {
        public const string PortoAlegreResult =
            @"{
  ""coord"": {
    ""lon"": -51.23,
    ""lat"": -30.0331
  },
  ""weather"": [
    {
      ""id"": 800,
      ""main"": ""Clear"",
      ""description"": ""céu limpo"",
      ""icon"": ""01n""
    }
  ],
  ""base"": ""stations"",
  ""main"": {
    ""temp"": 14.94,
    ""feels_like"": 14.21,
    ""temp_min"": 13.78,
    ""temp_max"": 15.55,
    ""pressure"": 1018,
    ""humidity"": 66
  },
  ""visibility"": 10000,
  ""wind"": {
    ""speed"": 3.13,
    ""deg"": 50,
    ""gust"": 7.6
  },
  ""clouds"": {
    ""all"": 0
  },
  ""dt"": 1633483457,
  ""sys"": {
    ""type"": 2,
    ""id"": 2021802,
    ""country"": ""BR"",
    ""sunrise"": 1633424281,
    ""sunset"": 1633469297
  },
  ""timezone"": -10800,
  ""id"": 3452925,
  ""name"": ""Porto Alegre"",
  ""cod"": 200
}";

        public const string CuritibaResult =
            @"{
  ""coord"": {
    ""lon"": -49.2908,
    ""lat"": -25.504
  },
  ""weather"": [
    {
      ""id"": 804,
      ""main"": ""Clouds"",
      ""description"": ""nublado"",
      ""icon"": ""04n""
    }
  ],
  ""base"": ""stations"",
  ""main"": {
    ""temp"": 12.74,
    ""feels_like"": 12.39,
    ""temp_min"": 11.71,
    ""temp_max"": 14.19,
    ""pressure"": 1021,
    ""humidity"": 89
  },
  ""visibility"": 10000,
  ""wind"": {
    ""speed"": 2.68,
    ""deg"": 254,
    ""gust"": 4.92
  },
  ""clouds"": {
    ""all"": 90
  },
  ""dt"": 1633483733,
  ""sys"": {
    ""type"": 2,
    ""id"": 67576,
    ""country"": ""BR"",
    ""sunrise"": 1633423943,
    ""sunset"": 1633468703
  },
  ""timezone"": -10800,
  ""id"": 6322752,
  ""name"": ""Curitiba"",
  ""cod"": 200
}";

        public const string FlorianopolisResult =
            @"{
  ""coord"": {
    ""lon"": -48.5012,
    ""lat"": -27.6146
  },
  ""weather"": [
    {
      ""id"": 804,
      ""main"": ""Clouds"",
      ""description"": ""nublado"",
      ""icon"": ""04n""
    }
  ],
  ""base"": ""stations"",
  ""main"": {
    ""temp"": 15.7,
    ""feels_like"": 15.28,
    ""temp_min"": 14.59,
    ""temp_max"": 16.87,
    ""pressure"": 1020,
    ""humidity"": 75
  },
  ""visibility"": 10000,
  ""wind"": {
    ""speed"": 0.51,
    ""deg"": 0
  },
  ""clouds"": {
    ""all"": 90
  },
  ""dt"": 1633483480,
  ""sys"": {
    ""type"": 2,
    ""id"": 2012878,
    ""country"": ""BR"",
    ""sunrise"": 1633423696,
    ""sunset"": 1633468572
  },
  ""timezone"": -10800,
  ""id"": 6323121,
  ""name"": ""Florianópolis"",
  ""cod"": 200
}";

        public const string MethodErrorResult =
            @"{
  ""cod"": ""404"",
  ""message"": ""Internal error""
}";

        public static IEnumerable<string> WeatherConsultSuccess = new string[] { PortoAlegreResult, CuritibaResult, FlorianopolisResult };

        public static IEnumerable<string> WeatherOneConsultNoConnection = new string[] { PortoAlegreResult, string.Empty, FlorianopolisResult };

        public static IEnumerable<string> WeatherMethodError = new string[] { MethodErrorResult, MethodErrorResult, MethodErrorResult };

        public static IEnumerable<string> WeatherInvalidUrl = new string[] { string.Empty, string.Empty, string.Empty };

        public static string[] FilteredOneConsultNoConnection = new string[] { PortoAlegreResult, FlorianopolisResult };

        public static SimplifiedWeatherInfo[] SimplifiedWeatherInfos = new SimplifiedWeatherInfo[]
        {
            new SimplifiedWeatherInfo() { CityId = 2021802, CityName = "Porto Alegre", Date = DateTime.Parse("2021/10/05 20:21:35"), Temperature = "14.94", Condition = "céu limpo"},
            new SimplifiedWeatherInfo() { CityId = 67576, CityName = "Curitiba", Date = DateTime.Parse("2021/10/05 20:21:35"), Temperature = "13.94", Condition = "nublado"},
            new SimplifiedWeatherInfo() { CityId = 2012878, CityName = "Florianópolis", Date = DateTime.Parse("2021/10/05 20:21:35"), Temperature = "16.94", Condition = "nuvens dispersas"}
        };
    }

    public class StubOMWCurrentWeatherSuccess : IOWMCurrentWeather
    {
        public string GetWeatherByCity(string cityName)
        {
            switch (cityName)
            {
                case "Porto Alegre":
                    return WeatherInfoStoreCollector_OM.PortoAlegreResult;

                case "Curitiba":
                    return WeatherInfoStoreCollector_OM.CuritibaResult;

                default:
                    return WeatherInfoStoreCollector_OM.FlorianopolisResult;
            }
        }
    }

    public class StubOMWCurrentWeatherOneConsultNoConnection : IOWMCurrentWeather
    {
        public string GetWeatherByCity(string cityName)
        {
            switch (cityName)
            {
                case "Porto Alegre":
                    return WeatherInfoStoreCollector_OM.PortoAlegreResult;

                case "Curitiba":
                    return string.Empty;

                default:
                    return WeatherInfoStoreCollector_OM.FlorianopolisResult;
            }
        }
    }

    public class StubOMWCurrentWeatherMethodError : IOWMCurrentWeather
    {
        public string GetWeatherByCity(string cityName)
        {
            return WeatherInfoStoreCollector_OM.MethodErrorResult;
        }
    }

    public class StubOMWCurrentWeatherInvalidUrl : IOWMCurrentWeather
    {
        public string GetWeatherByCity(string cityName)
        {
            return string.Empty;
        }
    }
}
