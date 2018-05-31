using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepositoryAsync : IRepositoryAsync<Party>
    {

        Task<IEnumerable<PartyLookup>> GetPartiesByUser(string userId);

        Task<IEnumerable<PartyLookup>> GetPartiesByUserId(string userId);

    }
}