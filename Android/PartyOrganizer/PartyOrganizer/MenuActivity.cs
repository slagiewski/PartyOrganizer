
using Android;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Auth;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Fragments;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Facebook;

namespace PartyOrganizer
{
    [Activity(Label = "MenuActivity", MainLauncher = false)]
    public class MenuActivity : FragmentActivity
    {
        private PartiesFragment _partiesFragment;
        private MyProfileFragment _myProfileFragment;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var _authLink = await FirebaseAuthLinkWrapper.GetAuthLink(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            var _partyRepository = new PersistantPartyRepository(_authLink);

            _partiesFragment = new PartiesFragment(_partyRepository);
            _myProfileFragment = new MyProfileFragment(_partyRepository);

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new ViewPagerFragmentsAdapter(SupportFragmentManager,
                new List<Android.Support.V4.App.Fragment>
                {
                    _partiesFragment,
                    _myProfileFragment
                });            
        }
        
        public async Task Refresh()
        {
            await _partiesFragment.Refresh();
        }
    }
}