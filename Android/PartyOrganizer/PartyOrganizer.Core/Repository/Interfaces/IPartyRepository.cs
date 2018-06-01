using System.Collections.Generic;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepository : IRepository<Party>
    {
        IEnumerable<Party> GetPartiesByUser(User User);

        IEnumerable<Party> GetPartiesWithUser(User user);
    }
}