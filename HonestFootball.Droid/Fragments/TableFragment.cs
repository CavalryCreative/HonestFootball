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
using HonestFootball.Droid.Core;
using System.Threading.Tasks;

namespace HonestFootball.Droid.Fragments
{
    public class TableFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.Table, container, false);

            var teams = Task.Run(async () => await GetStandings()).Result;

            ListView listview = view.FindViewById<ListView>(Resource.Id.tableListView);
            TableAdapter adapter = new TableAdapter(this, teams);
            listview.Adapter = adapter;

            return view;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private async Task<IList<Team>> GetStandings()
        {
            TableViewModel tableVM = new TableViewModel();

            IList<Team> teams = await tableVM.GetStandings(DroidSettings.TeamApiId);

            return teams;
        }
    }
}