namespace GEOMASTER.DTO.Country
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
    }
}

