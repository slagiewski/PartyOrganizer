using PartyOrganizer.Core.Model.Member;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public partial class Party
    {
        public string Id { get; set; }

        public PartyContent Content { get; set; }

        public IEnumerable<PartyMember> Members { get; set; }

        public IEnumerable<User> Pending { get; set; }
    }
}