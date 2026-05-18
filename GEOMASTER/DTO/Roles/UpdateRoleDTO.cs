namespace GEOMASTER.DTO.Roles
{
    public class UpdateRoleDTO
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public string? Description { get; set; }

        public string? SideMenu { get; set; }

        public string? UpdatedBy { get; set; }
    }
}