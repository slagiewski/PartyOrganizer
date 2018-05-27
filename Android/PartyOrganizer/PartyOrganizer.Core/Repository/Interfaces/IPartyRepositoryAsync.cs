using System.Collections.Generic;
using System.Threading.Tasks;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepositoryAsync : IRepositoryAsync<Party>
    {

        Task<IEnumerable<LookupParty>> GetPartiesByUser(string userId);

        Task<IEnumerable<LookupParty>> GetPartiesWithUser(string userId);

    }
}