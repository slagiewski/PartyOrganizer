using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Facebook;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook.Login.Widget;
using Java.Lang;
using Xamarin.Facebook.Login;
using Java.Security;

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
            var loginResult = result as LoginResult;
            var accessToken = loginResult.AccessToken;
            var test1 = loginResult.AccessToken.Token;
            var test = AccessToken.CurrentAccessToken.Token;

            var intent = new Intent();
            intent.SetClass(this, typeof(MenuActivity));
            StartActivity(intent);
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
    }
}