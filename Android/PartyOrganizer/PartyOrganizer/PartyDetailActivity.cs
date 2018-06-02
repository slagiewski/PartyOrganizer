//using Android.App;
//using Android.OS;
//using Android.Widget;
//using PartyOrganizer.Core.Model;
//using PartyOrganizer.Core.Repository;
//using PartyOrganizer.Core.Repository.Interfaces;
//using PartyOrganizer.Utility;
//using Square.Picasso;

//namespace PartyOrganizer
//{
//    [Activity(Label = "Party", MainLauncher = false)]
//    public class PartyDetailActivity : Activity
//    {
//        private IPartyRepository _partyRepository;
//        private PartyInfo _selectedParty;

//        private ImageView _partyAvatarImageView;
//        private TextView _partyShortDescriptionTextView;
//        private TextView _partyLongDescriptionTextView;
//        private TextView _partyAdminTextView;
//        private TextView _partyLocationTextView;
//        private TextView _partyDateTextView;

//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);
//            SetContentView(Resource.Layout.PartyDetailView);
//            _partyRepository = new PartyRepository();
//            var selectedPartyID = Intent.Extras.GetInt("selectedPartyID");
//            _selectedParty = _partyRepository.GetByID(selectedPartyID);

//            FindViews();

//            BindData();
//        }

//        private void FindViews()
//        {
//            _partyAvatarImageView = FindViewById<ImageView>(Resource.Id.partyAvatarImageView);
//            _partyShortDescriptionTextView = FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView);
//            _partyLongDescriptionTextView = FindViewById<TextView>(Resource.Id.partyLongDescriptionTextView);
//            _partyAdminTextView = FindViewById<TextView>(Resource.Id.partyAdminTextView);
//            _partyLocationTextView = FindViewById<TextView>(Resource.Id.partyLocationTextView);
//            _partyDateTextView = FindViewById<TextView>(Resource.Id.partyDateTextView);
//        }

//        private void BindData()
//        {
//            Picasso.With(this)
//                   .Load("https://openclipart.org/image/800px/svg_to_png/" + _selectedParty.Image)
//                   .Into(_partyAvatarImageView);
//            _partyShortDescriptionTextView.Text = _selectedParty.Name;
//            _partyLongDescriptionTextView.Text ="Szczegółowe informacje:\n\n" + _selectedParty.Description;
//            _partyAdminTextView.Text = _selectedParty.Admin?.ToString()??"App user (in progress)";
//            _partyLocationTextView.Text = _selectedParty.Location;
//            _partyDateTextView.Text = _selectedParty.Date.ToString("dd/MM/yyyy hh:mm"); ;
//        }

//    }
//}