using System.Collections.Generic;
using System.Linq;
using Android.App;
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

        public PartyListAdapter(Activity context, List<Party> parties)
        {
            _context = context;
            _parties = parties;
        }

        public override long GetItemId(int position)
        {
            return _parties[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var party = _parties[position];

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + party.ImagePath + ".jpg");

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView).Text = party.ShortDescription;
            convertView.FindViewById<TextView>(Resource.Id.dateTextView).Text = party.Date.ToString("dd/MM/yyyy hh:mm");
            convertView.FindViewById<TextView>(Resource.Id.adminTextView).Text = party.Admin.ToString();
            convertView.FindViewById<ImageView>(Resource.Id.partyImageView).SetImageBitmap(imageBitmap);
            
            return convertView;
        }

        public override int Count => _parties.Count;

        public override Party this[int position] => _parties[position];
    
    }
}