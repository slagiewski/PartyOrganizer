using System.Threading.Tasks;
using Firebase.Xamarin.Auth;

namespace PartyOrganizer.Core.Auth
{
    public static class FirebaseAuthLinkWrapper
    {
        private const string APIKEY = "";

        public static async Task<FirebaseAuthLink> Create(FirebaseAuthType authType, string oauthAccessToken)
        {
            if (AuthLink != null)
                return AuthLink;
            await Initialize(authType, oauthAccessToken);
            return AuthLink;
        }

        private static async Task Initialize(FirebaseAuthType authType, string oauthAccessToken)
        {
            var _authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKEY));
            AuthLink = await _authProvider.SignInWithOAuthAsync(authType, oauthAccessToken);
        }



        private static FirebaseAuthLink AuthLink { get; set; }
    }
}