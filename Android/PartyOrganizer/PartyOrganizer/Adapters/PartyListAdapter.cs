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

            convertView.FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView).Text = party.Name;
            convertView.FindViewById<TextView>(Resource.Id.adminTextView).Text = party.Host?.ToString()??"App user(in progress)";

            Picasso.With(_context)
                   .Load(party.Image)
                   .Into(convertView.FindViewById<ImageView>(Resource.Id.partyImageView));

            return convertView;
        }

        public override int Count => _parties.Count;

        public override PartyLookup this[int position] => _parties[position];

        public override long GetItemId(int position) =>
            position;
    }
}