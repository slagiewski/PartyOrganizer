using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;
using Plugin.Connectivity;
using SQLite;

namespace PartyOrganizer.Core.Repository
{
    public class PersistantPartyRepository : IPartyRepositoryAsync
    {
        private readonly IPartyRepositoryAsync _partyRepository;
        private readonly static string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                            "partyOrganizerDB");
        private readonly static SQLiteConnection _db;

        private static readonly DateTime _lastConnectionCheck;

        static PersistantPartyRepository()
        {
            _db = new SQLiteConnection(_dbPath);
            _db.CreateTable<Party>();
            _db.CreateTable<LookupParty>();
            _lastConnectionCheck = DateTime.Now;
        }

        public PersistantPartyRepository()
        {
            _partyRepository = new WebPartyRepository();
        }

        public Task<int> Add(Party entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Party>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Party> GetById(string ID)
        {
            var connected = await CheckConnection();

            if (connected == true)
            {
                var party = await _partyRepository.GetById(ID);
                if (party != null)
                    _db.InsertOrReplace(party);
                return party;
            }
            else
            {
                var party = _db.Get<Party>(p => p.Id == ID);
                return party;
            }
            
        }

        public Task<IEnumerable<LookupParty>> GetPartiesByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LookupParty>> GetPartiesWithUser(string userId)
        {
            throw new NotImplementedException();
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
    }
}