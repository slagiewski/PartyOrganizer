using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using PartyOrganizer.Core.Auth;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository.Interfaces;

namespace PartyOrganizer.Adapters
{
    class ItemListAdapter : BaseAdapter<PartyItem>
    { 
        private readonly List<PartyItem> _partyItems;
        private readonly IPartyRepositoryAsync _partyRepository;
        private readonly Party _party;
        private readonly FirebaseAuthLink _authLink;
        private Activity _context;
        private TextView _partyItemNameTextView;
        private TextView _partyItemAmountTextView;
        private EditText _partyItemTakeAmountEditText;
        private Button _partyItemTakeAmountButton;

        public ItemListAdapter(Activity context, List<PartyItem> partyItems,
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
            var partyItem = _partyItems[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.PartyItemRowView, null);
            }

            FindViews(convertView);

            _partyItemNameTextView.Text = partyItem.Name;
            _partyItemAmountTextView.Text = partyItem.Amount.ToString();

            HandleEvents(_party, partyItem);

            return convertView;
        }

        private void FindViews(View converView)
        {
            _partyItemNameTextView = converView.FindViewById<TextView>(Resource.Id.partyItemNameTextView);
            _partyItemAmountTextView = converView.FindViewById<TextView>(Resource.Id.partyItemAmountTextView);
            _partyItemTakeAmountEditText = converView.FindViewById<EditText>(Resource.Id.partyItemTakeAmountEditText);
            _partyItemTakeAmountButton = converView.FindViewById<Button>(Resource.Id.partyItemTakeAmountButton);
        }

        private void HandleEvents(Party party, PartyItem partyItem)
        {
            _partyItemTakeAmountButton.Click += (s, e) =>
            {
                try
                {
                    var currentAmount = 0;

                    foreach (var member in party.Members)
                    {
                        var item = member.Items?.FirstOrDefault(p => p.Name == partyItem.Name);
                        if (item != null)
                        {
                            currentAmount += item.Amount;
                        }
                    }

                    var amountLeft = partyItem.Amount - currentAmount;

                    var amount = Convert.ToInt32(_partyItemTakeAmountEditText.Text);
                    if (!String.IsNullOrWhiteSpace(_partyItemNameTextView.Text) && amount > 0 && amount <= amountLeft)
                    {
                        var userId = _authLink.User.LocalId;
                        var newParty = new Party
                        {
                            // Change item amount value
                            Id = party.Id,
                            Members = party.Members,
                            Pending = party.Pending,
                            Content = party.Content
                        };

                        var item = newParty
                                    .Members
                                    .FirstOrDefault(u => u.Id == userId)
                                    ?.Items
                                    .FirstOrDefault(i => i.Name == partyItem.Name);

                        item.Amount -= amount;

                        _partyRepository.UpdatePartyItems(newParty);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("TakeAmount error: " + ex.Message);
                }
                
            };
        }

        public override int Count => _partyItems.Count;

        public override PartyItem this[int position] => _partyItems[position];

        public override long GetItemId(int position) =>
            position;
    }
}