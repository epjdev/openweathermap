using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.WeatherConsultApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace epjdev.OpenWeatherMap.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        IWeatherConsultApplication _weatherConsultApplication;

        public WeatherController(IWeatherConsultApplication weatherConsultApplication)
        {
            _weatherConsultApplication = weatherConsultApplication;
        }

        [HttpGet("ConsultWeatherByDateInterval")]
        [Authorize]
        public ResultWeatherInfo ConsultWeatherByDateInterval(string city, string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                return new ResultWeatherInfo() { Message = "Parâmetros obrigatórios não informados." };

            DateTime convertedStartDate;
            bool successStartDateParse = DateTime.TryParse(startDate, out convertedStartDate);

            if (!successStartDateParse)
                return new ResultWeatherInfo() { Message = "Falha na conversão de data e hora inicial da chamada. Formato deve ser: aaaa/MM/dd HH:mm:ss e deve válida." };
            
            DateTime convertedEndDate;
            bool successEndDateParse = DateTime.TryParse(endDate, out convertedEndDate);

            if (!successEndDateParse)
                return new ResultWeatherInfo() { Message = "Falha na conversão de data e hora final da chamada. Formato deve ser: aaaa/MM/dd HH:mm:ss e deve válida." };

            return _weatherConsultApplication.Consult(city, convertedStartDate, convertedEndDate);
        }
    }
}
