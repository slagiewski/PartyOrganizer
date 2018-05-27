using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyMember
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } = "guest";
        public string Image { get; set; }
        public IEnumerable<PartyItem> Items{ get; set; }

    }
}