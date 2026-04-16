namespace GEOMASTER.DTO.State
{
    public class StateResponseDTO
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
