using SQLite;

namespace PartyOrganizer.Core.Model.Party
{
    public class LookupParty
    {
<<<<<<< Updated upstream
        [PrimaryKey]
=======
        
>>>>>>> Stashed changes
        public string Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string Image { get; set; }
        public int Unix { get; set; }
        public string Location { get; set; }
    }
}