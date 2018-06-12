//using Android.App;
//using Android.OS;
//using Android.Widget;
//using PartyOrganizer.Core;
//using PartyOrganizer.Core.Repository;
//using PartyOrganizer.Core.Repository.Interfaces;
//using PartyOrganizer.Utility;
//using Square.Picasso;

//namespace PartyOrganizer
//{
//    [Activity(Label = "User", MainLauncher = false)]
//    public class UserDetailActivity : Activity
//    {
//        private ImageView _avatarImageView;
//        private TextView _userFullNameTextView;
//        private TextView _userLocationTextView;
//        private TextView _userEmailTextView;

//        private User _selectedUser;
//        private IUserRepository _userRepository;

//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);

//            SetContentView(Resource.Layout.UserDetailView);

//            _userRepository = new UserRepository();
//            var selectedUserID = Intent.Extras.GetInt("selectedUserID");
//            _selectedUser = _userRepository.GetByID(selectedUserID);

//            FindViews();

//            BindData();
//        }

//        private void BindData()
//        {
//            Picasso.With(this)
//                   .Load("https://openclipart.org/image/800px/svg_to_png/" + _selectedUser.Image + ".jpg")
//                   .Into(_avatarImageView);
//            _userFullNameTextView.Text = _selectedUser.ToString();
//            _userLocationTextView.Text = _selectedUser.Location;
//            _userEmailTextView.Text = _selectedUser.Email;
//        }

//        private void FindViews()
//        {
//            _avatarImageView = FindViewById<ImageView>(Resource.Id.avatarImageView);
//            _userFullNameTextView = FindViewById<TextView>(Resource.Id.userFullNameTextView);
//            _userLocationTextView = FindViewById<TextView>(Resource.Id.userLocationTextView);
//            _userEmailTextView = FindViewById<TextView>(Resource.Id.userEmailTextView);
//        }
//    }
//}