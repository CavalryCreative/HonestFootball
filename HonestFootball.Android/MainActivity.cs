using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Android.Support.Design.Widget;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json.Linq;
using HonestFootball.ViewModels;
using HonestFootball.Models;
using HonestFootball.Android.Core;
using HonestFootball.Interfaces;
using HonestFootball.Android.Fragments;
using System.Collections.Generic;

namespace HonestFootball.Android
{
    [Activity(Label = "HonestFootball.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        private FrameLayout mFragmentContainer;
        private SupportFragment mCurrentFragment;
        private SettingsFragment settingsFragment;
        private ShareFragment shareFragment;
        private Stack<SupportFragment> mStackFragments;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Android platform specific
            //ServiceContainer.Register<IAppSettings>(() => new DroidSettings(this));
            ServiceContainer.Register<IWebService>(() => new WebService());
            //ViewModels
            ServiceContainer.Register<BaseViewModel>();
            ServiceContainer.Register<SettingsViewModel>();
            ServiceContainer.Register<CommentsViewModel>();
            ServiceContainer.Register<StandingsViewModel>();

            // Create UI
            SetContentView(Resource.Layout.Main);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Create fragments
            mFragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);

            settingsFragment = new SettingsFragment();
            shareFragment = new ShareFragment();
          
            mStackFragments = new Stack<SupportFragment>();

            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, settingsFragment, "SettingsFragment");
            trans.Hide(settingsFragment);

            trans.Add(Resource.Id.fragmentContainer, shareFragment, "ShareFragment");
            trans.Hide(shareFragment);

            // Init toolbar
            var toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Attach item selected handler to navigation view
            var navigationViewLeft = FindViewById<NavigationView>(Resource.Id.nav_view_left);
            navigationViewLeft.NavigationItemSelected += NavigationView_NavigationItemSelected;

            var navigationViewRight = FindViewById<NavigationView>(Resource.Id.nav_view_right);
            navigationViewRight.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            try
            {
                GetClient();
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            try
            {
                GetClient();
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.ConversationsMenu, menu);
            //return base.OnCreateOptionsMenu(menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_share:
                    ShowFragment(shareFragment);
                    return true;

                case Resource.Id.action_standings:
                    ShowFragment(settingsFragment);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
            {
                return;
            }

            var trans = SupportFragmentManager.BeginTransaction();

            trans.SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, Resource.Animation.slide_in, Resource.Animation.slide_out);

            fragment.View.BringToFront();
            mCurrentFragment.View.BringToFront();

            trans.Hide(mCurrentFragment);
            trans.Show(fragment);

            trans.AddToBackStack(null);
            mStackFragments.Push(mCurrentFragment);
            trans.Commit();

            mCurrentFragment = fragment;
        }

        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
                mCurrentFragment = mStackFragments.Pop();
            }

            else
            {
                base.OnBackPressed();
            }
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.action_settings):
                    // React on 'Settings' selection
                    break;
                case (Resource.Id.action_share):
                    // React on 'Share' selection
                    break;
                case (Resource.Id.action_standings):
                    // React on 'Standings' selection
                    break;
                case (Resource.Id.action_news):
                    // React on 'Discussion' selection
                    break;
            }

            // Close drawer
            drawerLayout.CloseDrawers();
        }

        private void GetClient()
        {
            var client = new Client("Android");

            client.Connect(this);

            client.OnMessageReceived += (sender, message) => RunOnUiThread(() =>
            {
                string json = message;
                string jPath = "Events";

                JToken token = JToken.Parse(json);

                var y = token.SelectTokens(jPath);

                DroidSettings.TeamApiId = "9378";//TODO - test remove when settings screen set up

                foreach (var childToken in y.Children())
                {
                    // Comment comment = new Comment();

                    var evtScore = childToken.SelectToken("Score").ToString();
                    var evtMinute = childToken.SelectToken("Minute").ToString();
                    var homeTeamId = childToken.SelectToken("HomeTeamAPIId").ToString();
                    var awayTeamId = childToken.SelectToken("AwayTeamAPIId").ToString();
                    var evtComment = childToken.SelectToken("EventComment").ToString();

                    if (DroidSettings.TeamApiId == homeTeamId || DroidSettings.TeamApiId == awayTeamId)
                    {
                        string jokeComment = string.Empty;

                        if (DroidSettings.TeamApiId == homeTeamId)
                        {
                            jokeComment = childToken.SelectToken("HomeComment").ToString();
                        }
                        else if (DroidSettings.TeamApiId == awayTeamId)
                        {
                            jokeComment = childToken.SelectToken("AwayComment").ToString();
                        }

                        var commentText = FindViewById<TextView>(Resource.Id.commentText);
                        var jokeCommentText = FindViewById<TextView>(Resource.Id.jokeCommentText);
                        var matchScore = FindViewById<TextView>(Resource.Id.matchScore);
                        var matchTime = FindViewById<TextView>(Resource.Id.matchTime);

                        commentText.Text = evtComment;
                        jokeCommentText.Text = jokeComment;
                        matchScore.Text = evtScore;
                        matchTime.Text = evtMinute;
                    }
                    //else
                    //{
                    //    DisplayText(evtComment.ToString());
                    //}
                }
            });
        }

        private async void GetStandings()
        {
            StandingsViewModel standingsVM = new StandingsViewModel();

            await standingsVM.GetStandings();
        }

        protected void DisplayError(Exception exc)
        {
            string error = exc.Message;

            new AlertDialog.Builder(this)
            .SetTitle(Resource.String.ErrorTitle)
            .SetMessage("Something's royally fucked up!")
            .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
            .Show();
        }

        protected void DisplayText(string txt)
        {
            new AlertDialog.Builder(this)
            .SetTitle(Resource.String.ErrorTitle)
            .SetMessage(txt)
            .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
            .Show();
        }
    }

    public class Client
    {
        private readonly string _platform;
        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;

        public event EventHandler<string> OnMessageReceived;

        public Client(string platform)
        {
            _platform = platform;
            _connection = new HubConnection("http://honest-apps.elasticbeanstalk.com/");
            _proxy = _connection.CreateHubProxy("FeedHub");
        }

        public async void Connect(Context context)
        {
            _proxy.On<string>("showMessage", (string text) =>
            {
                OnMessageReceived?.Invoke(this, text);
            });

            try
            {
                await _connection.Start();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                new AlertDialog.Builder(context)
                .SetTitle(Resource.String.ErrorTitle)
                .SetMessage(error)
                .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
                .Show();
            }
        }
    }
}

