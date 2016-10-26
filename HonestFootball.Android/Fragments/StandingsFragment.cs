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
using HonestFootball.ViewModels;
using HonestFootball.Models;
using HonestFootball.Android.Core;

namespace HonestFootball.Android.Fragments
{
    public class StandingsFragment : Fragment
    {
        private ListView listview;
        private StandingAdapter adapter;

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            StandingsViewModel settingsVM = new StandingsViewModel();

            IList<Team> teams = await settingsVM.GetStandings();

            listview = this.Activity.FindViewById<ListView>(Resource.Id.standingsListView);
            adapter = new StandingAdapter(this, teams.ToList());
            listview.Adapter = adapter;
        }

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    var view = inflater.Inflate(Resource.Layout.StandingsRow, container, false);

        //    var name = view.FindViewById<TextView>(Resource.Id.nameTextView);
        //    //name.Text = tea
        //    return view;
        //}
    }
}