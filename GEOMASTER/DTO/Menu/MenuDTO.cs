namespace GEOMASTER.DTO.Menu
{
    public class MenuDTO
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string? Icon { get; set; }

        public string? Url { get; set; }

        public List<MenuDTO> Children { get; set; } = new();
    }
}
