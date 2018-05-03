using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Utility;


namespace PartyOrganizer.Adapters
{
    class PartyListAdapter : BaseAdapter<Party>
    {
        private List<Party> _parties;
        private Activity _context;

        private static readonly string _defaultImagePath = "171448/cyberscooty-let-s-party-1.png";
        private static Bitmap _defaultPartyImage;

        static PartyListAdapter()
        {
            _defaultImagePath = "171448/cyberscooty-let-s-party-1.png";
            _defaultPartyImage = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + _defaultImagePath);
        }

        public PartyListAdapter(Activity context, List<Party> parties)
        {
            _context = context;
            _parties = parties;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var party = _parties[position];

            var imageBitmap = party.ImagePath == _defaultImagePath
                            ? _defaultPartyImage
                            : ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + party.ImagePath);

            //var imageBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + party.ImagePath);

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView).Text = party.Name;
            convertView.FindViewById<TextView>(Resource.Id.dateTextView).Text = party.Date.ToString("dd/MM/yyyy hh:mm");
            convertView.FindViewById<TextView>(Resource.Id.adminTextView).Text = party.Admin?.ToString()??"App user(in progress)";
            convertView.FindViewById<ImageView>(Resource.Id.partyImageView).SetImageBitmap(imageBitmap);

            return convertView;
        }

        public override int Count => _parties.Count;

        public override Party this[int position] => _parties[position];

        public override long GetItemId(int position) =>
            _parties[position].ID;
    }
}