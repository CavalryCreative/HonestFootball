using HonestFootball.Interfaces;
using Foundation;

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
    }
}
