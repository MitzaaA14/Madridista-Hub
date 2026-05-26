namespace RealMadridWeb.DTOs.Match
{
    public class MatchDto
    {
        public int Id { get; set; }
        public string Opponent { get; set; } = string.Empty;
        public string League { get; set; } = string.Empty;
        public string Stadium { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string? HomeTeamLogoUrl { get; set; }
        public string? AwayTeamLogoUrl { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public bool IsFinished { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
    }
}
