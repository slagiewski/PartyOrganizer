using Newtonsoft.Json;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyContent
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; } = "171448/cyberscooty-let-s-party-1.png";

        [JsonProperty("items")]
        public Dictionary<string, PartyItem> Items { get; set; }

        [JsonProperty("location")]
        public LocationData Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public IEnumerable<int> Order { get; set; }

        [JsonProperty("unix")]
        public int Unix { get; set; }
    }
}