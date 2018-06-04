using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Core.Auth;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using Xamarin.Facebook;

namespace PartyOrganizer.Fragments
{
    public class PartyInfoFragment : Android.Support.V4.App.Fragment
    {
        private Party _selectedParty;
        private IPartyRepositoryAsync _partyRepository;
        private TextView _partyShortDescriptionTextView;
        private TextView _partyLongDescriptionTextView;
        private TextView _partyDateTextView;
        private TextView _partyLocationTextView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);   
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            _partyRepository = new WebPartyRepository(authLink);
            var selectedPartyID = this.Activity.Intent.Extras.GetString("selectedPartyID");
            _selectedParty = await _partyRepository.GetById(selectedPartyID);

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
        }

        private void BindData()
        {
            _partyShortDescriptionTextView.Text = _selectedParty.Content.Name;
            _partyLongDescriptionTextView.Text = "Szczegółowe informacje:\n\n" + _selectedParty.Content.Description;
            _partyLocationTextView.Text = _selectedParty.Content.Location.ToString();
            _partyDateTextView.Text = DateTimeOffset.FromUnixTimeSeconds(_selectedParty.Content.Unix).ToString();
        }
    }
}