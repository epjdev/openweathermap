using epjdev.OpenWeatherMap.Cache;
using epjdev.OpenWeatherMap.Model;
using epjdev.OpenWeatherMap.WeatherInfoStorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.Test.Domain.WeatherInfoStorer
{
    [TestClass]
    public class WeatherInfoStoreManager_Test
    {
        ServiceProvider _services;

        [TestInitialize]
        public void TestInitialize()
        {
            _services = Initializer.StartSuccess();
        }

        [TestMethod]
        public void Salvar_Informacoes_Multiplas_E_Organizadas_Por_Cidade()
        {
            //arrange
            IWeatherInfoStoreManager weatherInfoStoreManager = _services.GetService<IWeatherInfoStoreManager>();
            List<StoreWeatherInfo> originalStorageInfo = WeatherInfoStoreManager_OM.CollectionSample();

            //execute
            weatherInfoStoreManager.Save(originalStorageInfo);

            StoreWeatherInfo poaStorageInfo = WeatherInfoStorage.Get("Porto Alegre");
            StoreWeatherInfo ctbaStorage = WeatherInfoStorage.Get("Curitiba");
            StoreWeatherInfo floripaStorage = WeatherInfoStorage.Get("Florianópolis");

            //assert
            AssertStorage(originalStorageInfo, poaStorageInfo, "Porto Alegre");
            AssertStorage(originalStorageInfo, ctbaStorage, "Curitiba");
            AssertStorage(originalStorageInfo, floripaStorage, "Florianópolis");
        }

        private void AssertStorage(List<StoreWeatherInfo> originalStorageInfo, StoreWeatherInfo storage, string cityName)
        {
            Assert.IsNotNull(storage);
            Assert.IsTrue(storage.WeatherInfos.Count == originalStorageInfo.FindAll(swi => swi.CityName == cityName).Count);
            foreach (StoreWeatherInfo storeWeatherInfo in originalStorageInfo.FindAll(swi => swi.CityName == cityName))
                foreach (WeatherInfo weatherInfo in storeWeatherInfo.WeatherInfos)
                    Assert.IsTrue(storage.WeatherInfos.Contains(weatherInfo));
        }

        [DataTestMethod]
        [DataRow("Porto Alegre", "2021/10/05 20:20:35", "2021/10/05 20:34:35", 4)]
        [DataRow("Porto Alegre", "2021/10/05 20:22:35", "2021/10/05 20:26:35", 2)]
        [DataRow("Porto Alegre", "2021/10/05 20:26:35", "2021/10/05 20:34:35", 1)]
        [DataRow("Curitiba", "2021/10/05 20:20:35", "2021/10/05 20:34:35", 5)]
        [DataRow("Curitiba", "2021/10/05 20:22:35", "2021/10/05 20:26:35", 2)]
        [DataRow("Curitiba", "2021/10/05 20:26:35", "2021/10/05 20:34:35", 2)]
        [DataRow("Florianópolis", "2021/10/05 20:20:35", "2021/10/05 20:34:35", 7)]
        [DataRow("Florianópolis", "2021/10/05 20:22:35", "2021/10/05 20:26:35", 2)]
        [DataRow("Florianópolis", "2021/10/05 20:26:35", "2021/10/05 20:34:35", 4)]
        public void Retornar_Informacoes_Multiplas_Por_Cidade_E_Data_Com_Sucesso(string cityName, string startDate, string endDate, int assertCount)
        {
            //arrange
            IWeatherInfoStoreManager weatherInfoStoreManager = _services.GetService<IWeatherInfoStoreManager>();
            List<StoreWeatherInfo> originalStorageInfo = WeatherInfoStoreManager_OM.CollectionSample();
            weatherInfoStoreManager.Save(originalStorageInfo);

            //execute
            ResultWeatherInfo resultWeatherInfo = weatherInfoStoreManager.Get(cityName, DateTime.Parse(startDate), DateTime.Parse(endDate));

            //assert
            Assert.AreEqual(resultWeatherInfo.Message, MessageConstants.Sucess);
            Assert.AreEqual(resultWeatherInfo.StoreWeatherInfo.WeatherInfos.Count, assertCount);
        }

        [DataTestMethod]
        [DataRow("Lages", "2021/10/05 20:20:35", "2021/10/05 20:34:35")]
        [DataRow("Curitiba", "2021/10/05 19:22:35", "2021/10/05 20:12:35")]
        public void Retornar_Informacoes_Multiplas_Por_Cidade_E_Data_Com_Falha_No_Filtro(string cityName, string startDate, string endDate)
        {
            //arrange
            IWeatherInfoStoreManager weatherInfoStoreManager = _services.GetService<IWeatherInfoStoreManager>();
            List<StoreWeatherInfo> originalStorageInfo = WeatherInfoStoreManager_OM.CollectionSample();
            weatherInfoStoreManager.Save(originalStorageInfo);

            //execute
            ResultWeatherInfo resultWeatherInfo = weatherInfoStoreManager.Get(cityName, DateTime.Parse(startDate), DateTime.Parse(endDate));

            //assert
            Assert.AreEqual(resultWeatherInfo.Message, MessageConstants.SucessNotFound);
            Assert.IsNull(resultWeatherInfo.StoreWeatherInfo);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WeatherInfoStorage.ClearStorage();
        }
    }
}
