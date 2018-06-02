﻿namespace PartyOrganizer.Core.Model.Party
{
    public class PartyItem
    {
        public int Amount { get; set; }

        public string Name { get; set; }

        public PartyItem(int amount, string name)
        {
            this.Amount = amount;
            this.Name = name;
        }
    }
    
}