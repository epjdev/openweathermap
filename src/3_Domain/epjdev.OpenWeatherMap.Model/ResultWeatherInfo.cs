using System.Collections.Generic;

namespace epjdev.OpenWeatherMap.Model
{
    public class ResultWeatherInfo
    {
        public string Message { get; set; }

        public StoreWeatherInfo StoreWeatherInfo { get; set; }
    }

    public static class MessageConstants
    {
        public const string Sucess = "Consulta realizada com sucesso.";

        public const string SucessNotFound = "Registros não encontrados com os parâmetros informados";

        public const string SucessGenericError = "Ocorreu uma falha na execução - {0}";
    }
}
