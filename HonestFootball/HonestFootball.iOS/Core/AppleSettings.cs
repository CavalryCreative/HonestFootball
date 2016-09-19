using HonestFootball.Interfaces;
using Foundation;
using HonestFootball.Models;
using System;

namespace HonestFootball.iOS.Core
{
    public class AppleSettings : ISettings
    {
        public bool IsSoundOn
        {
            get { return NSUserDefaults.StandardUserDefaults.BoolForKey("IsSoundOn"); }
            set
            {
                var defaults = NSUserDefaults.StandardUserDefaults;
                defaults.SetBool(value, "IsSoundOn");
                defaults.Synchronize();
            }
        }

        bool ISettings.IsSoundOn
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        User ISettings.User
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        void ISettings.Save()
        {
            throw new NotImplementedException();
        }
    }
}
