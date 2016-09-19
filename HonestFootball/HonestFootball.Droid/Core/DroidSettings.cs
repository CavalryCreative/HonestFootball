using System;
using Android.Content;
using HonestFootball.Interfaces;
using HonestFootball.Models;

namespace HonestFootball.Droid.Core
{
    public class DroidSettings : ISettings
    {
        private readonly ISharedPreferences preferences;

        public DroidSettings(Context context)
        {
            preferences = context.GetSharedPreferences(context.PackageName, FileCreationMode.Private);
        }

        public bool IsSoundOn
        {
            get { return preferences.GetBoolean("IsSoundOn", true); }
            set
            {
                using (var editor = preferences.Edit())
                {
                    editor.PutBoolean("IsSoundOn", value);
                    editor.Commit();
                }
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