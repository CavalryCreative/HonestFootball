using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json.Linq;
using HonestFootball.iOS.Core;

using UIKit;

namespace HonestFootball.iOS
{
	public partial class CommentsViewController : UIViewController
	{
        //private readonly Section _messages;
        private readonly Client _client;

        public CommentsViewController(IntPtr handle) : base (handle)
		{
            _client = new Client("iOS");
        }

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            await _client.Connect();

            _client.OnMessageReceived += (sender, message) => InvokeOnMainThread(() =>
                {
                    string json = message;
                    string jPath = "Events";

                    JToken token = JToken.Parse(json);

                    var y = token.SelectTokens(jPath);

                    AppleSettings.TeamApiId = "926";//TODO - test remove when settings screen set up

                    foreach (var childToken in y.Children())
                    {
                        // Comment comment = new Comment();

                        var evtScore = childToken.SelectToken("Score").ToString();
                        var evtMinute = childToken.SelectToken("Minute").ToString();
                        var homeTeamId = childToken.SelectToken("HomeTeamAPIId").ToString();
                        var awayTeamId = childToken.SelectToken("AwayTeamAPIId").ToString();
                        var evtComment = childToken.SelectToken("EventComment").ToString();

                        if (AppleSettings.TeamApiId == homeTeamId || AppleSettings.TeamApiId == awayTeamId)
                        {
                            string jokeComment = string.Empty;

                            if (AppleSettings.TeamApiId == homeTeamId)
                            {
                                jokeComment = childToken.SelectToken("HomeComment").ToString();
                            }
                            else if (AppleSettings.TeamApiId == awayTeamId)
                            {
                                jokeComment = childToken.SelectToken("AwayComment").ToString();
                            }

                            //var commentText = FindViewById<TextView>(Resource.Id.commentText);
                            //var jokeCommentText = FindViewById<TextView>(Resource.Id.jokeCommentText);
                            //var matchScore = FindViewById<TextView>(Resource.Id.matchScore);
                            //var matchTime = FindViewById<TextView>(Resource.Id.matchTime);

                            //commentText.Text = evtComment;
                            //jokeCommentText.Text = jokeComment;
                            //matchScore.Text = evtScore;
                            //matchTime.Text = evtMinute;
                        }
                        else
                        {
                            //DisplayText(evtComment.ToString());
                        }
                    }
                }
            );
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
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
            _connection = new HubConnection("http://192.168.1.103");
            _proxy = _connection.CreateHubProxy("Chat");
        }

        public async Task Connect()
        {
            await _connection.Start();

            _proxy.On("messageReceived", (string platform, string message) =>
            {
                if (OnMessageReceived != null)
                    OnMessageReceived(this, string.Format("{0}: {1}", platform, message));
            });
        }
    }
}

