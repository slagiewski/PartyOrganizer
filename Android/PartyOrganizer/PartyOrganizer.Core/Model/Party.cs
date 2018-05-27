using System;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model
{
    public partial class Party
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Image { get; set; } = "171448/cyberscooty-let-s-party-1.png";

        public LocationData Location { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> Order { get; set; }

        public int Unix { get; set; }

        public IEnumerable<Object> Members { get; set; }

    }
}