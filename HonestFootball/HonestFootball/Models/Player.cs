using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonestFootball.Models
{
    public class Player
    {
        public string Surname { get; set; }
        public bool IsHomePlayer { get; set; }
        public bool IsSub { get; set; }
        public bool Substituted { get; set; }
        public string SubTime { get; set; }
        public string Position { get; set; }
        public string Number { get; set; }
    }
}
