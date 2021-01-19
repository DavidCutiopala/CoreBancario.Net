
using Newtonsoft.Json;
using System.Collections.Generic;

namespace VotoElectronico.Generico
{
    public class DtoAES
    {
        [JsonProperty("votoFirmado")]
        public string VotoFirmado { get; set; }

        [JsonProperty("votoCifradoAes")]
        public string VotoCifradoAes { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("iv")]
        public string vectorInicializacion { get; set; }

        [JsonProperty("h")]
        public string mascara { get; set; }

        [JsonProperty("PREL")]
        public long procesoElectoralId { get; set; }
    }


    public class DtoOpcion
    {
        [JsonProperty("procesoElectoralId")]
        public string procesoElectoralId { get; set; }

        [JsonProperty("listaId")]
        public string listaId { get; set; }

        [JsonProperty("candidatoId")]
        public string candidatoId { get; set; }
        
        [JsonProperty("escanioId")]
        public string escanioId { get; set; }
    }

}
