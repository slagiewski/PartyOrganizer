using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model.Party;
using Square.Picasso;

namespace PartyOrganizer.Adapters
{
    class PartyListAdapter : BaseAdapter<PartyLookup>
    {
        private List<PartyLookup> _parties;
        private Activity _context;

        public PartyListAdapter(Activity context, List<PartyLookup> parties)
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

            FindViews(convertView, out TextView partyDescription, out TextView hostName, out ImageView partyImage);

            BindData(convertView, party, partyDescription, hostName, partyImage);

            return convertView;
        }

        private void BindData(View convertView, PartyLookup party, TextView partyDescription, TextView hostName, ImageView partyImage)
        {
            partyDescription.Text = party?.Name;
            hostName.Text = party.Host?.ToString() ?? "App user(in progress)";
            Picasso.With(_context)
                   .Load(party?.Image)
                   .Into(partyImage);
        }

        private static void FindViews(View convertView, out TextView partyDescription, out TextView hostName, out ImageView imageView)
        {
            partyDescription = convertView.FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView);
            hostName = convertView.FindViewById<TextView>(Resource.Id.adminTextView);
            imageView = convertView.FindViewById<ImageView>(Resource.Id.partyImageView);
        }

        public override int Count => _parties.Count;

        public override PartyLookup this[int position] => _parties[position];

        public override long GetItemId(int position) =>
            position;
    }
}