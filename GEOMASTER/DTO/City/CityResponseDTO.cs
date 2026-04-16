namespace GEOMASTER.DTO.City
{
    public class CityResponseDTO
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string? CityName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
