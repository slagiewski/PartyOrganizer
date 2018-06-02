using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using PartyOrganizer.Core.Model.Member;
using PartyOrganizer.Core.Model.Party;
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
                                          .OnceAsync<RawPartyData>();

            var firebaseObjectParty = firebaseObjectParties.FirstOrDefault();
            var rawPartyData = firebaseObjectParty?.Object;
            var partyId = firebaseObjectParty?.Key;

            var party = rawPartyData?.ToParty(partyId);
            return party;
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
        
        public async Task<bool> Join(string partyId, User user)
        {

            var party = await this.GetById(partyId);

            if (party != null)
            {
                await _fb
                      .Child("parties")
                      .Child(partyId)
                      .Child("pending")
                      .Child(user.Id)
                      .PutAsync<UserBase>(user);
                return true;
            }
            else
                return false;
        }

        public async Task AcceptRequest(string partyId, User user)
        {
            var newPartyMember = new PartyMember
            {
                Id = user.Id,
                Name = user.Name,
                Image = user.Image,
                Items = new List<PartyItem>()
            };


            await MoveFromPending(partyId, newPartyMember);

            await AddToLookup(partyId, newPartyMember);
        }

        public Task Remove(Party entity)
        {
            throw new System.NotImplementedException();
        }

        private async Task AddToLookup(string partyId, PartyMember newPartyMember)
        {
            await _fb
                  .Child("users")
                  .Child(partyId)
                  .Child("partiesMeta")
                  .Child(newPartyMember.Id)
                  .PutAsync<PartyMember>(new PartyMember
                  {
                      Name = newPartyMember.Name,
                      Image = newPartyMember.Image,
                      Items = new List<PartyItem>()
                  });
        }

        private async Task MoveFromPending(string partyId, PartyMember partyMember)
        {
            await _fb
                  .Child("parties")
                  .Child(partyId)
                  .Child("members")
                  .Child(partyMember.Id)
                  .PutAsync<PartyMember>(new PartyMember
                  {
                      Name = partyMember.Name,
                      Image = partyMember.Image,
                      Items = new List<PartyItem>()
                  });

            await _fb
                  .Child("parties")
                  .Child(partyId)
                  .Child("pending")
                  .Child(partyMember.Id)
                  .DeleteAsync();
        }
    }
}