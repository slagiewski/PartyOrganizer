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
        private Party _party;
        private IPartyRepositoryAsync _partyRepository;
        private Activity _context;
        private ImageView _partyPendingImageView;
        private TextView _partyPendingNameTextView;
        private Button _partyPendingRefuseButton;
        private Button _partyPendingAcceptButton;

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

            FindViews(convertView);

            Picasso.With(_context)
                   .Load(pendingUser.Image)
                   .Into(_partyPendingImageView);
            _partyPendingNameTextView.Text = pendingUser.Name;

            HandleEvents(pendingUser);

            return convertView;
        }

        // Move it up in the hierarchy
        private void HandleEvents(User pendingUser)
        {
            _partyPendingRefuseButton.Click += (s, e) =>
            {
                _partyRepository.RefuseRequest(_party, pendingUser);
            };

            _partyPendingAcceptButton.Click += (s, e) =>
            {
                _partyRepository.AcceptRequest(_party, pendingUser);
            };
        }

        private void FindViews(View convertView)
        {
            _partyPendingImageView = convertView.FindViewById<ImageView>(Resource.Id.partyPendingImageView);
            _partyPendingNameTextView = convertView.FindViewById<TextView>(Resource.Id.partyPendingNameTextView);
            _partyPendingRefuseButton = convertView.FindViewById<Button>(Resource.Id.partyPendingRefuseButton);
            _partyPendingAcceptButton = convertView.FindViewById<Button>(Resource.Id.partyPendingAcceptButton);
        }

        public override int Count => _pendingUsers.Count;

        public override User this[int position] => _pendingUsers[position];

        public override long GetItemId(int position) => position;
    }
}