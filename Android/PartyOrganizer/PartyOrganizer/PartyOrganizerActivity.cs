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

namespace PartyOrganizer
{
    [Activity(Label = "Party Organizer", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView partyListView;
        private List<Party> allParties;
        private IPartyRepository partyRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PartyOrganizerView);

            partyListView = FindViewById<ListView>(Resource.Id.partyOrganizerListView);

            partyRepository = new PartyRepository();

            allParties = partyRepository.GetAll().ToList();

            partyListView.Adapter = new PartyListAdapter(this, allParties);
            partyListView.ItemClick += PartyListView_ItemClick;
        }

        private void PartyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var party = allParties.ElementAt(e.Position);

            var intent = new Intent();
            intent.SetClass(this, typeof(PartyDetailActivity));
            intent.PutExtra("selectedPartyID", party.ID);

            StartActivityForResult(intent, 100);
        }
    }
}

