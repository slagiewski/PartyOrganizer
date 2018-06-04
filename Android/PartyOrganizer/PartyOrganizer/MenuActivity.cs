
using Android;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using PartyOrganizer.Adapters;
using PartyOrganizer.Fragments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyOrganizer
{
    [Activity(Label = "MenuActivity", MainLauncher = false)]
    public class MenuActivity : FragmentActivity
    {
        private PartiesFragment _partiesFragment;
        private MyProfileFragment _myProfileFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _partiesFragment = new PartiesFragment();
            _myProfileFragment = new MyProfileFragment();

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