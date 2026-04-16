namespace GEOMASTER.DTO.Designation
{
    public class UpdateDesignationDTO
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public int DepartmentId { get; set; }
        public string? Description { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
