using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer
{
    [Activity(Label = "FriendsActivity", MainLauncher = false)]
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