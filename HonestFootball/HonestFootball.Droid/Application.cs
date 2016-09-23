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
        HubConnection chatConnection;
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

            //
            chatConnection = new HubConnection("http://169.254.80.80:54810/");
            SignalRChatHub = chatConnection.CreateHubProxy("CommentsHub");

            SignalRChatHub.On<string>("addComment", comment =>
            {
                if (OnMessageReceived != null)
                    OnMessageReceived(this, string.Format("{0}", comment));
            });
        }
    }
}