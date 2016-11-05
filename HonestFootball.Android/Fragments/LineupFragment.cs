using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fragment = Android.Support.V4.App.Fragment;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using HonestFootball.Android;

namespace HonestFootball.Android.Fragments
{
    public class LineupFragment : Fragment, IDataFromActivityToLineupFragment
    {
        TextView txtView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Lineup, null);
            txtView = (TextView)rootView.FindViewById(Resource.Id.textView1);

            return rootView;
        }

        public void SendData(string data)
        {
            if (data != null)
                txtView.Text = data;
        }
    }
}