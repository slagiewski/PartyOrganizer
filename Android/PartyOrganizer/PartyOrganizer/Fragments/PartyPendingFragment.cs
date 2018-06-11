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
        private ListView _partyPendingsListView;
        private readonly IPartyRepositoryAsync _partyRepository;
        private readonly FirebaseAuthLink _authLink;
        private readonly Party _selectedParty;

        public PartyPendingFragment(Party party, IPartyRepositoryAsync partyRepository, FirebaseAuthLink authLink)
        {
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

            if (_selectedParty.Pending != null)
            {
                var adapter = new PendingListAdapter(this.Activity, _partyRepository, _selectedParty, _authLink);
                _partyPendingsListView = this.View.FindViewById<ListView>(Resource.Id.partyPendingsListView);
                _partyPendingsListView.Adapter = adapter;
            }
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyPendingView, container, false);
        }
    }
}