namespace GEOMASTER.DTO.Designation
{
    public class DesignationResponseDTO
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public int DepartmentId { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; internal set; }
        public DateTime? CreatedAt { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
    }
}
