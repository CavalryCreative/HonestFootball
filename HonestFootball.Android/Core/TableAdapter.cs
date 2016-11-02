using System.Collections.Generic;
using Android.App;
using Android.Views;
using HonestFootball.Models;
using Android.Widget;
using HonestFootball.Android.Fragments;

namespace HonestFootball.Android.Core
{
    public class TableAdapter : BaseAdapter<Team>
    {
        Activity context;
        List<Team> teams;

        public TableAdapter(TableFragment context, List<Team> teams)
        {
            this.context = context.Activity;
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
                view = context.LayoutInflater.Inflate(Resource.Layout.StandingsRow, null);

                var teamName = view.FindViewById<TextView>(Resource.Id.nameTextView);
                var gamesPlayed = view.FindViewById<TextView>(Resource.Id.gamesPlayedTextView);
                var gamesWon = view.FindViewById<TextView>(Resource.Id.gamesWonTextView);
                var gamesDrawn = view.FindViewById<TextView>(Resource.Id.gamesDrawnTextView);
                var gamesLost = view.FindViewById<TextView>(Resource.Id.gamesLostView);
                var points = view.FindViewById<TextView>(Resource.Id.pointsTextView);

                var vh = new TableViewHolder()
                {
                    Name = teamName,
                    GamesPlayed = gamesPlayed,
                    GamesWon = gamesWon,
                    GamesDrawn = gamesDrawn,
                    GamesLost = gamesLost,
                    Points = points
                };

                view.Tag = vh;
            }

            var holder = (TableViewHolder)view.Tag;
             
            holder.Name.Text = team.Name;
            holder.GamesPlayed.Text = team.GamesPlayed.ToString();
            holder.GamesWon.Text = team.GamesWon.ToString();
            holder.GamesDrawn.Text = team.GamesDrawn.ToString();
            holder.GamesLost.Text = team.GamesLost.ToString();
            holder.Points.Text = team.Points.ToString();

            return view;
        }
    }
}