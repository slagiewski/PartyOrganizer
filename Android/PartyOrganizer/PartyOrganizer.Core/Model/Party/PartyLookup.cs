using Newtonsoft.Json;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyLookup
    {
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("unix")]
        public int Unix { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}