using GEOMASTER.DTO.Roles;

namespace GEOMASTER.Interface.Role
{
    public interface IRoleService
    {
        Task<List<RoleResponseDTO>> GetAll();
        Task<RoleResponseDTO?> GetById(int id);
        Task Create(CreateRoleDTO dto);
        Task Update(UpdateRoleDTO dto);
        Task Delete(int id);
        Task SetActive(int id, bool isActive);
        Task<List<RoleResponseDTO>> Search(string keyword);
    }
}
