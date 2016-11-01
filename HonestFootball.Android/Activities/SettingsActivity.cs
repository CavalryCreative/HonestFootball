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
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ServiceContainer.Register<IWebService>(() => new WebService());
            //ViewModels
            ServiceContainer.Register<BaseViewModel>();
            ServiceContainer.Register<SettingsViewModel>();
            ServiceContainer.Register<CommentsViewModel>();
            ServiceContainer.Register<StandingsViewModel>();

           
        }
    }
}