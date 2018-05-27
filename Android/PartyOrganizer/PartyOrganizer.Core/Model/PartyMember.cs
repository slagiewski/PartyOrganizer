using System.Collections.Generic;

namespace PartyOrganizer.Core.Model
{
    public class PartyMember
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public IEnumerable<PartyItem> Items{ get; set; }

    }
}