﻿using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Utility;

namespace PartyOrganizer.Adapters
{
    class PartyListAdapter : BaseAdapter<Party>
    {
        // IEnumerable might affect performance, will test it later how much
        // Edit: not a good idea
        List<Party> parties;
        Activity context;

        public PartyListAdapter(Activity context, IEnumerable<Party> parties)
        {
            this.context = context;
            this.parties = parties.ToList();
        }

        public override long GetItemId(int position)
        {
            return parties[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var party = parties[position];

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl("https://openclipart.org/image/800px/svg_to_png/" + party.ImagePath + ".jpg");

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.PartyRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.partyShortDescriptionTextView).Text = party.ShortDescription;
            convertView.FindViewById<TextView>(Resource.Id.dateTextView).Text = party.Date.ToString("dd/MM/yyyy hh:mm");
            convertView.FindViewById<TextView>(Resource.Id.adminTextView).Text = party.Admin.ToString();
            convertView.FindViewById<ImageView>(Resource.Id.partyImageView).SetImageBitmap(imageBitmap);
            
            return convertView;
        }

        public override int Count => parties.Count;

        public override Party this[int position] => parties[position];
    
    }
}