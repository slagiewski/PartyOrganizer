using PartyOrganizer.Core.Model.Interfaces;
using System.Collections.Generic;

namespace PartyOrganizer.Core
{
    public class User : IEntity<User>
    {
        #region Properties

        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Location { get; set; }

        public bool Online { get; set; }

        public string StatusImagePath
        {
            get
            {
                if (Online)
                    return "103027/1293960051.png";
                else
                    return "169058/red-point.png";
            }
        }
            
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        public IEnumerable<User> Friends { get; set; }

        #endregion

        #region Methods

        public bool Equals(User other)
        {
            if (other == null) return false;
            if (this.ID == other.ID || this.Email == other.Email || 
                (this.Name == other.Name && this.Surname == other.Surname && this.PhoneNumber == other.PhoneNumber))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }

        #endregion
    }
}
