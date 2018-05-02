using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer
{
    [Activity(Label = "Znajomi", MainLauncher = true)]
    public class FriendsActivity : Activity
    {
        ListView friendsListView;
        List<User> friends;
        IUserRepository userRepository;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FriendsView);

            friendsListView = FindViewById<ListView>(Resource.Id.friendsListView);

            userRepository = new UserRepository();

            friends = userRepository.GetAll().ToList();

            friendsListView.Adapter = new FriendsListAdapter(this, friends);

            friendsListView.ItemClick += PartyListView_ItemClick;

        }

        private void PartyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var party = friends[e.Position];

            var intent = new Intent();
            intent.SetClass(this, typeof(UserDetailActivity));
            intent.PutExtra("selectedUserID", party.ID);

            StartActivityForResult(intent, 100);
        }
    }
}