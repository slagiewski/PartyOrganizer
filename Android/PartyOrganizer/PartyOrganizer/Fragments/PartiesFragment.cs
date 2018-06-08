using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private PartyListAdapter _adapter;
        private ListView _partyListView;
        private List<PartyLookup> _allParties;
        private IPartyRepositoryAsync _partyRepository;

        public override void OnCreate(Bundle savedInstanceState) =>
             base.OnCreate(savedInstanceState);
        

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);

            _partyRepository = new PersistantPartyRepository(authLink);
            var receivedParties = await _partyRepository.GetPartiesByUserId();
            if (receivedParties != null)
                _allParties = receivedParties.ToList();
            else
                _allParties = new List<PartyLookup>();

            _adapter = new PartyListAdapter(this.Activity, _allParties);

            _partyListView = this.View.FindViewById<ListView>(Resource.Id.partyOrganizerListView);
            _partyListView.Adapter = _adapter;

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

            // TODO

            //_refreshButton.Click += async (s, e) =>
            //{
            //    await Refresh();
            //};
        }

        public async Task Refresh()
        {
            _allParties = (await _partyRepository.GetPartiesByUserId()).ToList();
            _adapter.NotifyDataSetChanged();
        }
    }
}