using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HonestFootball.ViewModels;
using HonestFootball.Models;
using HonestFootball.Android.Core;
using HonestFootball.Interfaces;
using HonestFootball.Android.Fragments;

namespace HonestFootball.Android.Activities
{
    [Activity(Label = "SettingsActivity", MainLauncher = true)]
    public class SettingsActivity : Activity
    {
        private ListView settingListview;
        private SettingsAdapter settingsAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ServiceContainer.Register<IWebService>(() => new WebService());
            //ViewModels
            ServiceContainer.Register<BaseViewModel>();
            ServiceContainer.Register<SettingsViewModel>();
            ServiceContainer.Register<CommentsViewModel>();
            ServiceContainer.Register<StandingsViewModel>();

            //Get settings list
            SettingsViewModel settingsVM = new SettingsViewModel();
            List<Team> teams = new List<Team>();

            teams = settingsVM.GetTeams();

            settingListview = FindViewById<ListView>(Resource.Id.settingsListView);
            settingsAdapter = new SettingsAdapter(this, teams);
            settingListview.Adapter = settingsAdapter;
        }
    }
}