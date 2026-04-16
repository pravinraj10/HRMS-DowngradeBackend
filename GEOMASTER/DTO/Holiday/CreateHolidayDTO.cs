namespace GEOMASTER.DTO.Holiday
{
    public class CreateHolidayDTO
    {
        public string? Title { get; set; }
        public DateTime? HolidayDate { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
    }
}
