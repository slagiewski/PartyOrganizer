using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Auth;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Facebook;

namespace PartyOrganizer.Fragments
{
    public class PartyMembersFragment : Android.Support.V4.App.Fragment
    {
        private readonly MemberListAdapter _adapter;
        private ListView _partyMembersListView;
        private List<PartyMember> _allPartyMembers;
        private IPartyRepositoryAsync _partyRepository;
        private readonly Party _selectedParty;

        public PartyMembersFragment(Party party)
        {
            _selectedParty = party;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            //var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            //_partyRepository = new WebPartyRepository(authLink);

            //var selectedPartyID = this.Activity.Intent.Extras.GetString("selectedPartyID");
            //_selectedParty = await _partyRepository.GetById(selectedPartyID);
            _allPartyMembers = _selectedParty.Members.ToList();

            _adapter = new MemberListAdapter(this.Activity, _allPartyMembers);
            _partyMembersListView = this.View.FindViewById<ListView>(Resource.Id.partyMembersListView);
            _partyMembersListView.Adapter = _adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyMembersView, container, false);
        }
    }
}