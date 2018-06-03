using Android.App;
using Android.OS;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Core.Auth;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using Square.Picasso;
using System;
using System.Linq;
using Xamarin.Facebook;

namespace PartyOrganizer
{
    [Activity(Label = "Party", MainLauncher = false)]
    public class PartyDetailActivity : Activity
    {
        private IPartyRepositoryAsync _partyRepository;
        private Party _selectedParty;

        private ImageView _partyAvatarImageView;
        private TextView _partyShortDescriptionTextView;
        private TextView _partyLongDescriptionTextView;
        private TextView _partyAdminTextView;
        private TextView _partyLocationTextView;
        private TextView _partyDateTextView;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PartyDetailView);
            var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            _partyRepository = new WebPartyRepository(authLink);
            var selectedPartyID = Intent.Extras.GetString("selectedPartyID");
            _selectedParty = await _partyRepository.GetById(selectedPartyID);

            FindViews();

            BindData();
        }

        private void FindViews()
        {
            _partyAvatarImageView = FindViewById<ImageView>(Resource.Id.partyAvatarImageView);
            _partyShortDescriptionTextView = FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView);
            _partyLongDescriptionTextView = FindViewById<TextView>(Resource.Id.partyLongDescriptionTextView);
            _partyAdminTextView = FindViewById<TextView>(Resource.Id.partyAdminTextView);
            _partyLocationTextView = FindViewById<TextView>(Resource.Id.partyLocationTextView);
            _partyDateTextView = FindViewById<TextView>(Resource.Id.partyDateTextView);
        }

        private void BindData()
        {
            Picasso.With(this)
                   .Load(_selectedParty.Content.Image)
                   .Into(_partyAvatarImageView);
            _partyShortDescriptionTextView.Text = _selectedParty.Content.Name;
            _partyLongDescriptionTextView.Text ="Szczegółowe informacje:\n\n" + _selectedParty.Content.Description;
            _partyAdminTextView.Text = _selectedParty.Members?.FirstOrDefault(m  => m.Type.ToLower() == "host").Name ?? "App user (in progress)";
            _partyLocationTextView.Text = _selectedParty.Content.Location.ToString();
            _partyDateTextView.Text = DateTimeOffset.FromUnixTimeSeconds(_selectedParty.Content.Unix).ToString();
        }

    }
}