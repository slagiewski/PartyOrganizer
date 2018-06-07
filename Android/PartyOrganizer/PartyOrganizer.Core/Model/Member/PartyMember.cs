using Newtonsoft.Json;
using PartyOrganizer.Core.Model.Member;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyMember : User
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "guest";

        [JsonProperty("items")]
        public Dictionary<string, PartyItem> Items{ get; set; }
        
    }
}