using System;
using System.Collections.Generic;
using HonestFootball.Models;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace HonestFootball.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        public IList<Team> Teams { get; set; }

        public async Task<IList<Team>> GetStandings()
        {
            IsBusy = true;

            IList<Team> teams = new List<Team>();

            try
            {
                string uri = "http://api.football-api.com/2.0/standings/1204?Authorization=565ec012251f932ea4000001393b4115a8bf4bf551672b0543e35683";

                var webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "GET";
                var webResponse = await webRequest.GetResponseAsync();

                var reader = new StreamReader(webResponse.GetResponseStream());
                string s = reader.ReadToEnd();

                JArray array = JArray.Parse(s);

                foreach (JObject content in array.Children<JObject>())
                {
                    Team team = new Team();

                    foreach (JProperty prop in content.Properties())
                    {
                        team.Name = prop.SelectToken("team_name").ToString();
                        team.GamesPlayed = Convert.ToByte(prop.SelectToken("home_gp"));
                        team.GamesWon = Convert.ToByte(prop.SelectToken("home_w"));
                        team.GamesDrawn = Convert.ToByte(prop.SelectToken("home_d"));
                        team.GamesLost = Convert.ToByte(prop.SelectToken("home_l"));
                        team.Points = Convert.ToByte(prop.SelectToken("points"));
                    }

                    teams.Add(team);
                }
            }
            finally
            {
                IsBusy = false;
            }

            return teams;
        }
    }
}
