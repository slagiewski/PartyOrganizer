using System;
using System.IO;
using System.Threading.Tasks;
using Firebase.Xamarin.Auth;
using LiteDB;
using Plugin.Connectivity;

namespace PartyOrganizer.Core.Auth
{
    public static class FirebaseAuthLinkWrapper
    {
        private const string APIKEY = "AIzaSyDERJNDUI8FYT_u_q8yGhwcKXok1xhcHKs";
        private static string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                           "partyOrganizerDB.db");
        public static async Task<FirebaseAuthLink> Create(FirebaseAuthType authType, string oauthAccessToken)
        {
            if (CheckConnection())
            {
                if (AuthLink != null || !CheckConnection())
                    return AuthLink;

                await Initialize(authType, oauthAccessToken);
                Save();
                return AuthLink;
            }
            else
            {
                return null;
            }
        }

        private static async Task Initialize(FirebaseAuthType authType, string oauthAccessToken)
        {
            var _authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKEY));
            AuthLink = await _authProvider.SignInWithOAuthAsync(authType, oauthAccessToken);
        }

        private static bool CheckConnection() => CrossConnectivity.Current.IsConnected;

        private static void Save()
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var firebaseAuthLinks = db.GetCollection<FbAuthLinkSaveWrapper>();
                if (AuthLink != null)
                    firebaseAuthLinks.Upsert(new FbAuthLinkSaveWrapper
                    {
                        Id = 0,
                        AuthLink = AuthLink
                    });
            }
        }

        private static FirebaseAuthLink LoadFromDB()
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var firebaseAuthLinks = db.GetCollection<FbAuthLinkSaveWrapper>();
                var authLink = firebaseAuthLinks.FindOne( x => x.Id == 0);

                return authLink.AuthLink;
            }
        }

        private static FirebaseAuthLink AuthLink { get; set; }

        public class FbAuthLinkSaveWrapper
        {
            public int Id { get; set; }
            public FirebaseAuthLink AuthLink { get; set; }
        }

    }

    
}