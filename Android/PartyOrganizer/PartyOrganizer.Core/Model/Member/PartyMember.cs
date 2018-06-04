using PartyOrganizer.Core.Model.Member;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyMember : User
    {
        public string Type { get; set; } = "guest";

        public IEnumerable<PartyItem> Items{ get; set; }
        
    }
}