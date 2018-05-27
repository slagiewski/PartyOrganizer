using System.Collections.Generic;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepository : IRepository<PartyInfo>
    {
        IEnumerable<PartyInfo> GetPartiesByUser(User User);

        IEnumerable<PartyInfo> GetPartiesWithUser(User user);
    }
}