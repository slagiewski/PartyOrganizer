using Newtonsoft.Json;

namespace PartyOrganizer.Core.Model.Member
{
    public class UserBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}