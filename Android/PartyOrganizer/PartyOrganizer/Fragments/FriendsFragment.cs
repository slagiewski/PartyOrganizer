using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Fragments
{
    public class FriendsFragment : Android.Support.V4.App.Fragment
    {
        private ListView _friendsListView;
        private List<User> _friends;
        private IUserRepository _userRepository;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _friendsListView = this.View.FindViewById<ListView>(Resource.Id.friendsListView);

            _userRepository = new UserRepository();

            _friends = _userRepository.GetAll().ToList();

            _friendsListView.Adapter = new FriendsListAdapter(this.Activity, _friends);

            HandleEvents();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.FriendsView, container, false);
        }

        private void HandleEvents()
        {
            _friendsListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                var party = _friends[e.Position];

                var intent = new Intent();
                intent.SetClass(this.Activity, typeof(UserDetailActivity));
                intent.PutExtra("selectedUserID", party.ID);

                StartActivityForResult(intent, 100);
            };
        }
    }
}