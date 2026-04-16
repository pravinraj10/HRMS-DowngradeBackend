namespace GEOMASTER.DTO.State
{
    public class CreateStateDTO
    {
        public int CountryId { get; set; }
        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public string? CreatedBy { get; set; }
    }
}
