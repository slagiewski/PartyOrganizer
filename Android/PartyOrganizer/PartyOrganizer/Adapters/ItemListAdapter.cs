using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Adapters
{
    class ItemListAdapter : BaseAdapter<PartyItem>
    {

        private List<PartyItem> _partyItems;
        private Activity _context;
        private TextView _partyItemNameTextView;
        private TextView _partyItemAmountTextView;
        private EditText _partyItemTakeAmountEditText;
        private Button _partyItemTakeAmountButton;

        public ItemListAdapter(Activity context, List<PartyItem> partyItems) : base()
        {
            _context = context;
            _partyItems = partyItems;
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

            HandleEvents();

            return convertView;
        }

        private void FindViews(View converView)
        {
            _partyItemNameTextView = converView.FindViewById<TextView>(Resource.Id.partyItemNameTextView);
            _partyItemAmountTextView = converView.FindViewById<TextView>(Resource.Id.partyItemAmountTextView);
            _partyItemTakeAmountEditText = converView.FindViewById<EditText>(Resource.Id.partyItemTakeAmountEditText);
            _partyItemTakeAmountButton = converView.FindViewById<Button>(Resource.Id.partyItemTakeAmountButton);
        }

        private void HandleEvents()
        {
            _partyItemTakeAmountButton.Click += (object sender, EventArgs e) =>
            {
                // TODO: implement take amount method
            };
        }

        public override int Count => _partyItems.Count;

        public override PartyItem this[int position] => _partyItems[position];

        public override long GetItemId(int position) =>
            position;
    }
}