using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
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
        private readonly FirebaseAuthLink _auth;

        static WebPartyRepository()
        {
            _fb = new FirebaseClient("https://fir-testwithauth.firebaseio.com/");
        }

        public WebPartyRepository(FirebaseAuthLink authLink)
        {
            _auth = authLink;
        }

        public async Task<string> Add(Party party)
        {
            try
            {
                var firebaseObjectNewParty = await _fb
                                                   .Child("parties")
                                                   .PostAsync<Party>(new Party
                                                   {
                                                       Content = party.Content
                                                   });

                
                if (firebaseObjectNewParty.Key != null)
                {
                    await AddHost(firebaseObjectNewParty);

                    await AddPartyMetaData(party, firebaseObjectNewParty);
                }

                return firebaseObjectNewParty.Key;
            }
            catch
            {
                return null;
            }
            
        }

        private async Task AddPartyMetaData(Party party, FirebaseObject<Party> firebaseObjectNewParty)
        {
            await _fb
                  .Child("users")
                  .Child(_auth.User.LocalId)
                  .Child("partiesMeta")
                  .Child(firebaseObjectNewParty.Key)
                  .PutAsync<PartyLookup>(new PartyLookup
                  {
                      Name = party.Content.Name,
                      Host = _auth.User.DisplayName,
                      Image = _auth.User.PhotoUrl,
                      Unix = party.Content.Unix,
                      Location = party.Content.Location.Name
                  });
        }

        private async Task AddHost(FirebaseObject<Party> firebaseObjectNewParty)
        {
            await _fb
                  .Child("parties")
                  .Child(firebaseObjectNewParty.Key)
                  .Child("members")
                  .Child(_auth.User.LocalId)
                  .PutAsync(new PartyMember
                  {
                      Name = _auth.User.DisplayName,
                      Type = "host",
                      Image = _auth.User.PhotoUrl
                  });
        }

        public Task<IEnumerable<Party>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Party> GetById(string id)
        {

            //check if the person is a party member
            try
            {
                var firebaseObjectParties = await _fb
                                              .Child("parties")
                                              .OrderByKey()
                                              .StartAt(id)
                                              .EndAt(id)
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
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<PartyLookup>> GetPartiesByUserId()
        {
            try
            {
                var firebaseObjectParties = await _fb
                                                  .Child("users")
                                                  .Child(_auth.User.LocalId)
                                                  .Child("partiesMeta")
                                                  .OnceAsync<PartyLookup>();

                if (firebaseObjectParties.Count <= 0)
                    return null;

                var lookupParties = new List<PartyLookup>(firebaseObjectParties.Count);

                foreach (var party in firebaseObjectParties)
                {
                    party.Object.Id = party.Key;
                    lookupParties.Add(party.Object);
                }

                return lookupParties;
            }
            catch
            { 
                return null;
            }
            
        }
        
        public async Task<bool> Join(string partyId)
        {
            try
            {
                var party = await this.GetById(partyId);

                if (party != null)
                {
                    await _fb
                          .Child("parties")
                          .Child(partyId)
                          .Child("pending")
                          .Child(_auth.User.LocalId)
                          .PutAsync<UserBase>(new UserBase
                          {
                              Name = _auth.User.DisplayName,
                              Image = _auth.User.PhotoUrl
                          });

                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> AcceptRequest(Party party, Model.Member.User user)
        {
            try
            {
                var newPartyMember = new PartyMember
                {
                    Id = user.Id,
                    Name = user.Name,
                    Image = user.Image,
                    Items = null
                };

                await MoveFromPending(party, newPartyMember);

                await AddToLookup(party, newPartyMember);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Task Remove(Party entity)
        {
            throw new System.NotImplementedException();
        }

        private async Task AddToLookup(Party party, PartyMember newPartyMember)
        {
            await _fb
                  .Child("users")
                  .Child(party.Id)
                  .Child("partiesMeta")
                  .Child(newPartyMember.Id)
                  .PutAsync<PartyLookup>(new PartyLookup
                  {
                      Name = party.Content.Name,
                      Host = _auth.User.DisplayName,
                      Image = _auth.User.PhotoUrl,
                      Unix = party.Content.Unix,
                      Location = party.Content.Location.Name
                  });
        }

        private async Task MoveFromPending(Party party, PartyMember partyMember)
        {
            await _fb
                  .Child("parties")
                  .Child(party.Id)
                  .Child("members")
                  .Child(partyMember.Id)
                  .PutAsync<PartyMember>(new PartyMember
                  {
                      Name = partyMember.Name,
                      Image = partyMember.Image,
                      Items = null
                  });

            await _fb
                  .Child("parties")
                  .Child(party.Id)
                  .Child("pending")
                  .Child(partyMember.Id)
                  .DeleteAsync();
        }
    }
}