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

        public async Task<IList<Team>> GetStandings(string teamId)
        {
            IsBusy = true;

            IList<Team> teams = new List<Team>();

            try
            {
                string uri = "http://honest-apps.elasticbeanstalk.com/api/leaguestandings";

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
                        team.Name = prop.SelectToken("Name").ToString();
                        team.GamesPlayed = Convert.ToByte(prop.SelectToken("GamesPlayed"));
                        team.GamesWon = Convert.ToByte(prop.SelectToken("GamesWon"));
                        team.GamesDrawn = Convert.ToByte(prop.SelectToken("GamesDrawn"));
                        team.GamesLost = Convert.ToByte(prop.SelectToken("GamesLost"));
                        team.Points = Convert.ToByte(prop.SelectToken("Points"));
                        team.SelectedTeam = team.APIId == teamId ? true : false;
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
