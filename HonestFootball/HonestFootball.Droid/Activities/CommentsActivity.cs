
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using HonestFootball.ViewModels;
using HonestFootball.Models;
using HonestFootball.Droid.Core;
using System;
using Microsoft.AspNet.SignalR.Client;

using Newtonsoft.Json.Linq;

namespace HonestFootball.Droid.Activities
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon")]
    public class CommentsActivity : BaseActivity<CommentsViewModel>
    {
        //ListView listView;
        //Adapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Comments);

            //Create hub connection
            var client = new Client("Android");

            //listView = FindViewById<ListView>(Resource.Id.commentsList);
            //listView.Adapter = adapter = new Adapter(this);

            client.Connect(this);

            client.OnMessageReceived += (sender, message) => RunOnUiThread(() =>
            {
                string json = message;
                string jPath = "Events";

                JToken token = JToken.Parse(json);

                var y = token.SelectTokens(jPath);

                DroidSettings.TeamApiId = "9260";//TODO - test remove when settings screen set up

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
                    else
                    {
                        DisplayText(evtComment.ToString());
                    }
                }
            });
        }

        //protected async override void OnResume()
        //{
        //    base.OnResume();

        //    try
        //    {
        //        await viewModel.GetComments();

        //        adapter.NotifyDataSetInvalidated();
        //    }
        //    catch (Exception exc)
        //    {
        //        DisplayError(exc);
        //    }
        //}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.ConversationsMenu, menu);
            //return base.OnCreateOptionsMenu(menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //if (item.ItemId == Resource.Id.addFriendMenu)
            //{
            //    StartActivity(typeof(FriendsActivity));
            //}
            //return base.OnOptionsItemSelected(item);

            return true;
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