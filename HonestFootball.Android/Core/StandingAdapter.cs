using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using HonestFootball.Models;
using Android.Widget;

namespace HonestFootball.Android.Core
{
    public class StandingAdapter : BaseAdapter<Team>
    {
        Fragment context;
        List<Team> teams;

        public StandingAdapter(Fragment context, List<Team> teams)
        {
            this.context = context;
            this.teams = teams;
        }

        public override Team this[int position]
        {
            get
            {
                return teams[position];
            }
        }

        public override int Count
        {
            get
            {
                return teams.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }
    }
}