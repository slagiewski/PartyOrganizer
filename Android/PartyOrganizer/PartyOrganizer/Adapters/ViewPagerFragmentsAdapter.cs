using System.Collections.Generic;
using Android.Support.V4.App;

namespace PartyOrganizer.Adapters
{
    class ViewPagerFragmentsAdapter : FragmentPagerAdapter
    {
        private readonly List<Android.Support.V4.App.Fragment> _fragments;

        public ViewPagerFragmentsAdapter(Android.Support.V4.App.FragmentManager fm, List<Android.Support.V4.App.Fragment> fragments) : base(fm)
        {
            _fragments = fragments;
        }      

        public override int Count =>
            _fragments.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position) =>
            _fragments[position];

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position) =>
            new Java.Lang.String(_fragments[position].GetType().Name.Replace("Fragment",""));

            
        
    }
}