using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.CoreTests.Repository
{
    [TestClass]
    public class TempTests
    {
        private readonly IPartyRepositoryAsync repository = new PersistantPartyRepository();
        FirebaseClient _fb = new FirebaseClient("https://fir-test-420af.firebaseio.com/");

        [TestMethod]
        public async Task GetLookupPartiesAsync()
        {
            var id = "AAUdmniSRZOGxnAH0RYahSDBS2E3";
            var x = await repository.GetPartiesByUserId(id);
        }

        [TestMethod]
        public async Task GetPartyByIdAsync()
        {
            var id = "-LDXSvePZvl7t48IXBPc";

            var party = await repository.GetById(id);
                        
        }
    }
}
