using Android.App;
using Android.OS;
using Android.Widget;
using PartyOrganizer.Core;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using PartyOrganizer.Utility;

namespace PartyOrganizer
{
    [Activity(Label = "Użytkownik", MainLauncher = true)]
    public class UserDetailActivity : Activity
    {
        private ImageView avatarImageView;
        private TextView userFullNameTextView;
        private TextView userLocationTextView;
        private TextView userEmailTextView;

        private User selectedUser;
        private IUserRepository userRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserDetailView);

            userRepository = new UserRepository();
            selectedUser = userRepository.GetByID(0);

            FindViews();

            BindData();
        }

        private void BindData()
        {
            var avatarBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + selectedUser.ImagePath + ".jpg");
            avatarImageView.SetImageBitmap(avatarBitmap);
            userFullNameTextView.Text = selectedUser.FullName;
            userLocationTextView.Text = selectedUser.Location;
            userEmailTextView.Text = selectedUser.Email;
        }

        private void FindViews()
        {
            avatarImageView = FindViewById<ImageView>(Resource.Id.avatarImageView);
            userFullNameTextView = FindViewById<TextView>(Resource.Id.userFullNameTextView);
            userLocationTextView = FindViewById<TextView>(Resource.Id.userLocationTextView);
            userEmailTextView = FindViewById<TextView>(Resource.Id.userEmailTextView);
        }
    }
}