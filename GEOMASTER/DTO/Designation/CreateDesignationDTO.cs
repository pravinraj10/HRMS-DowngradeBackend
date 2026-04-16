namespace GEOMASTER.DTO.Designation
{
    public class CreateDesignationDTO
    {
        public string DesignationName { get; set; }
        public int DepartmentId { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
    }
}
