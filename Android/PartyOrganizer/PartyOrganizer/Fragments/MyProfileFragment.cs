using System;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Auth;
using Square.Picasso;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace PartyOrganizer.Fragments
{
    public class MyProfileFragment : Android.Support.V4.App.Fragment
    {
        private ImageView _profileImage;
        private TextView _nameTextView;
        private Button _addPartyButton;
        private Button _joinPartyButton;
        private LoginButton _loginButton;        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.MyProfileView, container, false);
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();

            HandleEvents();

            await BindData();
        }

        private async Task BindData()
        {
            var authLink = await FirebaseAuthLinkWrapper.Create(Firebase.Xamarin.Auth.FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            
            if (authLink != null)
            {
                _nameTextView.Text = authLink.User?.DisplayName;

                Picasso.With(this.Context)
                       .Load(authLink.User.PhotoUrl)
                       .Into(_profileImage);
            }
                
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void FindViews()
        {
            _profileImage = this.View.FindViewById<ImageView>(Resource.Id.profileImageView);
            _nameTextView = this.View.FindViewById<TextView>(Resource.Id.userTextView);
            _addPartyButton = this.View.FindViewById<Button>(Resource.Id.addPartyButton);
            _joinPartyButton = this.View.FindViewById<Button>(Resource.Id.joinPartyButton);
            _loginButton = this.View.FindViewById<LoginButton>(Resource.Id.loginProfileButton);
        }

        private void HandleEvents()
        {
            _addPartyButton.Click += (s, e) =>
            {
                var intent = new Intent();
                intent.SetClass(this.Context, typeof(AddPartyActivity));
                StartActivity(intent);
            };

            _loginButton.Click += (s, e) =>
            {
                LoginManager.Instance.LogOut();
                var intent = new Intent();
                intent.SetClass(this.Context, typeof(LoginActivity));
                StartActivity(intent);
            };

            _joinPartyButton.Click += (s, e) =>
            {
                var intent = new Intent();
               // intent.SetClass(this.Context, typeof());
                StartActivity(intent);
            };
        }

    }
}