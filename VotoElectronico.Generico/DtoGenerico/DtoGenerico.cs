using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoGenerico
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("dt1")]
        public string Dt1 { get; set; }
        [JsonProperty("dt2")]
        public string Dt2 { get; set; }

        [JsonProperty("dt3")]
        public string Dt3 { get; set; }

        [JsonProperty("dt4")]
        public string Dt4 { get; set; }

        [JsonProperty("dt5")]
        public string Dt5 { get; set; }

        [JsonProperty("dt6")]
        public string Dt6 { get; set; }

        [JsonProperty("dt7")]
        public string Dt7 { get; set; }

        [JsonProperty("dt8")]
        public string Dt8 { get; set; }
        [JsonProperty("dt20")]
        public string Dt20 { get; set; }

        [JsonProperty("ndt1")]
        public decimal Ndt1 { get; set; }
        [JsonProperty("ndt2")]
        public decimal Ndt2 { get; set; }
        [JsonProperty("ndt3")]
        public decimal Ndt3 { get; set; }
        [JsonProperty("bdt1")]
        public bool Bdt1 { get; set; }



    }
}
