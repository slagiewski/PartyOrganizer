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
using PartyOrganizer.Fragments;
using Plugin.Connectivity;

namespace PartyOrganizer.Adapters
{
    class ItemListAdapter : BaseAdapter<PartyItem>
    { 
        private readonly Dictionary<string, PartyItem> _partyItems;
        private readonly IPartyRepositoryAsync _partyRepository;
        private readonly Party _party;
        private readonly FirebaseAuthLink _authLink;
        private PartyItemsFragment _context;

        public ItemListAdapter(PartyItemsFragment context, Dictionary<string, PartyItem> partyItems,
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

            FindViews(convertView, out TextView _partyItemNameTextView, out TextView _partyItemAmountTextView, out EditText _partyItemTakeAmountEditText, out Button _partyItemTakeAmountButton);

            var partyItem = _partyItems.ElementAt(position);

            _partyItemNameTextView.Text = partyItem.Value.Name;
            _partyItemAmountTextView.Text = partyItem.Value.Amount.ToString();

            HandleEvents(_partyItemNameTextView, _partyItemTakeAmountEditText, _partyItemTakeAmountButton, partyItem);

            return convertView;
        }

        private void FindViews(View convertView, out TextView _partyItemNameTextView, out TextView _partyItemAmountTextView, out EditText _partyItemTakeAmountEditText, out Button _partyItemTakeAmountButton)
        {
            _partyItemNameTextView = convertView.FindViewById<TextView>(Resource.Id.partyItemNameTextView);
            _partyItemAmountTextView = convertView.FindViewById<TextView>(Resource.Id.partyItemAmountTextView);
            _partyItemTakeAmountEditText = convertView.FindViewById<EditText>(Resource.Id.partyItemTakeAmountEditText);
            _partyItemTakeAmountButton = convertView.FindViewById<Button>(Resource.Id.partyItemTakeAmountButton);
            if (!CheckConnection())
            {
                _partyItemTakeAmountEditText.Visibility = Android.Views.ViewStates.Invisible;
                _partyItemTakeAmountButton.Visibility = Android.Views.ViewStates.Invisible;
                _partyItemTakeAmountEditText.Enabled = false;
                _partyItemTakeAmountButton.Enabled = false;
            }

        }

        private bool CheckConnection() => CrossConnectivity.Current.IsConnected;

        private void HandleEvents(TextView _partyItemNameTextView, EditText _partyItemTakeAmountEditText, Button _partyItemTakeAmountButton, KeyValuePair<string, PartyItem> partyItem)
        {
            _partyItemTakeAmountButton.Click += async (s, e) =>
            {
                try
                {
                    var amount = Convert.ToInt32(_partyItemTakeAmountEditText.Text);

                    if (!String.IsNullOrWhiteSpace(_partyItemNameTextView.Text) && amount > 0 && amount <= _party.Content.Items[partyItem.Key].Amount)
                    {
                        if (_partyRepository.UpdatePartyItem(this._party, partyItem, amount) == null)
                            throw new Exception();
                        else
                            await _context.NotifyDataChanged();
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (Exception ex)
                {
                    if (ex is FormatException)
                        ShowDialog("Invalid amount!");
                    else
                        ShowDialog("Error occurred!");
                    Debug.WriteLine("TakeAmount error: " + ex.Message);
                }
            };
        }

        private void ShowDialog(string message)
        {
            var alert = new AlertDialog.Builder(_context.Activity);

            alert.SetTitle("Party Items");
            alert.SetMessage(message);

            alert.SetPositiveButton("Ok", (sx, ex) => { });

            alert.Show();
        }

        public override int Count => _partyItems.Count;

        public override PartyItem this[int position] => _partyItems.ElementAt(position).Value;

        public override long GetItemId(int position) => position;
    }
}