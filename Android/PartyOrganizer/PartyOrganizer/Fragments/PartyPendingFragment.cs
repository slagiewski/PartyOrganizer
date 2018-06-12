using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private Party _selectedParty;
        private List<Core.Model.Member.User> _pendingUsers;
        private readonly IPartyRepositoryAsync _partyRepository;
        private PartyDetailActivity _context;
        private PendingListAdapter _adapter;
        private ListView _partyPendingsListView;
        private readonly FirebaseAuthLink _authLink;

        public PartyPendingFragment(PartyDetailActivity context, Party party, IPartyRepositoryAsync partyRepository, FirebaseAuthLink authLink)
        {
            _context = context;
            _pendingUsers = party.Pending?.ToList();
            _selectedParty = party;
            _partyRepository = partyRepository;
            _authLink = authLink;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _adapter = new PendingListAdapter(this, _pendingUsers, _partyRepository, _selectedParty, _authLink);
            _partyPendingsListView = this.View.FindViewById<ListView>(Resource.Id.partyPendingsListView);
            _partyPendingsListView.Adapter = _adapter;
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyPendingView, container, false);
        }

        public async Task NotifyDataChanged()
        {
            await _context.Refresh();
        }

        public void Refresh(Party party)
        {
            _selectedParty = party;
            _pendingUsers?.Clear();
            foreach (var item in _selectedParty.Pending ?? Enumerable.Empty<Core.Model.Member.User>())
            {
                _pendingUsers.Add(item);
            }

            _adapter?.NotifyDataSetChanged();
        }
    }
}