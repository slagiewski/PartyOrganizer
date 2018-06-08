using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Xamarin.Auth;
using LiteDB;
using PartyOrganizer.Core.Model.Member;
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

        public PersistantPartyRepository(FirebaseAuthLink authLink)
        {
            _partyRepository = new WebPartyRepository(authLink);
        }

        public Task<string> Add(Party entity)
        {
            return _partyRepository.Add(entity);
        }

        public Task<IEnumerable<Party>> GetAll()
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

                if (CheckConnection())
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

        public async Task<IEnumerable<PartyLookup>> GetPartiesByUserId()
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var partieslookupTable = db.GetCollection<PartyLookup>();
                
                if (CheckConnection())
                {
                    var parties = await _partyRepository.GetPartiesByUserId();
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

        public Task<bool> Join(string partyId)
        {
            return _partyRepository.Join(partyId);
        }

        public Task<bool> AcceptRequest(Party party, Model.Member.User user)
        {
            return _partyRepository.AcceptRequest(party, user);
        }

        public Task<bool> RefuseRequest(Party party, Model.Member.User user)
        {
            return _partyRepository.RefuseRequest(party, user);
        }

        private bool CheckConnection() => CrossConnectivity.Current.IsConnected;

        public Task<Party> UpdatePartyItem(Party party, KeyValuePair<string, PartyItem> partyItem, int amountToSubstract)
        {
            return _partyRepository.UpdatePartyItem(party, partyItem, amountToSubstract);
        }
    }
}