namespace GEOMASTER.DTO.Country
{
    public class UpdateCountryDTO
    {
        public int Id { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }

        public bool? IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
