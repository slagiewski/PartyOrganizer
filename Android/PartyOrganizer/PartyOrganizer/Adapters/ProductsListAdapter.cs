using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model.Party;

namespace PartyOrganizer.Adapters
{
    class ProductsListAdapter : BaseAdapter<PartyItem>
    {

        private List<PartyItem> _partyItems;
        private Activity _context;

        public ProductsListAdapter(Activity context, List<PartyItem> partyItems) : base()
        {
            _context = context;
            _partyItems = partyItems;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var partyItem = _partyItems[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.ProductRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.productNameTextView).Text = partyItem.Name;
            convertView.FindViewById<TextView>(Resource.Id.productAmountTextView).Text = partyItem.Amount.ToString();

            return convertView;
        }

        public override int Count => _partyItems.Count;

        public override PartyItem this[int position] => _partyItems[position];

        public override long GetItemId(int position)
        {
            return position;
        }
    }
}