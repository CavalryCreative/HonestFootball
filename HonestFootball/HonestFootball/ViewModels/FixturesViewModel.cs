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
    public class FixturesViewModel : BaseViewModel
    {
        public IList<Fixture> Fixtures { get; set; }

        public async Task<IList<Fixture>> GetStandings(int teamId)
        {
            IsBusy = true;

            IList<Fixture> fixtures = new List<Fixture>();

            try
            {
                string uri = "http://honestfootball.eu-west-1.elasticbeanstalk.com/api/fixtures/" + teamId.ToString();

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(uri))

                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();
                    string jPath = "Fixtures";

                    JToken token = JToken.Parse(result);

                    var y = token.SelectTokens(jPath);

                    foreach (var childToken in y.Children())
                    {
                        Fixture fixture = new Fixture();

                        fixture.APIId = childToken.SelectToken("APIId").ToString();
                        fixture.AwayTeamAPIId = Convert.ToInt32(childToken.SelectToken("AwayTeamAPIId"));
                        fixture.AwayTeam = childToken.SelectToken("AwayTeam").ToString();
                        fixture.HomeTeamAPIId = Convert.ToInt32(childToken.SelectToken("HomeTeamAPIId"));
                        fixture.HomeTeam = childToken.SelectToken("HomeTeam").ToString();
                        //fixture.FullTimeScore = childToken.SelectToken("FullTimeScore").ToString();
                        fixture.KickOff = childToken.SelectToken("KickOff").ToString();
                        fixture.MatchDate = childToken.SelectToken("MatchDate").ToString();

                        fixtures.Add(fixture);
                    }
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
