namespace GEOMASTER.Models
{
    public class Tblholiday
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? HolidayDate { get; set; }
        public string? Description { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
