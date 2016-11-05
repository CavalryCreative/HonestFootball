using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HonestFootball.Droid.Core
{
    public class TableViewHolder : Java.Lang.Object
    {      
        public TextView Name { get; set; }
        public TextView GamesPlayed { get; set; }
        public TextView GamesWon { get; set; }
        public TextView GamesDrawn { get; set; }
        public TextView GamesLost { get; set; }
        public TextView Points { get; set; }
    }
}