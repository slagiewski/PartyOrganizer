using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Newtonsoft.Json;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyOrganizer.Core.Repository
{
    public class WebPartyRepository : IPartyRepositoryAsync
    {
        static private FirebaseClient _fb;

        static WebPartyRepository()
        {
            _fb = new FirebaseClient("https://fir-test-420af.firebaseio.com/");
        }

        public async Task Add(Party entity)
        {
            await _fb.Child("parties")
                .PostAsync<Party>(entity);
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
                                          .OnceAsync<RawPartyData>();

            var firebaseObjectParty = firebaseObjectParties.FirstOrDefault();

            var party = new Party
            {
                Id = firebaseObjectParty.Key,
                Content = firebaseObjectParty.Object.Content
            };

            if (firebaseObjectParty.Object.Members != null)
            {
                var members = JsonConvert.DeserializeObject<Dictionary<string, PartyMember>>(firebaseObjectParty.Object.Members.ToString());
                var membersList = new List<PartyMember>(members.Count);
                foreach (var member in members)
                {
                    member.Value.Id = member.Key;
                    membersList.Add(member.Value);
                }

                party.Members = membersList;
            }   

            if (firebaseObjectParty.Object.Pending != null)
            {
                var pendingMembers = JsonConvert.DeserializeObject<Dictionary<string, PartyMember>>(firebaseObjectParty.Object.Pending.ToString());
                var pendingList = new List<PartyMember>(pendingMembers.Count);
                foreach (var pendingMember in pendingMembers)
                {
                    pendingMember.Value.Id = pendingMember.Key;
                    pendingList.Add(pendingMember.Value);
                }

                party.Pending = pendingList;
            }
            
            return party;
        }

        public Task<IEnumerable<PartyLookup>> GetPartiesByUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PartyLookup>> GetPartiesByUserId(string userId)
        {
            var firebaseObjectParties = await _fb
                                         .Child("users")
                                         .Child(userId)
                                         .Child("partiesMeta")
                                         .OnceAsync<PartyLookup>();

            var partiesLookup = new List<PartyLookup>(firebaseObjectParties.Count);

            foreach (var party in firebaseObjectParties)
            {
                party.Object.Id = party.Key;
                partiesLookup.Add(party.Object);
            }

            return partiesLookup;
        }

        public Task Remove(Party entity)
        {
            throw new System.NotImplementedException();
        }
    }
}