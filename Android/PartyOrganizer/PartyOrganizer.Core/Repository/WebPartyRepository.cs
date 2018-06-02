using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Xamarin.Facebook;
using Firebase.Xamarin.Database.Query;
using PartyOrganizer.Core.Model.Member;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;
using Firebase.Xamarin.Auth;

namespace PartyOrganizer.Core.Repository
{
    public class WebPartyRepository : IPartyRepositoryAsync
    {
        static private FirebaseClient _fb;
        static private FirebaseAuthLink _auth;

        static WebPartyRepository()
        {
            _fb = new FirebaseClient("https://fir-test-420af.firebaseio.com/");
        }

        private static async Task Authorize()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(""));
            _auth = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
        }

        public async Task Add(Party entity)
        {
            await Authorize();
            await _fb
                  .Child("parties")
                  .WithAuth(_auth.FirebaseToken)
                  .PostAsync<Party>(entity);
        }

        public Task<IEnumerable<Party>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Party> GetById(string id)
        {
            //await Authorize();
            var firebaseObjectParties = await _fb
                                              .Child("parties")
                                              .OrderByKey()
                                              .StartAt(id)
                                              .EndAt(id)
                                              .WithAuth(_auth.FirebaseToken)
                                              .OnceAsync<RawPartyData>();

            if (firebaseObjectParties.Count == 0)
            {
                return null;
            }

            var firebaseObjectParty = firebaseObjectParties.FirstOrDefault();
            var rawPartyData = firebaseObjectParty.Object;
            var partyId = firebaseObjectParty.Key;

            var party = rawPartyData.ToParty(partyId);
            return party;
        }

        public async Task<IEnumerable<PartyLookup>> GetPartiesByUserId(string userId)
        {
            var firebaseObjectParties = await _fb
                                              .Child("users")
                                              .Child(userId)
                                              .Child("partiesMeta")
                                              .OnceAsync<PartyLookup>();

            var lookupParties = new List<PartyLookup>(firebaseObjectParties.Count);

            foreach (var party in firebaseObjectParties)
            {
                party.Object.Id = party.Key;
                lookupParties.Add(party.Object);
            }
            
            return lookupParties;
        }
        
        public async Task<bool> Join(string partyId, Model.Member.User user)
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

        public async Task AcceptRequest(string partyId, Model.Member.User user)
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