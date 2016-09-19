using System;
using Android.App;
using Android.Runtime;
using HonestFootball.Interfaces;
using HonestFootball.ViewModels;
using HonestFootball.Droid.Core;

namespace HonestFootball.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class Application : Android.App.Application
    {
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
    }
}