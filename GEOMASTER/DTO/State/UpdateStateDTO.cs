namespace GEOMASTER.DTO.State
{
    public class UpdateStateDTO
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
