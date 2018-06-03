using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Core.Auth;
using Square.Picasso;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;

namespace PartyOrganizer.Fragments
{
    public class MyProfileFragment : Android.Support.V4.App.Fragment, IFacebookCallback
    {
        private ICallbackManager _callBackManager;
        private ImageView _profilImage;
        private TextView _nameTextView;
        private Button _addPartyButton;
        private Button _joinPartyButton;
        private LoginButton _loginButton;

        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.MyProfileView, container, false);
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            var authLink = await FirebaseAuthLinkWrapper.Create(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            _callBackManager = CallbackManagerFactory.Create();
            _loginButton.SetReadPermissions("user_friends");
            _loginButton.RegisterCallback(_callBackManager, this);
            Picasso.With(this.Activity)
                   .Load(authLink.User.PhotoUrl)
                   .Into(_profilImage);

            _nameTextView.Text = authLink.User.DisplayName;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _callBackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        private void FindViews()
        {
            _profilImage = this.View.FindViewById<ImageView>(Resource.Id.profileImageView);
            _nameTextView = this.View.FindViewById<TextView>(Resource.Id.userTextView);
            _addPartyButton = this.View.FindViewById<Button>(Resource.Id.addPartyButton);
            _joinPartyButton = this.View.FindViewById<Button>(Resource.Id.joinPartyButton);
            _loginButton = this.View.FindViewById<LoginButton>(Resource.Id.loginProfileButton);
        }

        public void OnError(FacebookException error)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            //var intent = new Intent();
            //intent.SetClass(this.Activity, typeof(LoginActivity));
            //StartActivity(intent);
            //this.Activity.Finish();
        }

        public void OnCancel()
        {
            throw new NotImplementedException();
        }
    }
}