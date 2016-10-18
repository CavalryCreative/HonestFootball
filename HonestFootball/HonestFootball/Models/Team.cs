using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonestFootball.Models
{
    public class Team
    {
        public string APIId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public byte Position { get; set; }
        public byte GamesPlayed { get; set; }
        public byte GamesWon { get; set; }
        public byte GamesDrawn { get; set; }
        public byte GamesLost { get; set; }
        public byte GoalsFor { get; set; }
        public byte GoalsAgainst { get; set; }
        public byte Points { get; set; }
    }
}
