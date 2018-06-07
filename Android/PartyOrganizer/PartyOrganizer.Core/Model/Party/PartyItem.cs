using Newtonsoft.Json;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyItem
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}