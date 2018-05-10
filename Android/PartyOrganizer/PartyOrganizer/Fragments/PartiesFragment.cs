using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Fragments
{
    public class PartiesFragment : Android.Support.V4.App.Fragment
    {
        private ListView _partyListView;
        private List<Party> _allParties;
        private IPartyRepository _partyRepository;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _partyListView = this.View.FindViewById<ListView>(Resource.Id.partyOrganizerListView);

            _partyRepository = new PartyRepository();

            _allParties = _partyRepository.GetAll().ToList();

            _partyListView.Adapter = new PartyListAdapter(this.Activity, _allParties);

            HandleEvents();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.PartyOrganizerView, container, false);
        }

        private void HandleEvents()
        {
            _partyListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var party = _allParties[e.Position];

                var intent = new Intent();
                intent.SetClass(this.Activity, typeof(PartyDetailActivity));
                intent.PutExtra("selectedPartyID", party.ID);

                StartActivityForResult(intent, 100);
            };
        }
    }
}