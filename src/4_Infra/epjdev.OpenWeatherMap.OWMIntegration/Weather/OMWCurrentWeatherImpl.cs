using RestSharp;

namespace epjdev.OpenWeatherMap.OWMIntegration.Weather
{
    internal class OMWCurrentWeatherImpl : OMWCurrentWeatherAbstract, IOWMCurrentWeather
    {
        RestClient _client;

        public OMWCurrentWeatherImpl()
        {
            _client = new RestClient(Url);
            _client.AddDefaultQueryParameter("units", Units);
            _client.AddDefaultQueryParameter("lang", Lang);
            _client.AddDefaultQueryParameter("appid", AppId);
        }

        public string GetWeatherByCity(string cityName)
        {
            IRestRequest request = new RestRequest(Method.GET);
            request.AddQueryParameter("q", cityName);

            IRestResponse response = _client.Execute(request);
            
            return response.Content;
        }
    }
}
