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
        readonly IPartyRepositoryAsync repository = new WebPartyRepository(null);

        [TestMethod]
        public async Task GetLookupPartiesAsync()
        {
            var parties = await repository.GetPartiesByUserId();


        }

        [TestMethod]
        public async Task GetPartyByIdAsync()
        {
            var id = "-LDXoHHc18uqBQd1pi_Z";

            var party = await repository.GetById(id);
                        
        }

        //[TestMethod]
        //public async Task JoinParty()
        //{
        ////    var partyId = "-LDXoHHc18uqBQd1pi_Z";
        ////    var user = new User
        ////    {
        ////        Id = "AAUdmniSRZOGxnAH0RYahSDBS2E3",
        ////        Image = "https://graph.facebook.com/1027942604038688/picture",
        ////        Name = "Jakub"

        ////    };

        ////    var result = await repository.Join(partyId, user);
        //}

        //[TestMethod]
        //public async Task AcceptRequest()
        //{
        //    //var partyId = "-LDXoHHc18uqBQd1pi_Z";
        //    //var user = new User
        //    //{
        //    //    Id = "AAUdmniSRZOGxnAH0RYahSDBS2E3",
        //    //    Image = "https://graph.facebook.com/1027942604038688/picture",
        //    //    Name = "Jakub"

        //    //};

        //    //await repository.AcceptRequest(user);
        //}
    }
}
