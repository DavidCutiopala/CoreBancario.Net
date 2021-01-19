using Newtonsoft.Json;
using System.Collections.Generic;
namespace VotoElectronico.Generico
{
    public class DtoApiResponseMessage
    {
        [JsonProperty("errorExceptionMessage")]
        public DtoMensaje errorExceptionMessage { get; set; }
        [JsonProperty("informationMessage")]
        public DtoMensaje informationMessage { get; set; }
        [JsonProperty("responseList")]
        public IEnumerable<object> responseList { get; set; }

    }
}
