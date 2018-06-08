using Newtonsoft.Json;
using PartyOrganizer.Core.Model.Member;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public partial class Party
    {
        public string Id { get; set; }

        [JsonProperty("content")]
        public PartyContent Content { get; set; }

        [JsonProperty("members")]
        public IEnumerable<PartyMember> Members { get; set; }

        [JsonProperty("pending")]
        public IEnumerable<User> Pending { get; set; }
    }
}