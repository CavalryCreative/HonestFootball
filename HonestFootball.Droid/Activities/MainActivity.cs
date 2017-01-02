using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using ViewPager = Android.Support.V4.View.ViewPager;
using Android.Support.Design.Widget;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json.Linq;
using HonestFootball.ViewModels;
using HonestFootball.Models;
using HonestFootball.Droid.Core;
using HonestFootball.Interfaces;
using HonestFootball.Droid.Fragments;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android;

namespace HonestFootball.Droid
{
    [Activity(Label = "HonestFootball.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        private ListView settingListview;
        private SettingsAdapter settingsAdapter;

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
            ServiceContainer.Register<TableViewModel>();

            //Get team id
            DroidSettings.TeamApiId = "9406";

            // Create UI
            SetContentView(Resource.Layout.Main);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            var toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            //Get settings list
            SettingsViewModel settingsVM = new SettingsViewModel();
            List<Team> teams = new List<Team>();

            teams = settingsVM.GetTeams();

            settingListview = FindViewById<ListView>(Resource.Id.settingsListView);
            settingsAdapter = new SettingsAdapter(this, teams);
            settingListview.Adapter = settingsAdapter;

            //Load stat fragments
            var fragments = new SupportFragment[]
            {
                new LineupFragment(),
                new TableFragment(),
                new FixturesFragment()
            };

            var titles = CharSequence.ArrayFromStringArray(new []
            {
                "Lineups",
                "Table",
                "Fixtures"
            });

            var viewPager = FindViewById<ViewPager>(Resource.Id.statsViewPager);
            viewPager.Adapter = new StatsPageAdapter(base.SupportFragmentManager, fragments, titles);

            try
            {
                GetClient();
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        #region Override methods
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

        //public override void OnBackPressed()
        //{
        //    if (SupportFragmentManager.BackStackEntryCount > 0)
        //    {
        //        SupportFragmentManager.PopBackStack();
        //        mCurrentFragment = mStackFragments.Pop();
        //    }

        //    else
        //    {
        //        base.OnBackPressed();
        //    }
        //}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.ConversationsMenu, menu);
            //return base.OnCreateOptionsMenu(menu);
            return true;
        }

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    switch (item.ItemId)
        //    {
        //        case Resource.Id.action_share:
        //            ShowFragment(shareFragment);
        //            return true;

        //        case Resource.Id.action_standings:
        //            ShowFragment(standingsFragment);
        //            return true;

        //        default:
        //            return base.OnOptionsItemSelected(item);
        //    }
        //}

        #endregion

        //private void ShowFragment(SupportFragment fragment)
        //{
        //    if (fragment.IsVisible)
        //    {
        //        return;
        //    }

        //    var trans = SupportFragmentManager.BeginTransaction();

        //    trans.SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, Resource.Animation.slide_in, Resource.Animation.slide_out);

        //    fragment.View.BringToFront();
        //    mCurrentFragment.View.BringToFront();

        //    trans.Hide(mCurrentFragment);
        //    trans.Show(fragment);

        //    trans.AddToBackStack(null);
        //    mStackFragments.Push(mCurrentFragment);
        //    trans.Commit();

        //    mCurrentFragment = fragment;
        //}

        //void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        //{
        //    switch (e.MenuItem.ItemId)
        //    {
        //        case (Resource.Id.action_settings):
        //            // React on 'Settings' selection
        //            break;
        //        case (Resource.Id.action_share):
        //            // React on 'Share' selection
        //            break;
        //        case (Resource.Id.action_standings):
        //            // React on 'Standings' selection
        //            break;
        //        case (Resource.Id.action_news):
        //            // React on 'Discussion' selection
        //            break;
        //    }

        //    // Close drawer
        //    drawerLayout.CloseDrawers();
        //}

        #region Get methods
        private void GetClient()
        {
            var client = new Client("Android");

            client.Connect(this);

            client.OnMessageReceived += (sender, message) => RunOnUiThread(() =>
            {
                string json = message;
                string jPath = "Matches";

                JToken token = JToken.Parse(json);

                var y = token.SelectTokens(jPath);

                foreach (var childToken in y.Children())
                {
                    // Comment comment = new Comment();
                    var latestEvent = childToken.SelectTokens("LatestEvent");

                    if (!JsonExtensions.IsNullOrEmpty(childToken.SelectToken("LatestEvent")))
                    {
                        foreach (var evt in latestEvent)
                        {
                            var evtScore = evt.SelectToken("Score").ToString();
                            var evtMinute = evt.SelectToken("Minute").ToString();
                            var homeTeamId = evt.SelectToken("HomeTeamAPIId").ToString();
                            var awayTeamId = evt.SelectToken("AwayTeamAPIId").ToString();
                            var evtComment = evt.SelectToken("EventComment").ToString();

                            if (DroidSettings.TeamApiId == homeTeamId || DroidSettings.TeamApiId == awayTeamId)
                            {
                                string jokeComment = string.Empty;

                                if (DroidSettings.TeamApiId == homeTeamId)
                                {
                                    jokeComment = evt.SelectToken("HomeComment").ToString();
                                }
                                else if (DroidSettings.TeamApiId == awayTeamId)
                                {
                                    jokeComment = evt.SelectToken("AwayComment").ToString();
                                }

                                var commentText = FindViewById<TextView>(Resource.Id.commentText);
                                var jokeCommentText = FindViewById<TextView>(Resource.Id.jokeCommentText);
                                var matchScore = FindViewById<TextView>(Resource.Id.matchScore);
                                var matchTime = FindViewById<TextView>(Resource.Id.matchTime);

                                commentText.Text = evtComment;
                                jokeCommentText.Text = jokeComment;
                                matchScore.Text = evtScore;
                                matchTime.Text = evtMinute;

                                //Return lineups/substitutions
                                var homeLineup = childToken.SelectTokens("HomeLineUp");
                                var awayLineup = childToken.SelectTokens("AwayLineUp");

                                LineupFragment lineUpFragment = new LineupFragment();
                                var layout = FindViewById<LinearLayout>(Resource.Id.lineupLayout);

                                var trans = SupportFragmentManager.BeginTransaction();
                                trans.Replace(Resource.Id.lineupLayout, lineUpFragment);
                                trans.Commit();

                                IList<Player> homeTeam = new List<Player>();
                                IList<Player> awayTeam = new List<Player>();

                                foreach (var homePlayer in homeLineup.Children())
                                {
                                    Player player = new Player();

                                    var jef = homePlayer.SelectToken("PlayerSurname").ToString();
                                    player.Surname = homePlayer.SelectToken("PlayerSurname").ToString();
                                    player.IsHomePlayer = Convert.ToBoolean(homePlayer.SelectToken("IsHomePlayer"));
                                    player.IsSub = Convert.ToBoolean(homePlayer.SelectToken("IsSub"));
                                    player.Substituted = Convert.ToBoolean(homePlayer.SelectToken("Substituted"));
                                    player.SubTime = homePlayer.SelectToken("SubTime").ToString();
                                    player.Position = homePlayer.SelectToken("Position").ToString();
                                    player.Number = homePlayer.SelectToken("Number").ToString();

                                    homeTeam.Add(player);
                                }

                                foreach (var awayPlayer in awayLineup.Children())
                                {
                                    Player player = new Player();

                                    player.Surname = awayPlayer.SelectToken("PlayerSurname").ToString();
                                    player.IsHomePlayer = Convert.ToBoolean(awayPlayer.SelectToken("IsHomePlayer"));
                                    player.IsSub = Convert.ToBoolean(awayPlayer.SelectToken("IsSub"));
                                    player.Substituted = Convert.ToBoolean(awayPlayer.SelectToken("Substituted"));
                                    player.SubTime = awayPlayer.SelectToken("SubTime").ToString();
                                    player.Position = awayPlayer.SelectToken("Position").ToString();
                                    player.Number = awayPlayer.SelectToken("Number").ToString();

                                    awayTeam.Add(player);
                                }

                                lineUpFragment.SendData(layout, homeTeam, awayTeam);

                                //Return stats
                            }
                        }
                    }
                    else
                    {
                        DisplayText("No games today");
                    }
                }
            });
        }

        #endregion

        #region Display message methods
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

        #endregion
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

    public interface IDataFromActivityToLineupFragment
    {
        void SendData(LinearLayout res, IList<Player> homeTeam, IList<Player> awayTeam);
    }
}

