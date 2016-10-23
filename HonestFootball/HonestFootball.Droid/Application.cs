using System;
using Android.App;
using Android.Runtime;
using HonestFootball.Interfaces;
using HonestFootball.ViewModels;
using HonestFootball.Droid.Core;

namespace HonestFootball.Droid
{
    [Application]
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
            //ServiceContainer.Register<IAppSettings>(() => new DroidSettings(this));
            ServiceContainer.Register<IWebService>(() => new WebService());
            //ViewModels
            ServiceContainer.Register<BaseViewModel>();
            ServiceContainer.Register<SettingsViewModel>();
            ServiceContainer.Register<CommentsViewModel>();     
        }
    }
}