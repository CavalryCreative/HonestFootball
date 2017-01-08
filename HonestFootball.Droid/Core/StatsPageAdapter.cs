using SupportV4Fragment = Android.Support.V4.App.Fragment;
using SupportV4FragmentManager = Android.Support.V4.App.FragmentManager;
using FragmentPagerAdapter = Android.Support.V4.App.FragmentPagerAdapter;
using Java.Lang;

namespace HonestFootball.Droid.Core
{
    public class StatsPageAdapter : FragmentPagerAdapter
    {
        SupportV4Fragment[] fragments;
        ICharSequence[] titles;

        public StatsPageAdapter(SupportV4FragmentManager fm, SupportV4Fragment[] fragments, ICharSequence[] titles) : base(fm)
        {
            this.fragments = fragments;
            this.titles = titles;
        }

        public override int Count
        {
            get
            {
                return fragments.Length;
            }
        }

        public override SupportV4Fragment GetItem(int position)
        {
            return fragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return titles[position];
        }
    }
}