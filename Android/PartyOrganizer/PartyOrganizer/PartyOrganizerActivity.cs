using Android.App;
using Android.Widget;
using Android.OS;
using PartyOrganizer.Core.Model;
using System.Collections.Generic;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Adapters;

namespace PartyOrganizer
{
    [Activity(Label = "PartyOrganizer", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView partyListView;
        private IEnumerable<Party> allParties;
        private PartyRepository partyRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.PartyOrganizerView);

            partyListView = FindViewById<ListView>(Resource.Id.PartyOrganizerListView);

            partyRepository = new PartyRepository();

            allParties = partyRepository.GetAll();

            partyListView.Adapter = new PartyListAdapter(this, allParties);

            //hotDogListView.FastScrollEnabled = true;

        }
    }
}

