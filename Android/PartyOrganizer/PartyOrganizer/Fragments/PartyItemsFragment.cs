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
    public class PartyItemsFragment : Android.Support.V4.App.Fragment
    {
        private ItemListAdapter _adapter;
        private ListView _partyItemsListView;
        private Dictionary<string, PartyItem> _allPartyItems;
        private IPartyRepositoryAsync _partyRepository;
        private Party _selectedParty;
        private FirebaseAuthLink _auth;

        public PartyItemsFragment(Party party, IPartyRepositoryAsync partyRepository, FirebaseAuthLink auth)
        {
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
                _adapter = new ItemListAdapter(this.Activity, _allPartyItems, _partyRepository, _selectedParty, _auth);
                _partyItemsListView = this.View.FindViewById<ListView>(Resource.Id.partyItemsListView);
                _partyItemsListView.Adapter = _adapter;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyItemsView, container, false);
        }
    }
}