namespace GEOMASTER.DTO.City
{
    public class UpdateCityDTO
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string? CityName { get; set; }
    }
}
