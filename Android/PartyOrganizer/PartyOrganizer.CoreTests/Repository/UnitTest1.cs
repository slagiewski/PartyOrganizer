using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartyOrganizer.Core;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.CoreTests.Repository
{
    [TestClass]
    public class UnitTest1
    {
        readonly IPartyRepositoryAsync repository = new WebPartyRepository();
        FirebaseClient _fb = new FirebaseClient("https://fir-test-420af.firebaseio.com/");

        [TestMethod]
        public async Task GetLookupPartiesAsync()
        {
            var id = "AAUdmniSRZOGxnAH0RYahSDBS2E3";
            var parties = await repository.GetPartiesWithUser(id);


        }

        [TestMethod]
        public async Task GetPartyByIdAsync()
        {
            var id = "-LDXSvePZvl7t48IXBPc";
            //var parties = await repository.GetById(id);

            var firebaseObjectParties = await _fb
                                         .Child("parties")
                                         .OrderByKey()
                                         .StartAt(id)
                                         .EndAt(id)
                                         .OnceAsync<Party>();

        }

        class UserResults
        {
            Dictionary<string, LookupParty> partiesMeta;
        }
    }
}
