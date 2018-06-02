using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyOrganizer.Core.Model.Member;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.CoreTests.Repository
{
    [TestClass]
    public class TempTests
    {
        readonly IPartyRepositoryAsync repository = new WebPartyRepository();
        readonly FirebaseClient _fb = new FirebaseClient("https://fir-test-420af.firebaseio.com/");

        [TestMethod]
        public async Task GetLookupPartiesAsync()
        {
            var id = "amXgDFj6WcOQkffgtN3pOJupXEz2";
            var parties = await repository.GetPartiesByUserId(id);


        }

        [TestMethod]
        public async Task GetPartyByIdAsync()
        {
            var id = "-LDYmodu8aySj6HK9H0n";

            var party = await repository.GetById(id);
                        
        }

        [TestMethod]
        public async Task JoinParty()
        {
            var partyId = "-LDXoHHc18uqBQd1pi_Z";
            var user = new User
            {
                Id = "AAUdmniSRZOGxnAH0RYahSDBS2E3",
                Image = "https://graph.facebook.com/1027942604038688/picture",
                Name = "Jakub"

            };

            var result = await repository.Join(partyId, user);
        }

        [TestMethod]
        public async Task AcceptRequest()
        {
            var partyId = "-LDXoHHc18uqBQd1pi_Z";
            var user = new User
            {
                Id = "AAUdmniSRZOGxnAH0RYahSDBS2E3",
                Image = "https://graph.facebook.com/1027942604038688/picture",
                Name = "Jakub"

            };

            await repository.AcceptRequest(partyId, user);
        }
    }
}
