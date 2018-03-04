namespace EdbotClientAPI.Communication.Web.Json
{
    using Newtonsoft.Json;

    public class Motion
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
