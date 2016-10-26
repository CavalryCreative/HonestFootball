using System.Collections.Generic;
using HonestFootball.Models;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace HonestFootball.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public bool IsSoundOn { get; set; }

        public void Save()
        {
            //settings.IsSoundOn = IsSoundOn;
        }

        public List<Team> GetTeams()
        {
            List<Team> teams = new List<Team>();

            Team arsenal = new Team();
            arsenal.APIId = "9002";
            arsenal.Name = "Arsenal";
            arsenal.PrimaryColour = "";
            arsenal.SecondaryColour = "";
            teams.Add(arsenal);

            Team bournemouth = new Team();
            bournemouth.APIId = "9053";
            bournemouth.Name = "Bournemouth";
            bournemouth.PrimaryColour = "";
            bournemouth.SecondaryColour = "";
            teams.Add(bournemouth);

            Team burnley = new Team();
            burnley.APIId = "9072";
            burnley.Name = "Burnley";
            burnley.PrimaryColour = "";
            burnley.SecondaryColour = "";
            teams.Add(burnley);

            Team chelsea = new Team();
            chelsea.APIId = "9092";
            chelsea.Name = "Chelsea";
            chelsea.PrimaryColour = "";
            chelsea.SecondaryColour = "";
            teams.Add(chelsea);

            Team crystalpalace = new Team();
            crystalpalace.APIId = "9127";
            crystalpalace.Name = "Crystal Palace";
            crystalpalace.PrimaryColour = "";
            crystalpalace.SecondaryColour = "";
            teams.Add(crystalpalace);

            Team everton = new Team();
            everton.APIId = "9158";
            everton.Name = "Everton";
            everton.PrimaryColour = "";
            everton.SecondaryColour = "";
            teams.Add(everton);

            Team hull = new Team();
            hull.APIId = "9221";
            hull.Name = "Hull City";
            hull.PrimaryColour = "";
            hull.SecondaryColour = "";
            teams.Add(hull);

            Team leicester = new Team();
            leicester.APIId = "9240";
            leicester.Name = "Leicester City";
            leicester.PrimaryColour = "";
            leicester.SecondaryColour = "";
            teams.Add(leicester);

            Team liverpool = new Team();
            liverpool.APIId = "9249";
            liverpool.Name = "Liverpool";
            liverpool.PrimaryColour = "";
            liverpool.SecondaryColour = "";
            teams.Add(liverpool);

            Team mancity = new Team();
            mancity.APIId = "9259";
            mancity.Name = "Manchester City";
            mancity.PrimaryColour = "";
            mancity.SecondaryColour = "";
            teams.Add(mancity);

            Team manutd = new Team();
            manutd.APIId = "9260";
            manutd.Name = "Manchester United";
            manutd.PrimaryColour = "";
            manutd.SecondaryColour = "";
            teams.Add(manutd);

            Team middlesborough = new Team();
            middlesborough.APIId = "9274";
            middlesborough.Name = "Middlesborough";
            middlesborough.PrimaryColour = "";
            middlesborough.SecondaryColour = "";
            teams.Add(middlesborough);

            Team southampton = new Team();
            southampton.APIId = "9363";
            southampton.Name = "Southampton";
            southampton.PrimaryColour = "";
            southampton.SecondaryColour = "";
            teams.Add(southampton);

            Team stoke = new Team();
            stoke.APIId = "9378";
            stoke.Name = "Stoke City";
            stoke.PrimaryColour = "";
            stoke.SecondaryColour = "";
            teams.Add(stoke);

            Team sunderland = new Team();
            sunderland.APIId = "9384";
            sunderland.Name = "Sunderland";
            sunderland.PrimaryColour = "";
            sunderland.SecondaryColour = "";
            teams.Add(sunderland);

            Team swansea = new Team();
            swansea.APIId = "9387";
            swansea.Name = "Swansea City";
            swansea.PrimaryColour = "";
            swansea.SecondaryColour = "";
            teams.Add(swansea);

            Team tottenham = new Team();
            tottenham.APIId = "9406";
            tottenham.Name = "Tottenham";
            tottenham.PrimaryColour = "";
            tottenham.SecondaryColour = "";
            teams.Add(tottenham);

            Team watford = new Team();
            watford.APIId = "9423";
            watford.Name = "Watford";
            watford.PrimaryColour = "";
            watford.SecondaryColour = "";
            teams.Add(watford);

            Team westbrom = new Team();
            westbrom.APIId = "9426";
            westbrom.Name = "West Brom";
            westbrom.PrimaryColour = "";
            westbrom.SecondaryColour = "";
            teams.Add(westbrom);

            Team westham = new Team();
            westham.APIId = "9427";
            westham.Name = "West Ham";
            westham.PrimaryColour = "";
            westham.SecondaryColour = "";
            teams.Add(westham);

            return teams;
        }
    }
}
