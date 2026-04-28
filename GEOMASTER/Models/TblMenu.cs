namespace GEOMASTER.Models
{
    public class TblMenu
    {
        public int Id { get; set; }

        public string Label { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }

        public int? ParentId { get; set; }
        public TblMenu? Parent { get; set; }

        public ICollection<TblMenu> Children { get; set; } = new List<TblMenu>();

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
