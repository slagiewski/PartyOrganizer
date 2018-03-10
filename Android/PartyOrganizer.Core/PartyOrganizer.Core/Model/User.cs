using PartyOrganizer.Core.Model.Interfaces;
using System.Collections.Generic;

namespace PartyOrganizer.Core
{
    public class User : IEntity<User>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool Online { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<User> Friends { get; set; }

        public bool Equals(User other)
        {
            if (other == null) return false;
            if (this.ID == other.ID || this.Email == other.Email || (this.Name == other.Name && this.Surname == other.Surname && this.PhoneNumber == other.PhoneNumber))
                return true;
            else
                return false;
        }
    }
}
