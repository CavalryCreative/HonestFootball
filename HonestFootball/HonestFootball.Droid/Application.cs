using System;
using Android.App;
using Android.Runtime;
using HonestFootball.Interfaces;
using HonestFootball.ViewModels;
using HonestFootball.Droid.Core;

using Microsoft.AspNet.SignalR.Client;

namespace HonestFootball.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class Application : Android.App.Application
    {
        HubConnection hubConnection;
        IHubProxy SignalRChatHub;
        public event EventHandler<string> OnMessageReceived;

        //This constructor is required
        public Application(IntPtr javaReference,
                           JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            //Android platform specific
            ServiceContainer.Register<ISettings>(() => new DroidSettings(this));
            //ViewModels
            ServiceContainer.Register<SettingsViewModel>();
            ServiceContainer.Register<CommentsViewModel>();           
        }

        private async void GetInfo_OnGetInfoComplete(object sender, EventArgs e)
        {
            hubConnection = new HubConnection("http://honest-apps.elasticbeanstalk.com/");
            SignalRChatHub = hubConnection.CreateHubProxy("FeedHub");

            SignalRChatHub.On<string>("BroadcastMessage", teamId =>
            {
                OnMessageReceived?.Invoke(this, string.Format("{0}", teamId));
            });

            try
            {
                await hubConnection.Start();
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());//TODO - capture error and show dialog
            }
        }      
    }
}