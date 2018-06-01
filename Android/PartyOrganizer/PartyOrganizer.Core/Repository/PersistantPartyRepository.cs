using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;
using Plugin.Connectivity;

namespace PartyOrganizer.Core.Repository
{
    public class PersistantPartyRepository : IPartyRepositoryAsync
    {
        private readonly IPartyRepositoryAsync _partyRepository;
        private readonly static string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                            "partyOrganizerDB.db");

        private static readonly DateTime _lastConnectionCheck;

        static PersistantPartyRepository()
        {
        }

        public PersistantPartyRepository()
        {
            _partyRepository = new WebPartyRepository();
        }

        public Task<int> Add(Party entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Party>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Party> GetById(string id)
        {
            //
            //var connected = await CheckConnection();


            using (var db = new LiteDatabase(_dbPath))
            {
                var partiesTable = db.GetCollection<Party>();

                if (true)
                {
                    var party = await _partyRepository.GetById(id);
                    if (party != null)
                        partiesTable.Upsert(party);
                    return party;
                }
                else
                {
                    var party = partiesTable.FindById(id);
                    return party;
                }

            }
        }

        public Task<IEnumerable<PartyLookup>> GetPartiesByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PartyLookup>> GetPartiesByUserId(string userId)
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var partieslookupTable = db.GetCollection<PartyLookup>();
                
                if (true)
                {
                    var parties = await _partyRepository.GetPartiesByUserId(userId);
                    if (parties != null)
                        partieslookupTable.Upsert(parties);
                    return parties;
                }
                else
                {
                    var parties = partieslookupTable.FindAll();
                    return parties;
                }

            }
        }

        public Task Remove(Party entity)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CheckConnection()
        {
            // If offline - check if the time difference is below [5] seconds
            // 
            //var internetStatus = await CrossConnectivity.Current.IsReachable("www.google.com", 200);

            var internetStatus = CrossConnectivity.Current.IsConnected;

            return internetStatus;
        }

        Task IRepositoryAsync<Party>.Add(Party entity)
        {
            throw new NotImplementedException();
        }
    }
}