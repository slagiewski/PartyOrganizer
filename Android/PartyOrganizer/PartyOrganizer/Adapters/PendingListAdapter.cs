using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;
using PartyOrganizer.Fragments;
using Plugin.Connectivity;
using Square.Picasso;

namespace PartyOrganizer.Adapters
{
    class PendingListAdapter : BaseAdapter<Core.Model.Member.User>
    {
        private List<Core.Model.Member.User> _pendingUsers;
        private Party _party;
        private readonly FirebaseAuthLink _authLink;
        private IPartyRepositoryAsync _partyRepository;
        private PartyPendingFragment _context;

        public PendingListAdapter(PartyPendingFragment context, List<Core.Model.Member.User> pendingUsers, IPartyRepositoryAsync partyRepository,
                                  Party party, FirebaseAuthLink authLink)
        {
            this._context = context;
            _pendingUsers = pendingUsers;
            _partyRepository = partyRepository;
            _party = party;
            _authLink = authLink;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (_pendingUsers != null)
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

            return null;
        }

        private void BindData(Core.Model.Member.User pendingUser, ImageView partyPendingImageView, TextView partyPendingNameTextView)
        {
            Picasso.With(_context.Activity)
                   .Load(pendingUser.Image)
                   .Into(partyPendingImageView);

            partyPendingNameTextView.Text = pendingUser.Name;
        }

        private void FindViews(View convertView, out ImageView partyPendingImageView, out TextView partyPendingNameTextView, out Button partyPendingRefuseButton, out Button partyPendingAcceptButton)
        {
            partyPendingImageView = convertView.FindViewById<ImageView>(Resource.Id.partyPendingImageView);
            partyPendingNameTextView = convertView.FindViewById<TextView>(Resource.Id.partyPendingNameTextView);
            partyPendingRefuseButton = convertView.FindViewById<Button>(Resource.Id.partyPendingRefuseButton);
            partyPendingAcceptButton = convertView.FindViewById<Button>(Resource.Id.partyPendingAcceptButton);

            if (!CheckConnection() || _authLink.User.LocalId != GetHost()?.Id )
            {
                partyPendingAcceptButton.Visibility = Android.Views.ViewStates.Invisible;
                partyPendingRefuseButton.Visibility = Android.Views.ViewStates.Invisible;
                partyPendingAcceptButton.Enabled = false;
                partyPendingRefuseButton.Enabled = false;
            }
        }

        private bool CheckConnection() => CrossConnectivity.Current.IsConnected;

        private Core.Model.Member.User GetHost() => _party.Members.FirstOrDefault(u => u.Type.ToLower() == "host");

        private void HandleEvents(Core.Model.Member.User pendingUser, Button partyPendingRefuseButton, Button partyPendingAcceptButton)
        {
            partyPendingRefuseButton.Click += (s, e) =>
            {
                
                var alert = new AlertDialog.Builder(_context.Activity);

                alert.SetTitle("Refuse request");
                alert.SetMessage("Do you really want to delete this request?");

                alert.SetPositiveButton("Yes", async (sx, ex) =>
                {
                    var result = await _partyRepository.RefuseRequest(_party, pendingUser);
                    if (result)
                    {
                        await _context.NotifyDataChanged();
                    }
                });

                alert.SetNegativeButton("No", (sx, ex) => { });

                alert.Show();
            };

            partyPendingAcceptButton.Click += async (s, e) =>
            {
                var result = await _partyRepository.AcceptRequest(_party, pendingUser);
                if (result)
                {
                    await _context.NotifyDataChanged();
                }
            };
        }

        public override int Count => _pendingUsers?.Count ?? 0;

        public override Core.Model.Member.User this[int position] => _pendingUsers[position];

        public override long GetItemId(int position) => position;
    }
}