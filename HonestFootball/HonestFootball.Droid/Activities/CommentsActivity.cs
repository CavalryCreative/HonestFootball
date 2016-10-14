
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using HonestFootball.ViewModels;
using HonestFootball.Models;
using System;
using Microsoft.AspNet.SignalR.Client;

using Newtonsoft.Json.Linq;

namespace HonestFootball.Droid.Activities
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true)]
    public class CommentsActivity : BaseActivity<CommentsViewModel>
    {
        HubConnection hubConnection;
        IHubProxy SignalRChatHubProxy;
        public event EventHandler<string> OnMessageReceived;

        ListView listView;
        Adapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Comments);

            //Create hub connection
            hubConnection = new HubConnection("http://honest-apps.elasticbeanstalk.com/");
            SignalRChatHubProxy = hubConnection.CreateHubProxy("FeedHub");

            listView = FindViewById<ListView>(Resource.Id.commentsList);
            listView.Adapter = adapter = new Adapter(this);

            Connect();

            OnMessageReceived += (sender, message) => RunOnUiThread(() =>
            {
                string json = message;
                string jPath = "Events";

                JToken token = JToken.Parse(json);

                var y = token.SelectTokens(jPath);

                foreach (var childToken in y.Children())
                {
                   
                   
                }
            });
        }

        private async void Connect()
        {
            SignalRChatHubProxy.On<string>("showMessage", (string text) =>
            {
                OnMessageReceived?.Invoke(this, text);
            });

            try
            {
                await hubConnection.Start();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());//TODO - capture error and show dialog
            }
        }

        protected async override void OnResume()
        {
            base.OnResume();

            try
            {
                await viewModel.GetComments();

                adapter.NotifyDataSetInvalidated();
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
            //if (item.ItemId == Resource.Id.addFriendMenu)
            //{
            //    StartActivity(typeof(FriendsActivity));
            //}
            //return base.OnOptionsItemSelected(item);

            return true;
        }
    }

    class Adapter : BaseAdapter<Comment>
    {
        readonly CommentsViewModel commentsViewModel = ServiceContainer.Resolve<CommentsViewModel>();
        readonly LayoutInflater inflater;

        public Adapter(Context context)
        {
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }

        public override long GetItemId(int position)
        {
            return commentsViewModel.Comments[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.Comments, null);
            }

            var comment = this[position];
            var commentText = convertView.FindViewById<TextView>(Resource.Id.commentText);
            var matchScore = convertView.FindViewById<TextView>(Resource.Id.matchScore);
            var matchTime = convertView.FindViewById<TextView>(Resource.Id.matchTime);

            commentText.Text = comment.Text;
            matchScore.Text = comment.Score;
            matchTime.Text = comment.Time;

            return convertView;
        }

        public override int Count
        {
            get { return commentsViewModel.Comments == null ? 0 : commentsViewModel.Comments.Length; }
        }

        public override Comment this[int index]
        {
            get { return commentsViewModel.Comments[index]; }
        }
    }
}