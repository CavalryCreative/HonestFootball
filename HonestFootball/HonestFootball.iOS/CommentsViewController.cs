using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

using UIKit;

namespace HonestFootball.iOS
{
	public partial class CommentsViewController : UIViewController
	{
        //private readonly Section _messages;
        private readonly Client _client;

        public CommentsViewController(IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

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

