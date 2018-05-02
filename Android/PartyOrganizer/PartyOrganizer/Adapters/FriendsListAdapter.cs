using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core;
using PartyOrganizer.Utility;

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

        public override long GetItemId(int position)
        {
            return _friends[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var friend = _friends[position];
            var avatarImageBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + friend.ImagePath + ".jpg");
            var statusImageBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + friend.StatusImagePath + ".jpg");

            if ( convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.FriendRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.nameAndSurnameTextView).Text = friend.ToString();
            convertView.FindViewById<ImageView>(Resource.Id.statusImageView).SetImageBitmap(statusImageBitmap);
            convertView.FindViewById<ImageView>(Resource.Id.friendImageView).SetImageBitmap(avatarImageBitmap);

            return convertView;
        }

        public override int Count => _friends.Count;

        public override User this[int position] => _friends[position];
    }
}