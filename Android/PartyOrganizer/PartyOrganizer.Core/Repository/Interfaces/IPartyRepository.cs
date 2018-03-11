using System.Collections.Generic;
using PartyOrganizer.Core.Model;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepository : IRepository<Party>
    {
        IEnumerable<Party> GetPartiesOrganizedByUser(User User);

        IEnumerable<Party> GetPartiesUserParticipateIn(User user);

    }
}