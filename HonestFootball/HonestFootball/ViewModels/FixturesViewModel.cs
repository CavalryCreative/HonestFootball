using System;
using System.Collections.Generic;
using HonestFootball.Models;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace HonestFootball.ViewModels
{
    public class FixturesViewModel : BaseViewModel
    {
        public IList<Fixture> Fixtures { get; set; }

        public async Task<IList<Fixture>> GetStandings(int teamId)
        {
            IsBusy = true;

            IList<Fixture> fixtures = new List<Fixture>();

            try
            {
                string uri = "http://honest-apps.elasticbeanstalk.com/api/fixtures/" + teamId.ToString();

                var webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "GET";
                var webResponse = await webRequest.GetResponseAsync();

                var reader = new StreamReader(webResponse.GetResponseStream());
                string s = reader.ReadToEnd();

                JArray array = JArray.Parse(s);

                foreach (JObject content in array.Children<JObject>())
                {
                    Fixture fixture = new Fixture();

                    foreach (JProperty prop in content.Properties())
                    {
                        fixture.APIId = prop.SelectToken("APIId").ToString();
                        fixture.AwayTeamAPIId = Convert.ToInt32(prop.SelectToken("AwayTeamAPIId"));
                        fixture.AwayTeam = prop.SelectToken("AwayTeam").ToString();
                        fixture.HomeTeamAPIId = Convert.ToInt32(prop.SelectToken("HomeTeamAPIId"));
                        fixture.HomeTeam = prop.SelectToken("HomeTeam").ToString();
                        //fixture.FullTimeScore = prop.SelectToken("FullTimeScore").ToString();
                        fixture.KickOff = prop.SelectToken("KickOff").ToString();
                        fixture.MatchDate = prop.SelectToken("MatchDate").ToString();
                    }

                    fixtures.Add(fixture);
                }
            }
            finally
            {
                IsBusy = false;
            }

            return fixtures;
        }
    }
}
