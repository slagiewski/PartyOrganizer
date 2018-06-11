using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Fragments
{
    public class PartyInfoFragment : Android.Support.V4.App.Fragment
    {
        private Party _selectedParty;
        private TextView _partyShortDescriptionTextView;
        private TextView _partyLongDescriptionTextView;
        private TextView _partyDateTextView;
        private TextView _partyLocationTextView;
        private TextView _partyIdTextView;

        public PartyInfoFragment(Party party)
        {
            _selectedParty = party;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);   
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            BindData();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PartyInfoView, container, false);
        }

        private void FindViews()
        {
            _partyShortDescriptionTextView = this.Activity.FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView);
            _partyLongDescriptionTextView = this.Activity.FindViewById<TextView>(Resource.Id.partyLongDescriptionTextView);
            _partyDateTextView = this.Activity.FindViewById<TextView>(Resource.Id.partyDateTextView);
            _partyLocationTextView = this.Activity.FindViewById<TextView>(Resource.Id.partyLocationTextView);
            _partyIdTextView = this.Activity.FindViewById<TextView>(Resource.Id.partyIdTextView);
        }

        private void BindData()
        {
            _partyShortDescriptionTextView.Text = _selectedParty.Content.Name;
            _partyLongDescriptionTextView.Text = "Szczegółowe informacje:\n\n" + _selectedParty.Content.Description;
            _partyLocationTextView.Text = _selectedParty.Content.Location.ToString();
            _partyDateTextView.Text = DateTimeOffset.FromUnixTimeSeconds(_selectedParty.Content.Unix).ToString();
            _partyIdTextView.Text = _selectedParty.Id;
        }
    }
}