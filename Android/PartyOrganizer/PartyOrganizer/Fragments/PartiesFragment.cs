using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Auth;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using Xamarin.Facebook;

namespace PartyOrganizer.Fragments
{
    public class PartiesFragment : Android.Support.V4.App.Fragment
    {
        private ListView _partyListView;
        private List<PartyLookup> _allParties;
        private IPartyRepositoryAsync _partyRepository;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _partyListView = this.View.FindViewById<ListView>(Resource.Id.partyOrganizerListView);

            var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);

            _partyRepository = new WebPartyRepository(authLink);

            _allParties = (await _partyRepository.GetPartiesByUserId()).ToList();

            _partyListView.Adapter = new PartyListAdapter(this.Activity, _allParties);

            HandleEvents();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyOrganizerView, container, false);
        }

        private void HandleEvents()
        {
            _partyListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var party = _allParties[e.Position];

                var intent = new Intent();
                intent.SetClass(this.Activity, typeof(PartyDetailActivity));
                intent.PutExtra("selectedPartyID", party.Id);

                StartActivityForResult(intent, 100);
            };
        }
    }
}