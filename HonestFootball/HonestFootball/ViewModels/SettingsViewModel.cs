﻿using HonestFootball.Interfaces;

namespace HonestFootball.ViewModels
{
    public class SettingsViewModel
    {
        private readonly ISettings settings;

        public SettingsViewModel()
        {
            this.settings = ServiceContainer.Resolve<ISettings>();
        }

        public bool IsSoundOn { get; set; }

        public void Save()
        {
            settings.IsSoundOn = IsSoundOn;
        }
    }
}
