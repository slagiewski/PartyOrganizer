using Xamarin.Facebook;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;

namespace PartyOrganizer
{
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity, IFacebookCallback
    {
        private ICallbackManager mCallBackManager;

        public void OnCancel()
        {
            //throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
            // throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            ChangeActivity();
            // Profile.FetchProfileForCurrentAccessToken();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mCallBackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
                
            SetContentView(Resource.Layout.LoginView);

            if (Profile.CurrentProfile != null && AccessToken.CurrentAccessToken?.Token != null)
            {
                ChangeActivity();
            }

            var button = FindViewById<LoginButton>(Resource.Id.login_button);

            mCallBackManager = CallbackManagerFactory.Create();

            button.SetReadPermissions("user_friends");

            button.RegisterCallback(mCallBackManager, this);

            

            // resolve keyHash

            //var info = this.PackageManager.GetPackageInfo("com.test.PartyOrganizer", Android.Content.PM.PackageInfoFlags.Signatures);

            //foreach (var signature in info.Signatures)
            //{
            //    var md = MessageDigest.GetInstance("SHA");
            //    md.Update(signature.ToByteArray());

            //    var keyHash = Convert.ToBase64String(md.Digest());

            //}

        }

        private void ChangeActivity()
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(MenuActivity));
            StartActivity(intent);
        }
    }
}