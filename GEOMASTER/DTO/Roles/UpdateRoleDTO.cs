namespace GEOMASTER.DTO.Roles
{
    public class UpdateRoleDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public string? RoleType { get; set; }
        public string? Description { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
