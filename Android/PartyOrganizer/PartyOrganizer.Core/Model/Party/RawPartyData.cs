using System;

namespace PartyOrganizer.Core.Model.Party
{
    public class RawPartyData
    {
        public PartyContent Content { get; set; }

        public Object Pending { get; set; }

        public Object Members { get; set; }
    }
}