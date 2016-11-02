using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Views;
using HonestFootball.Models;
using Android.Widget;
using HonestFootball.Android.Fragments;

namespace HonestFootball.Android.Core
{
    public class SettingsAdapter : BaseAdapter<Team>
    {
        Activity context;
        List<Team> teams;

        public SettingsAdapter(Activity context, List<Team> teams)
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
            var team = teams[position];
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.TeamRow, null);

                var teamName = view.FindViewById<TextView>(Resource.Id.nameTextView);

                var vh = new TableViewHolder()
                {
                    Name = teamName                  
                };

                view.Tag = vh;
            }

            var holder = (TableViewHolder)view.Tag;

            holder.Name.Text = team.Name;
           
            return view;
        }
    }
}