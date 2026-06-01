namespace GEOMASTER.DTO.Roles
{
    public class RoleResponseDTO
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public string? SideMenu { get; set; }
        public bool IsActive { get; set; }
        public int AssignedUsers { get; set; }
    }
}