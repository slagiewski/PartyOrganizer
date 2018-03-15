using Android.App;
using Android.OS;
using Android.Widget;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using PartyOrganizer.Utility;

namespace PartyOrganizer
{
    [Activity(Label = "Wydarzenie", MainLauncher = true)]
    public class PartyDetailActivity : Activity
    {
        IPartyRepository partyRepository;
        Party selectedParty;

        ImageView partyAvatarImageView;
        TextView partyShortDescriptionTextView;
        TextView partyLongDescriptionTextView;
        TextView partyAdminTextView;
        TextView partyLocationTextView;
        TextView partyDateTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PartyDetailView);
            partyRepository = new PartyRepository();
            selectedParty = partyRepository.GetByID(0);

            FindViews();

            BindData();
        }

        private void FindViews()
        {
            partyAvatarImageView = FindViewById<ImageView>(Resource.Id.partyAvatarImageView);
            partyShortDescriptionTextView = FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView);
            partyLongDescriptionTextView = FindViewById<TextView>(Resource.Id.partyLongDescriptionTextView);
            partyAdminTextView = FindViewById<TextView>(Resource.Id.partyAdminTextView);
            partyLocationTextView = FindViewById<TextView>(Resource.Id.partyLocationTextView);
            partyDateTextView = FindViewById<TextView>(Resource.Id.partyDateTextView);
        }

        private void BindData()
        {
            var avatarBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + selectedParty.ImagePath + ".jpg");
            partyAvatarImageView.SetImageBitmap(avatarBitmap);
            partyShortDescriptionTextView.Text = selectedParty.ShortDescription;
            partyLongDescriptionTextView.Text ="Szczegółowe informacje:\n\n" + selectedParty.Description;
            partyAdminTextView.Text = selectedParty.Admin.FullName;
            partyLocationTextView.Text = selectedParty.Location;
            partyDateTextView.Text = selectedParty.Date.ToString();
        }

    }
}