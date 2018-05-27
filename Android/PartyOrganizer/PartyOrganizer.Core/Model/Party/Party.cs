using Firebase.Xamarin.Database;
using System;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public partial class Party
    {
        public string Id { get; set; }

        public PartyContent Content { get; set; }

        public IEnumerable<PartyMember> Members { get; set; }

        public IEnumerable<PartyMember> Pending { get; set; }
    }
}