using Newtonsoft.Json;
using PartyOrganizer.Core.Model.Member;
using System;
using System.Collections.Generic;

namespace PartyOrganizer.Core.Model.Party
{
    public class RawPartyData
    {
        public PartyContent Content { get; set; }

        public Object Pending { get; set; }

        public Object Members { get; set; }

        public Party ToParty(string partyId)
        {
            var party = new Party
            {
                Id = partyId,
                Content = this.Content
            };

            if (this.Members != null)
            {
                var members = JsonConvert.DeserializeObject<Dictionary<string, PartyMember>>(this.Members.ToString());
                var membersList = new List<PartyMember>(members.Count);
                foreach (var member in members)
                {
                    member.Value.Id = member.Key;
                    membersList.Add(member.Value);
                }

                party.Members = membersList;
            }

            if (this.Pending != null)
            {
                var pendingMembers = JsonConvert.DeserializeObject<Dictionary<string, User>>(this.Pending.ToString());
                var pendingList = new List<User>(pendingMembers.Count);
                foreach (var pendingMember in pendingMembers)
                {
                    pendingMember.Value.Id = pendingMember.Key;
                    pendingList.Add(pendingMember.Value);
                }

                party.Pending = pendingList;
            }

            return party;
        }
    }
}