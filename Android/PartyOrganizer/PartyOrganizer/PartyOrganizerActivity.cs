using Android.App;
using Android.Widget;
using Android.OS;
using PartyOrganizer.Core.Model;
using System.Collections.Generic;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer
{
    [Activity(Label = "PartyOrganizer", MainLauncher = false)]
    public class MainActivity : Activity
    {
        private ListView partyListView;
        private IEnumerable<Party> allParties;
        private IPartyRepository partyRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PartyOrganizerView);

            partyListView = FindViewById<ListView>(Resource.Id.partyOrganizerListView);

            partyRepository = new PartyRepository();

            allParties = partyRepository.GetAll();

            partyListView.Adapter = new PartyListAdapter(this, allParties);

        }
    }
}

