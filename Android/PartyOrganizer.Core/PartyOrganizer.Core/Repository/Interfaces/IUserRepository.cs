using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetFriends(User user);
        
    }
}