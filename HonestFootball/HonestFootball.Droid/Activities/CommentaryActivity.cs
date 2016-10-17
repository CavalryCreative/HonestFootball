using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using HonestFootball.ViewModels;
using HonestFootball.Models;
using System;
using Microsoft.AspNet.SignalR.Client;

namespace HonestFootball.Droid.Activities
{
    [Activity(Label = "@string/ApplicationName")]
    public class CommentaryActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Commentary);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }

    //class Adapter : BaseAdapter<Comment>
    //{
    //    readonly CommentsViewModel commentsViewModel = ServiceContainer.Resolve<CommentsViewModel>();
    //    readonly LayoutInflater inflater;

    //    public Adapter(Context context)
    //    {
    //        inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
    //    }

    //    public override long GetItemId(int position)
    //    {
    //        return commentsViewModel.Comments[position].Id;
    //    }

    //    public override View GetView(int position, View convertView, ViewGroup parent)
    //    {
    //        if (convertView == null)
    //        {
    //            convertView = inflater.Inflate(Resource.Layout.Comments, null);
    //        }

    //        var comment = this[position];
    //        var commentText = convertView.FindViewById<TextView>(Resource.Id.commentText);
    //        var matchScore = convertView.FindViewById<TextView>(Resource.Id.matchScore);
    //        var matchTime = convertView.FindViewById<TextView>(Resource.Id.matchTime);

    //        commentText.Text = comment.Text;
    //        matchScore.Text = comment.Score;
    //        matchTime.Text = comment.Time;

    //        return convertView;
    //    }

    //    public override int Count
    //    {
    //        get { return commentsViewModel.Comments == null ? 0 : commentsViewModel.Comments.Length; }
    //    }

    //    public override Comment this[int index]
    //    {
    //        get { return commentsViewModel.Comments[index]; }
    //    }
    //}

    //public class Client
    //{
    //    private readonly string _platform;
    //    private readonly HubConnection _connection;
    //    private readonly IHubProxy _proxy;

    //    public event EventHandler<string> OnMessageReceived;

    //    public Client(string platform)
    //    {
    //        _platform = platform;
    //        _connection = new HubConnection("http://honest-apps.elasticbeanstalk.com/");
    //        _proxy = _connection.CreateHubProxy("FeedHub");
    //    }

    //    public async void Connect()
    //    {
    //        try
    //        {
    //            await _connection.Start();
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.Write(ex.ToString());//TODO - capture error and show dialog
    //        }

    //        _proxy.On<string>("showMessage", (string text) =>
    //        {
    //            OnMessageReceived?.Invoke(this, text);
    //        });
    //    }
    //}
}