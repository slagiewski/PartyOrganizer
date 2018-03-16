using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer
{
    [Activity(Label = "Znajomi", MainLauncher = false)]
    public class FriendsActivity : Activity
    {
        ListView friendsListView;
        IEnumerable<User> friends;
        IUserRepository userRepository;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FriendsView);

            friendsListView = FindViewById<ListView>(Resource.Id.friendsListView);

            userRepository = new UserRepository();

            friends = userRepository.GetAll();

            friendsListView.Adapter = new FriendsListAdapter(this, friends);

        }
    }
}