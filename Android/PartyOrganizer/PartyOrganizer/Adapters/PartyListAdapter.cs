using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Utility;
using Square.Picasso;

namespace PartyOrganizer.Adapters
{
    class PartyListAdapter : BaseAdapter<PartyInfo>
    {
        private List<PartyInfo> _parties;
        private Activity _context;

        public PartyListAdapter(Activity context, List<PartyInfo> parties)
        {
            _context = context;
            _parties = parties;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var party = _parties[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView).Text = party.Name;
            convertView.FindViewById<TextView>(Resource.Id.dateTextView).Text = party.Date.ToString("dd/MM/yyyy hh:mm");
            convertView.FindViewById<TextView>(Resource.Id.adminTextView).Text = party.Admin?.ToString()??"App user(in progress)";

            Picasso.With(_context)
                   .Load("https://openclipart.org/image/800px/svg_to_png/" + party.Image)
                   .Into(convertView.FindViewById<ImageView>(Resource.Id.partyImageView));

            //using (var imageBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + party.ImagePath))
            //{
            //    convertView.FindViewById<ImageView>(Resource.Id.partyImageView).SetImageBitmap(imageBitmap);
            //}

            return convertView;
        }

        public override int Count => _parties.Count;

        public override PartyInfo this[int position] => _parties[position];

        public override long GetItemId(int position) =>
            _parties[position].ID;
    }
}