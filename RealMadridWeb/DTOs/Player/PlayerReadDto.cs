namespace RealMadridWeb.DTOs
{
    public class PlayerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int ShirtNumber { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public string? ImageUrl { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
    }
}