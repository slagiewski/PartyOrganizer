using Android.OS;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Model.Party;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyOrganizer.Fragments
{
    public class PartyMembersFragment : Android.Support.V4.App.Fragment
    {
        private MemberListAdapter _adapter;
        private ListView _partyMembersListView;
        private List<PartyMember> _allPartyMembers;
        private Party _selectedParty;
        private PartyDetailActivity _context;

        public PartyMembersFragment(PartyDetailActivity context, Party party)
        {
            _context = context;
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

            _adapter = new MemberListAdapter(this, _allPartyMembers);
            _partyMembersListView = this.View.FindViewById<ListView>(Resource.Id.partyMembersListView);
            _partyMembersListView.Adapter = _adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyMembersView, container, false);
        }

        public async Task NotifyDataChanged()
        {
            await _context.Refresh();
        }

        public void Refresh(Party party)
        {
            _selectedParty = party;
            _allPartyMembers.Clear();
            foreach (var item in _selectedParty.Members)
            {
                _allPartyMembers.Add(item);
            }

            _adapter.NotifyDataSetChanged();
        }
    }
}