using System.Collections.Generic;
using PartyOrganizer.Core.Model;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepository : IRepository<PartyInfo>
    {
        IEnumerable<PartyInfo> GetPartiesByUser(User User);

        IEnumerable<PartyInfo> GetPartiesWithUser(User user);
    }
}