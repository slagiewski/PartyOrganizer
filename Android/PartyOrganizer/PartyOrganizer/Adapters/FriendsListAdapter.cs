using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core;
using PartyOrganizer.Utility;
using Square.Picasso;

namespace PartyOrganizer.Adapters
{
    class FriendsListAdapter : BaseAdapter<User>
    {
        private List<User> _friends;
        private Activity _context;

        public FriendsListAdapter(Activity context, List<User> friends) : base()
        {
            _context = context;
            _friends = friends;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var friend = _friends[position];

            if ( convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.FriendRowView, null);
            }

            //TODO: use Picasso to load images async.

            Picasso.With(_context)
                   .Load("https://openclipart.org/image/800px/svg_to_png/" + friend.StatusImagePath)
                   .Into(convertView.FindViewById<ImageView>(Resource.Id.statusImageView));

            Picasso.With(_context)
                   .Load("https://openclipart.org/image/800px/svg_to_png/" + friend.Image)
                   .Into(convertView.FindViewById<ImageView>(Resource.Id.friendImageView));
            
            convertView.FindViewById<TextView>(Resource.Id.nameAndSurnameTextView).Text = friend.ToString();
            
            return convertView;
        }

        public override int Count => _friends.Count;

        public override User this[int position] => _friends[position];

        public override long GetItemId(int position)
        {
            return _friends[position].ID;
        }
    }
}