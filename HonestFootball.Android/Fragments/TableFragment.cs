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
    public class TableFragment : Fragment
    {
        private ListView listview;
        private TableAdapter adapter;

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TableViewModel tableVM = new TableViewModel();

            IList<Team> teams = await tableVM.GetStandings();

            listview = this.Activity.FindViewById<ListView>(Resource.Id.tableListView);
            adapter = new TableAdapter(this, teams.ToList());
            listview.Adapter = adapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}