using Android.App;
using Android.Widget;
using Android.OS;
using PartyOrganizer.Core.Model;
using System.Collections.Generic;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Repository.Interfaces;
using System.Linq;
using Android.Content;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer
{
    [Activity(Label = "Party Organizer", MainLauncher = false)]
    public class PartiesActivity : Activity
    {
        private ListView _partyListView;
        private List<PartyLookup> _allParties;
        private IPartyRepositoryAsync _partyRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PartyOrganizerView);

            _partyListView = FindViewById<ListView>(Resource.Id.partyOrganizerListView);

            _partyRepository = new WebPartyRepository();

            _allParties = _partyRepository.GetPartiesByUserId("Swue0CeusPTA1adRHD4Qk9Bbsxf1").Result.ToList(); 

            _partyListView.Adapter = new PartyListAdapter(this, _allParties);

            HandleEvents();
        }

        private void HandleEvents()
        {
            _partyListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var party = _allParties[e.Position];

                var intent = new Intent();
                intent.SetClass(this, typeof(PartyDetailActivity));
                intent.PutExtra("selectedPartyID", party.Id);

                StartActivityForResult(intent, 100);
            };          
        }
    }
}

