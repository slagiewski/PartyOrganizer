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
using PartyOrganizer.Core.Model;

namespace PartyOrganizer.Core.Repository.Interfaces
{
    public interface IPartyRepository : IRepository<Party>
    {
        IEnumerable<Party> GetPartiesOrganizedByUser(User User);

        IEnumerable<Party> GetPartiesUserParticipateIn(User user);

    }
}