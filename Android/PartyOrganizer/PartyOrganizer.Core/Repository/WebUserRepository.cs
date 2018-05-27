using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Core.Repository
{
    public class WebUserRepository : IUserRepositoryAsync
    {
        static private FirebaseClient _fb;

        static WebUserRepository()
        {
            _fb = new FirebaseClient("https://fir-test-420af.firebaseio.com/");
        }

        public Task<int> Add(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(string ID)
        {
            var users = await _fb
                         .Child("Users")
                         .Child("Key")
                         .OnceAsync<User>();

            return users.FirstOrDefault().Object;
        }

        public Task<IEnumerable<User>> GetFriends(User user)
        {
            throw new NotImplementedException();
        }

        public Task Remove(User entity)
        {
            throw new NotImplementedException();
        }
    }
}