using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.WeatherInfoCollector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace epjdev.OpenWeatherMap.Test.Domain.WeatherInfoCollector
{
    [TestClass]
    public class WeatherInfoStoreCollector_Test
    {
        [TestMethod]
        public void Capturar_Informacao_Da_Api_Open_Weather_Com_Sucesso()
        {
            //arrange
            ServiceProvider services = Initializer.StartSuccess();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            IEnumerable<string> weatherInfo = weatherInfoCollector.Collect(ConsultWeatherInfo.CityNames);

            //assert
            Assert.AreEqual(weatherInfo.Count(), ConsultWeatherInfo.CityNames.Length);
            Assert.IsFalse(weatherInfo.Any(wisa => string.IsNullOrEmpty(wisa)));
        }

        [TestMethod]
        public void Capturar_Informacao_Da_Api_Open_Weather_Com_Uma_Consulta_Sem_Conexao()
        {
            //arrange
            ServiceProvider services = Initializer.StartOneCityNoConnection();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            IEnumerable<string> weatherInfo = weatherInfoCollector.Collect(ConsultWeatherInfo.CityNames);

            //assert
            Assert.AreEqual(weatherInfo.Count(), ConsultWeatherInfo.CityNames.Length);
            Assert.IsTrue(weatherInfo.Any(wisa => string.IsNullOrEmpty(wisa)));
        }

        [TestMethod]
        public void Capturar_Informacao_Da_Api_Open_Weather_Com_Falha_De_Metodo()
        {
            //arrange
            ServiceProvider services = Initializer.StartMethodError();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            IEnumerable<string> weatherInfo = weatherInfoCollector.Collect(ConsultWeatherInfo.CityNames);

            //assert
            Assert.AreEqual(weatherInfo.Count(), ConsultWeatherInfo.CityNames.Length);
            Assert.IsFalse(weatherInfo.Any(wisa => string.IsNullOrEmpty(wisa)));
        }

        [TestMethod]
        public void Capturar_Informacao_Da_Api_Open_Weather_Com_Url_Invalida()
        {
            //arrange
            ServiceProvider services = Initializer.StartInvalidUrl();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            IEnumerable<string> weatherInfo = weatherInfoCollector.Collect(ConsultWeatherInfo.CityNames);

            //assert
            Assert.AreEqual(weatherInfo.Count(), ConsultWeatherInfo.CityNames.Length);
            Assert.IsTrue(weatherInfo.All(wisa => string.IsNullOrEmpty(wisa)));
        }

        [TestMethod]
        public void Validar_Informacao_Da_Api_Open_Weather_Com_Falha_De_Metodo()
        {
            //arrange
            ServiceProvider services = Initializer.Start();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            string[] weatherInfosArray = weatherInfoCollector.ValidateResults(WeatherInfoStoreCollector_OM.WeatherMethodError.ToArray());

            //assert
            Assert.AreEqual(weatherInfosArray.Count(), 0);
        }

        [TestMethod]
        public void Validar_Informacao_Da_Api_Open_Weather_Com_Url_Invalida()
        {
            //arrange
            ServiceProvider services = Initializer.Start();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            string[] weatherInfosArray = weatherInfoCollector.ValidateResults(WeatherInfoStoreCollector_OM.WeatherInvalidUrl.ToArray());

            //assert
            Assert.AreEqual(weatherInfosArray.Count(), 0);
        }

        [TestMethod]
        public void Validar_e_Simplificar_Informacao_Da_Api_Open_Weather_Com_Sucesso()
        {
            //arrange
            ServiceProvider services = Initializer.Start();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            string[] weatherInfosArray = weatherInfoCollector.ValidateResults(WeatherInfoStoreCollector_OM.WeatherConsultSuccess.ToArray());
            SimplifiedWeatherInfo[] simplifiedWeatherInfos = weatherInfoCollector.Simplify(weatherInfosArray);


            //assert
            Assert.AreEqual(simplifiedWeatherInfos.Count(), 3);
            Assert.AreEqual(simplifiedWeatherInfos.Count(swi => swi.CityName == "Porto Alegre"), 1);
            Assert.AreEqual(simplifiedWeatherInfos.Count(swi => swi.CityName == "Curitiba"), 1);
            Assert.AreEqual(simplifiedWeatherInfos.Count(swi => swi.CityName == "Florianópolis"), 1);
        }

        [TestMethod]
        public void Validar_e_Simplificar_Informacao_Da_Api_Open_Weather_Com_Sucesso_De_Uma_Consulta_Sem_Conexao()
        {
            //arrange
            ServiceProvider services = Initializer.Start();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            string[] weatherInfosArray = weatherInfoCollector.ValidateResults(WeatherInfoStoreCollector_OM.WeatherOneConsultNoConnection.ToArray());
            SimplifiedWeatherInfo[] simplifiedWeatherInfos = weatherInfoCollector.Simplify(weatherInfosArray);

            //assert
            Assert.AreEqual(simplifiedWeatherInfos.Count(), 2);
        }

        [TestMethod]
        public void Converter_Informacao_Simplificada_Da_Api_Open_Weather_Com_Sucesso()
        {
            //arrange
            ServiceProvider services = Initializer.Start();
            IWeatherInfoCollector weatherInfoCollector = services.GetService<IWeatherInfoCollector>();

            //execute
            List<StoreWeatherInfo> storeWeatherInfos = weatherInfoCollector.ConvertWeatherToStore(WeatherInfoStoreCollector_OM.SimplifiedWeatherInfos);

            //assert
            Assert.AreEqual(storeWeatherInfos.Count(), 3);
            AssertEspecificCityConversion("Porto Alegre", storeWeatherInfos);
            AssertEspecificCityConversion("Curitiba", storeWeatherInfos);
            AssertEspecificCityConversion("Florianópolis", storeWeatherInfos);
        }

        private void AssertEspecificCityConversion(string cityName, List<StoreWeatherInfo> storeWeatherInfos)
        {
            SimplifiedWeatherInfo simplifiedWeatherInfo = WeatherInfoStoreCollector_OM.SimplifiedWeatherInfos.First(swi => swi.CityName == cityName);
            StoreWeatherInfo storeWeatherInfo = storeWeatherInfos.First(swi => swi.CityName == cityName);

            Assert.AreEqual(simplifiedWeatherInfo.CityId, storeWeatherInfo.CityId);
            Assert.AreEqual(simplifiedWeatherInfo.CityName, storeWeatherInfo.CityName);
            Assert.AreEqual(simplifiedWeatherInfo.Date, storeWeatherInfo.WeatherInfos[0].Date);
            Assert.AreEqual(simplifiedWeatherInfo.Temperature, storeWeatherInfo.WeatherInfos[0].Temperature);
            Assert.AreEqual(simplifiedWeatherInfo.Condition, storeWeatherInfo.WeatherInfos[0].Condition);
        }
    }
}