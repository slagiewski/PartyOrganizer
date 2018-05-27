

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Core.Repository
{
    public class WebPartyRepository : IPartyRepositoryAsync
    {
        static private FirebaseClient _fb;

        static WebPartyRepository()
        {
            _fb = new FirebaseClient("https://fir-test-420af.firebaseio.com/");
        }

        public Task<int> Add(Party entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Party>> GetAll()
        {
            throw new System.NotImplementedException();
           
        }

        public async Task<Party> GetById(string id)
        {
            var firebaseObjectParties = await _fb
                                         .Child("parties")
                                         .OrderByKey()
                                         .StartAt(id)
                                         .EndAt(id)
                                         .OnceAsync<Party>();

            var party = firebaseObjectParties.FirstOrDefault();
            party.Object.Id = party.Key;

            return party.Object;
        }

        public Task<IEnumerable<LookupParty>> GetPartiesByUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<LookupParty>> GetPartiesWithUser(string userId)
        {
            var firebaseObjectParties = await _fb
                                         .Child("users")
                                         .Child(userId)
                                         .Child("partiesMeta")
                                         .OnceAsync<LookupParty>();

            var lookupParties = new List<LookupParty>(firebaseObjectParties.Count);

            foreach (var party in firebaseObjectParties)
            {
                party.Object.Id = party.Key;
                lookupParties.Add(party.Object);
            }
            
            return lookupParties;
        }

        public Task Remove(Party entity)
        {
            throw new System.NotImplementedException();
        }
    }
}