using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Fragments;
using Square.Picasso;
using System.Collections.Generic;

namespace PartyOrganizer.Adapters
{
    class MemberListAdapter : BaseAdapter<PartyMember>
    {        
        private PartyMembersFragment _context;
        private List<PartyMember> _partyMembers;
        private ImageView _partyMemberImageView;
        private TextView _partyMemberNameTextView;
        private TextView _partyMemberHostTextView;

        public MemberListAdapter(PartyMembersFragment context, List<PartyMember> partyMembers) : base()
        {
            _context = context;
            _partyMembers = partyMembers;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var partyMember = _partyMembers[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyMemberRowView, null);
            }

            FindViews(convertView);
            BindData(partyMember);

            return convertView;
        }

        private void BindData(PartyMember partyMember)
        {
            Picasso.With(_context.Activity)
                        .Load(partyMember.Image)
                        .Into(_partyMemberImageView);
            _partyMemberNameTextView.Text = partyMember.Name;
            _partyMemberHostTextView.Text = partyMember.Type;
        }

        public override int Count => _partyMembers.Count;

        public override PartyMember this[int position] => _partyMembers[position];

        public override long GetItemId(int position) =>
            position;

        private void FindViews(View converView)
        {
            _partyMemberNameTextView = converView.FindViewById<TextView>(Resource.Id.partyMemberNameTextView);
            _partyMemberHostTextView = converView.FindViewById<TextView>(Resource.Id.partyMemberHostTextView);
            _partyMemberImageView = converView.FindViewById<ImageView>(Resource.Id.partyMemberImageView);
        }
    }
}