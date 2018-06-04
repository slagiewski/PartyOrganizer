using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Auth;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using PartyOrganizer.Fragments;
using Xamarin.Facebook;
using System.Collections.Generic;

namespace PartyOrganizer
{
    [Activity(Label = "Party", MainLauncher = false)]
    public class PartyDetailActivity : FragmentActivity
    {
        private PartyInfoFragment _partyInfoFragment;
        private PartyItemsFragment _partyItemsFragment;
        private PartyMembersFragment _partyMembersFragment;
        private PartyPendingFragment _partyPendingFragment;
        private IPartyRepositoryAsync _partyRepository;      

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PartyDetailView);

            _partyInfoFragment = new PartyInfoFragment();
            _partyItemsFragment = new PartyItemsFragment();
            _partyMembersFragment = new PartyMembersFragment();
            _partyPendingFragment = new PartyPendingFragment();

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new ViewPagerFragmentsAdapter(SupportFragmentManager,
                new List<Android.Support.V4.App.Fragment>
                {
                    _partyInfoFragment,
                    _partyItemsFragment,
                    _partyMembersFragment,
                    _partyPendingFragment
                });
            var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            _partyRepository = new WebPartyRepository(authLink);
            var selectedPartyID = Intent.Extras.GetString("selectedPartyID");
            //_selectedParty = await _partyRepository.GetById(selectedPartyID);

        }
    }
}