namespace EdbotClientAPI.Communication.Web.Json
{
    using Newtonsoft.Json;

    public class Colours
    {
        [JsonProperty("red")]
        public int Red { get; set; }

        [JsonProperty("magenta")]
        public int Magenta { get; set; }

        [JsonProperty("green")]
        public int Green { get; set; }

        [JsonProperty("blue")]
        public int Blue { get; set; }

        [JsonProperty("white")]
        public int White { get; set; }

        [JsonProperty("yellow")]
        public int Yellow { get; set; }

        [JsonProperty("cyan")]
        public int Cyan { get; set; }

        [JsonProperty("off")]
        public int Off { get; set; }
    }
}
