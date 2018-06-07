using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Adapters
{
    class ItemListAdapter : BaseAdapter<PartyItem>
    { 
        private readonly Dictionary<string, PartyItem> _partyItems;
        private readonly IPartyRepositoryAsync _partyRepository;
        private readonly Party _party;
        private readonly FirebaseAuthLink _authLink;
        private Activity _context;

        public ItemListAdapter(Activity context, Dictionary<string, PartyItem> partyItems,
            IPartyRepositoryAsync partyRepository, Party party, FirebaseAuthLink authLink) : base()
        {
            _context = context;
            _partyItems = partyItems;
            _partyRepository = partyRepository;
            _party = party;
            _authLink = authLink;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyItemRowView, null);
            }

            var _partyItemNameTextView = convertView.FindViewById<TextView>(Resource.Id.partyItemNameTextView);
            var _partyItemAmountTextView = convertView.FindViewById<TextView>(Resource.Id.partyItemAmountTextView);
            var _partyItemTakeAmountEditText = convertView.FindViewById<EditText>(Resource.Id.partyItemTakeAmountEditText);
            var _partyItemTakeAmountButton = convertView.FindViewById<Button>(Resource.Id.partyItemTakeAmountButton);

            var partyItem = _partyItems.ElementAt(position);

            _partyItemNameTextView.Text = partyItem.Value.Name;
            _partyItemAmountTextView.Text = partyItem.Value.Amount.ToString();

            _partyItemTakeAmountButton.Click += (s, e) =>
            {
                try
                {
                    var amount = Convert.ToInt32(_partyItemTakeAmountEditText.Text);

                    
                    if (!String.IsNullOrWhiteSpace(_partyItemNameTextView.Text) && amount > 0 && amount <= _party.Content.Items[partyItem.Key].Amount)
                    {
                        _partyRepository.UpdatePartyItem(this._party, partyItem, amount);

                    }


                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("TakeAmount error: " + ex.Message);

                }
                

            };

            return convertView;
        }

        private void HandleEvents(Party party, PartyItem partyItem, int position)
        {
            //_partyItemTakeAmountButton.Click += (s, e) =>
            //{
            //    try
            //    {
            //        var currentAmount = 0;

            //        foreach (var member in party.Members)
            //        {
            //            var item = member.Items?.FirstOrDefault(p => p.Name == partyItem.Name);
            //            if (item != null)
            //            {
            //                currentAmount += item.Amount;
            //            }
            //        }

            //        var amountLeft = partyItem.Amount - currentAmount;

            //        var amount = Convert.ToInt32(_partyItemTakeAmountEditText.Text);
            //        if (!String.IsNullOrWhiteSpace(_partyItemNameTextView.Text) && amount > 0 && amount <= amountLeft)
            //        {
            //            var userId = _authLink.User.LocalId;
            //            var newParty = new Party
            //            {
            //                // Change item amount value
            //                Id = party.Id,
            //                Members = party.Members,
            //                Pending = party.Pending,
            //                Content = party.Content
            //            };

            //            var item = newParty
            //                        .Members
            //                        .FirstOrDefault(u => u.Id == userId)
            //                        ?.Items
            //                        .FirstOrDefault(i => i.Name == partyItem.Name);

            //            item.Amount -= amount;

            //            _partyRepository.UpdatePartyItems(newParty);
            //        }
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine("TakeAmount error: " + ex.Message);
                //}

            //};
        }

        public override int Count => _partyItems.Count;

        public override PartyItem this[int position] => _partyItems.ElementAt(position).Value;

        public override long GetItemId(int position) =>
            position;
    }
}