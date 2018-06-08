using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model.Member;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;
using Square.Picasso;

namespace PartyOrganizer.Adapters
{
    class PendingListAdapter : BaseAdapter<User>
    {
        private List<User> _pendingUsers;
        private readonly Party _party;
        private IPartyRepositoryAsync _partyRepository;
        private Activity _context;

        public PendingListAdapter(Activity context, List<User> pendingUsers,
            IPartyRepositoryAsync partyRepository, Party party)
        {
            this._context = context;
            _pendingUsers = pendingUsers;
            _partyRepository = partyRepository;
            _party = party;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var pendingUser = _pendingUsers[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyPendingRowView, null);
            }

            FindViews(convertView, out ImageView partyPendingImageView, out TextView partyPendingNameTextView,
                                   out Button partyPendingRefuseButton, out Button partyPendingAcceptButton);

            HandleEvents(pendingUser, partyPendingRefuseButton, partyPendingAcceptButton);

            BindData(pendingUser, partyPendingImageView, partyPendingNameTextView);

            return convertView;
        }

        private void BindData(User pendingUser, ImageView partyPendingImageView, TextView partyPendingNameTextView)
        {
            Picasso.With(_context)
                   .Load(pendingUser.Image)
                   .Into(partyPendingImageView);

            partyPendingNameTextView.Text = pendingUser.Name;
        }

        private static void FindViews(View convertView, out ImageView partyPendingImageView, out TextView partyPendingNameTextView, out Button partyPendingRefuseButton, out Button partyPendingAcceptButton)
        {
            partyPendingImageView = convertView.FindViewById<ImageView>(Resource.Id.partyPendingImageView);
            partyPendingNameTextView = convertView.FindViewById<TextView>(Resource.Id.partyPendingNameTextView);
            partyPendingRefuseButton = convertView.FindViewById<Button>(Resource.Id.partyPendingRefuseButton);
            partyPendingAcceptButton = convertView.FindViewById<Button>(Resource.Id.partyPendingAcceptButton);
        }

        private void HandleEvents(User pendingUser, Button partyPendingRefuseButton, Button partyPendingAcceptButton)
        {
            partyPendingRefuseButton.Click += (s, e) =>
            {
                _partyRepository.RefuseRequest(_party, pendingUser);
            };

            partyPendingAcceptButton.Click += (s, e) =>
            {
                _partyRepository.AcceptRequest(_party, pendingUser);
            };
        }

        public override int Count => _pendingUsers.Count;

        public override User this[int position] => _pendingUsers[position];

        public override long GetItemId(int position) => position;
    }
}