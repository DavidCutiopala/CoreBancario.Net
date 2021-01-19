using Newtonsoft.Json;

namespace VotoElectronico.Generico
{
    public class DtoVotoRSA
    {
        [JsonProperty("votoCifrado")]
        public string VotoCifrado { get; set; }

        [JsonProperty("votoFirmado")]
        public string VotoFirmado { get; set; }

        [JsonProperty("mascara")]
        public string Mascara { get; set; }

        [JsonProperty("pelcid")]
        public long ProcesoElectoralId { get; set; }

    }


    
}
