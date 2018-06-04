using System.Collections.Generic;
using System.Linq;
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
    public class PartyPendingFragment : Android.Support.V4.App.Fragment
    {
        private PendingListAdapter _adapter;
        private ListView _partyPendingsListView;
        private List<Core.Model.Member.User> _allPartyPendings;
        private IPartyRepositoryAsync _partyRepository;
        private Party _selectedParty;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            _partyRepository = new WebPartyRepository(authLink);

            var selectedPartyID = this.Activity.Intent.Extras.GetString("selectedPartyID");
            _selectedParty = await _partyRepository.GetById(selectedPartyID);
            var receivedPendings = _selectedParty.Pending.ToList();
            if (receivedPendings != null)
                _allPartyPendings = _selectedParty.Pending.ToList();
            else
                _allPartyPendings = new List<Core.Model.Member.User>();

            _adapter = new PendingListAdapter(this.Activity, _allPartyPendings);
            _partyPendingsListView = this.View.FindViewById<ListView>(Resource.Id.partyMembersListView);
            _partyPendingsListView.Adapter = _adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyPendingView, container, false);
        }
    }
}