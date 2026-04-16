namespace GEOMASTER.DTO.BusinessUnit
{
    public class BusinessUnitResponseDTO
    {
        public int Id { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
