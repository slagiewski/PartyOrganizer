using Android.OS;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Model.Party;
using System.Collections.Generic;
using System.Linq;

namespace PartyOrganizer.Fragments
{
    public class PartyMembersFragment : Android.Support.V4.App.Fragment
    {
        private MemberListAdapter _adapter;
        private ListView _partyMembersListView;
        
        private List<PartyMember> _allPartyMembers;
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