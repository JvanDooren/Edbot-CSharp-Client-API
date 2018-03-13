namespace EdbotClientAPI.Communication.Web.Json
{
    using Newtonsoft.Json;
    using System;

    public class Motion : IEquatable<Motion>
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        public bool Equals(Motion other)
        {
            if (other == null) return false;
            else if (!string.IsNullOrEmpty(Name))
            {
                return Name.Equals(other.Name) && Id == other.Id;
            }
            else return string.IsNullOrEmpty(other.Name) && Id == other.Id;
        }
    }
}
