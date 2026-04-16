namespace GEOMASTER.DTO.Roles
{
    public class CreateRoleDTO
    {
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public string? RoleType { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
    }
}
