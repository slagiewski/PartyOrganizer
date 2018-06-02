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
    [Activity(Label = "Friends", MainLauncher = false)]
    public class FriendsActivity : Activity
    {
        //private ListView _friendsListView;
        //private List<User> _friends;
        //private IUserRepository _userRepository;
        
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    SetContentView(Resource.Layout.FriendsView);

        //    _friendsListView = FindViewById<ListView>(Resource.Id.friendsListView);

        //    _userRepository = new UserRepository();

        //    _friends = _userRepository.GetAll().ToList();

        //    _friendsListView.Adapter = new FriendsListAdapter(this, _friends);

        //    HandleEvents();

        //}

        //private void HandleEvents()
        //{
        //    _friendsListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
        //    {
        //        var party = _friends[e.Position];

        //        var intent = new Intent();
        //        intent.SetClass(this, typeof(UserDetailActivity));
        //        intent.PutExtra("selectedUserID", party.ID);

        //        StartActivityForResult(intent, 100);
        //    }; 
        //}
    }
}