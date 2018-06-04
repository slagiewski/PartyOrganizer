using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model.Member;
using Square.Picasso;

namespace PartyOrganizer.Adapters
{
    class PendingListAdapter : BaseAdapter<User>
    {
        private List<User> _partyPendings;
        private Activity _context;
        private ImageView _partyPendingImageView;
        private TextView _partyPendingNameTextView;
        private Button _partyPendingRefuseButton;
        private Button _partyPendingAcceptButton;

        public PendingListAdapter(Activity context, List<User> partyPendings)
        {
            this._context = context;
            _partyPendings = partyPendings;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var partyPending = _partyPendings[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyPendingRowView, null);
            }

            FindViews(convertView);

            Picasso.With(_context)
                   .Load(partyPending.Image)
                   .Into(_partyPendingImageView);
            _partyPendingNameTextView.Text = partyPending.Name;

            //HandleEvents();

            return convertView;
        }

        private void HandleEvents()
        {
            _partyPendingRefuseButton.Click += (object sender, EventArgs e) =>
            {
                // TODO: implement take amount method
            };

            _partyPendingAcceptButton.Click += (object sender, EventArgs e) =>
            {
                // TODO: implement take amount method
            };
        }

        private void FindViews(View convertView)
        {
            _partyPendingImageView = convertView.FindViewById<ImageView>(Resource.Id.partyPendingImageView);
            _partyPendingNameTextView = convertView.FindViewById<TextView>(Resource.Id.partyPendingNameTextView);
            _partyPendingRefuseButton = convertView.FindViewById<Button>(Resource.Id.partyPendingRefuseButton);
            _partyPendingAcceptButton = convertView.FindViewById<Button>(Resource.Id.partyPendingAcceptButton);
        }

        public override int Count => _partyPendings.Count;

        public override User this[int position] => _partyPendings[position];

        public override long GetItemId(int position) =>
            position;
    }
}