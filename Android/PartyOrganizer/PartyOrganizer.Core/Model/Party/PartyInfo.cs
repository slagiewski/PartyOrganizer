using System;
using System.Collections.Generic;
using PartyOrganizer.Core.Model.Interfaces;

namespace PartyOrganizer.Core.Model.Party
{
    public class PartyInfo : IEntity<PartyInfo>
    {
        #region Properties
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public User Admin { get; set; }

        public IEnumerable<User> Participants { get; set; }

        public string Image { get; set; } = "171448/cyberscooty-let-s-party-1.png";

        public DateTime Date { get; set; } = DateTime.Now;
        #endregion

        #region Methods

        public bool Equals(PartyInfo other)
        {
            if (other == null) return false;
            if (other.ID == this.ID || String.Equals(other.Name, Name,StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }

        #endregion

    }
}