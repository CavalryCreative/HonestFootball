
namespace HonestFootball.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string EventComment { get; set; }
        public string Score { get; set; }
        public string Minute { get; set; }
        public string MatchAPIId { get; set; }
        public string HomeComment { get; set; }
        public string HomeTeamAPIId { get; set; }
        public string AwayComment { get; set; }
        public string AwayTeamAPIId { get; set; }
    }
}
