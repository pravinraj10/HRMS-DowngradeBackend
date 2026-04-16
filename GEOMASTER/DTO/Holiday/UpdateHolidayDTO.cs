namespace GEOMASTER.DTO.Holiday
{
    public class UpdateHolidayDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? HolidayDate { get; set; }
        public string? Description { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
