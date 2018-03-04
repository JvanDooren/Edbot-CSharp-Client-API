namespace EdbotClientAPI.Communication.Web.Json
{
    using Newtonsoft.Json;

    public class Servo
    {
        [JsonProperty("aligning")]
        public bool Aligning { get; set; }

        [JsonProperty("load")]
        public double Load { get; set; }

        [JsonProperty("torque")]
        public bool Torque { get; set; }

        [JsonProperty("position")]
        public double Position { get; set; }

        //unknown type
        [JsonProperty("extended")]
        public object Extended { get; set; }
    }
}
