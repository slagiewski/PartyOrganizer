
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using PartyOrganizer.Fragments;

namespace PartyOrganizer
{
    [Activity(Label = "MenuActivity", MainLauncher = true)]
    public class MenuActivity : AppCompatActivity
    {
        private Fragment _partiesFragment;
        private Fragment _friendsFragment;
        private Fragment _currentFragment;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            //SupportActionBar.Title = "Party Organizer";
            var fragmentTransaction = this.FragmentManager.BeginTransaction();
            _partiesFragment = new PartiesFragment();
            _friendsFragment = new FriendsFragment();

            fragmentTransaction.Add(Resource.Id.fragmentContainer, _partiesFragment);
            fragmentTransaction.Hide(_partiesFragment);
            fragmentTransaction.Add(Resource.Id.fragmentContainer, _friendsFragment);
            fragmentTransaction.Commit();
            _currentFragment = _friendsFragment;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ToolbarMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.friends:
                    ShowFragment(_friendsFragment);
                    break;
                case Resource.Id.parties:
                    ShowFragment(_partiesFragment);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void ShowFragment(Fragment fragment)
        {
            var fragmentTransaction = this.FragmentManager.BeginTransaction();
            fragmentTransaction.Hide(_currentFragment);
            fragmentTransaction.Show(fragment);
            fragmentTransaction.AddToBackStack(null);
            fragmentTransaction.Commit();
            _currentFragment = fragment;
        }
    }
}