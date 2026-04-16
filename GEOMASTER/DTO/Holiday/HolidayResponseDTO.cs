namespace GEOMASTER.DTO.Holiday
{
    public class HolidayResponseDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Date { get; set; }   // formatted
        public string? Description { get; set; }
        public string Status { get; set; } = "Active";
    }
}
