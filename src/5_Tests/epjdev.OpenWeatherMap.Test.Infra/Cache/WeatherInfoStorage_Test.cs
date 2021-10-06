using epjdev.OpenWeatherMap.Cache;
using epjdev.OpenWeatherMap.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace epjdev.OpenWeatherMap.Test.Infra.Cache
{
    [TestClass]
    public class WeatherInfoStorage_Test
    {
        [TestMethod]
        public void Armezenar_e_Resgatar_um_Objeto_no_Storage_Com_Sucesso()
        {
            //arrange
            StoreWeatherInfo storeWeatherInfo = WeatherInfoStorage_OM.PortoAlegreSample();

            //execute
            WeatherInfoStorage.Add(storeWeatherInfo);

            StoreWeatherInfo assertStoreWeatherInfo = WeatherInfoStorage.Get("Porto Alegre");

            //assert
            Assert.IsNotNull(assertStoreWeatherInfo);
            Assert.AreEqual(storeWeatherInfo, assertStoreWeatherInfo);
        }

        [TestMethod]
        public void Armezenar_e_Resgatar_um_Objeto_no_Storage_Com_Falha_Objeto_Nao_Encontrado()
        {
            //arrange
            StoreWeatherInfo storeWeatherInfo = WeatherInfoStorage_OM.PortoAlegreSample();

            //execute
            WeatherInfoStorage.Add(storeWeatherInfo);

            StoreWeatherInfo assertStoreWeatherInfo = WeatherInfoStorage.Get("Florianópolis");

            //assert
            Assert.IsNull(assertStoreWeatherInfo);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WeatherInfoStorage.ClearStorage();
        }
    }
}
