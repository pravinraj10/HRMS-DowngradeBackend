namespace GEOMASTER.DTO.Roles
{
    public class RoleResponseDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string? RoleType { get; set; }
        public string? Description { get; set; }
        public int AssignedUsers { get; set; }
        public bool IsActive { get; set; }
    }
}
