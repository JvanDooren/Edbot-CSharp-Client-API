namespace EdbotClientAPI.Communication.Web.Json
{
    using Newtonsoft.Json;

    public class Edbot
    {
        [JsonProperty("connected")]
        public bool Connected { get; set; }

        [JsonProperty("reporters")]
        public Reporters Reporters { get; set; }

        [JsonProperty("activeUser")]
        public string ActiveUser { get; set; }

        [JsonProperty("motions")]
        public Motions Motions { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("colours")]
        public Colours Colours { get; set; }
    }
}
