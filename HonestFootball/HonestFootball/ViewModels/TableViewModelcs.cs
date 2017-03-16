using System;
using System.Collections.Generic;
using HonestFootball.Models;
using System.Net.Http;
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
                string uri = "http://honestfootball.eu-west-1.elasticbeanstalk.com/api/leaguestandings";

                //var webRequest = (HttpWebRequest)WebRequest.Create(uri);
                //webRequest.Method = "GET";
                //var webResponse = await webRequest.GetResponseAsync();

                //var reader = new StreamReader(webResponse.GetResponseStream());
                //string s = reader.ReadToEnd();

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(uri))

                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();
                    string jPath = "Standings";

                    JToken token = JToken.Parse(result);

                    var y = token.SelectTokens(jPath);

                    foreach (var childToken in y.Children())
                    {
                        Team team = new Team();

                        team.Name = childToken.SelectToken("Name").ToString();
                        team.GamesPlayed = Convert.ToByte(childToken.SelectToken("GamesPlayed").ToString());
                        team.GamesWon = Convert.ToByte(childToken.SelectToken("GamesWon").ToString());
                        team.GamesDrawn = Convert.ToByte(childToken.SelectToken("GamesDrawn").ToString());
                        team.GamesLost = Convert.ToByte(childToken.SelectToken("GamesLost").ToString());
                        team.Points = Convert.ToByte(childToken.SelectToken("Points").ToString());
                        team.SelectedTeam = team.APIId == teamId ? true : false;

                        teams.Add(team);
                    }
                }

                //    JArray array = JArray.Parse(s);

                //foreach (JObject content in array.Children<JObject>())
                //{
                //    Team team = new Team();

                //    foreach (JProperty prop in content.Properties())
                //    {
                //        team.Name = prop.SelectToken("Name").ToString();
                //        team.GamesPlayed = Convert.ToByte(prop.SelectToken("GamesPlayed"));
                //        team.GamesWon = Convert.ToByte(prop.SelectToken("GamesWon"));
                //        team.GamesDrawn = Convert.ToByte(prop.SelectToken("GamesDrawn"));
                //        team.GamesLost = Convert.ToByte(prop.SelectToken("GamesLost"));
                //        team.Points = Convert.ToByte(prop.SelectToken("Points"));
                //        team.SelectedTeam = team.APIId == teamId ? true : false;
                //    }

                //    teams.Add(team);
                //}
            }
            finally
            {
                IsBusy = false;
            }

            return teams;
        }
    }
}
