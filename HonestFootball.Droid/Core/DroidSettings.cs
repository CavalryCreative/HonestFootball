using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace HonestFootball.Droid.Core
{
    public class DroidSettings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        private const string TeamIdKey = "TeamAPIId";
        private static readonly string TeamIdDefault = string.Empty;

        public static string TeamApiId
        {
            get { return AppSettings.GetValueOrDefault(TeamIdKey, TeamIdDefault); }
            set { AppSettings.AddOrUpdateValue(TeamIdKey, value); }
        }
    }
}