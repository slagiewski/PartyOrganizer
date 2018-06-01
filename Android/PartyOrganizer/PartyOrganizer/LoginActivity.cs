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
            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mCallBackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //facebook init
            

            SetContentView(Resource.Layout.LoginView);

            var button = FindViewById<LoginButton>(Resource.Id.login_button);

            mCallBackManager = CallbackManagerFactory.Create();

            button.SetReadPermissions("user_friends");

            button.RegisterCallback(mCallBackManager, this);

        }
    }
}