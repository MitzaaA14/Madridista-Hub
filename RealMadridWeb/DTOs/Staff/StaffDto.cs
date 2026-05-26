namespace RealMadridWeb.DTOs.Staff
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Role { get; set; } = string.Empty;
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}
