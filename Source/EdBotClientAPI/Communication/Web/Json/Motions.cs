namespace EdbotClientAPI.Communication.Web.Json
{
    using Newtonsoft.Json;

    public class Motions
    {
        [JsonProperty("All")]
        public Motion[] All { get; set; }

        [JsonProperty("Basic")]
        public Motion[] Basic { get; set; }

        [JsonProperty("Sport")]
        public Motion[] Sport { get; set; }

        [JsonProperty("Greet")]
        public Motion[] Greet { get; set; }

        [JsonProperty("Dance")]
        public Motion[] Dance { get; set; }

        [JsonProperty("Gym")]
        public Motion[] Gym { get; set; }

        [JsonProperty("Fight")]
        public Motion[] Fight { get; set; }
    }
}
