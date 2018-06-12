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
using System.Threading.Tasks;
using Xamarin.Facebook;

namespace PartyOrganizer.Fragments
{
    public class PartyItemsFragment : Android.Support.V4.App.Fragment
    {
        private ItemListAdapter _adapter;
        private ListView _partyItemsListView;
        private Dictionary<string, PartyItem> _allPartyItems;
        private readonly IPartyRepositoryAsync _partyRepository;
        private Party _selectedParty;
        private readonly FirebaseAuthLink _auth;
        private PartyDetailActivity _context;

        public PartyItemsFragment(PartyDetailActivity context, Party party, IPartyRepositoryAsync partyRepository, FirebaseAuthLink auth)
        {
            _context = context;
            _selectedParty = party;
            _partyRepository = partyRepository;
            _auth = auth;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _allPartyItems = _selectedParty.Content?.Items;

            if(_allPartyItems != null)
            {
                _adapter = new ItemListAdapter(this, _allPartyItems, _partyRepository, _selectedParty, _auth);
                _partyItemsListView = this.View.FindViewById<ListView>(Resource.Id.partyItemsListView);
                _partyItemsListView.Adapter = _adapter;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyItemsView, container, false);
        }

        public async Task NotifyDataChanged()
        {
            await _context.Refresh();
        }

        public void Refresh(Party party)
        {
            _selectedParty = party;
            if (_adapter != null)
            {
                _allPartyItems.Clear();
                _adapter.NotifyDataSetChanged();
                foreach (var item in _selectedParty.Content.Items ?? Enumerable.Empty<KeyValuePair<string, PartyItem>>())
                {
                    _allPartyItems.Add(item.Key, item.Value);
                }

                _adapter.NotifyDataSetChanged();
            }
            
        }
    }
}