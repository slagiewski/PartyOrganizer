using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyContent
    {
        public string Description { get; set; }

        public string Image { get; set; } = "171448/cyberscooty-let-s-party-1.png";

        public IEnumerable<PartyItem> Items { get; set; }

        public LocationData Location { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> Order { get; set; }

        public int Unix { get; set; }
    }
}