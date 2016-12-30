using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonestFootball.Models
{
    public class Fixture
    {
        public string APIId { get; set; }
        public int HomeTeamAPIId { get; set; }
        public int AwayTeamAPIId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string MatchDate { get; set; }
        public string KickOff { get; set; }
        public string FullTimeScore { get; set; }
    }
}
