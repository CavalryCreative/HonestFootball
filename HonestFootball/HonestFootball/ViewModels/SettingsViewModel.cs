using HonestFootball.Interfaces;

namespace HonestFootball.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public bool IsSoundOn { get; set; }

        public void Save()
        {
            settings.IsSoundOn = IsSoundOn;
        }
    }
}
