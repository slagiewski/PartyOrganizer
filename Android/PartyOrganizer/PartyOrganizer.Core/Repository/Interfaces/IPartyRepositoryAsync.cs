using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PartyOrganizer.Core.Model.Member;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepositoryAsync : IRepositoryAsync<Party>
    {
        Task<IEnumerable<PartyLookup>> GetPartiesByUserId();

        Task<bool> Join(string partyId);

        Task<bool> AcceptRequest(Party party, User user);

        Task<bool> RefuseRequest(Party party, User user);

        Task<Party> UpdatePartyItem(Party party, KeyValuePair<string, PartyItem> partyItem, int amountToSubstract);
    }
}