using GEOMASTER.DTO.Roles;
using GEOMASTER.Interface.Role;
using GEOMASTER.Models.GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;

        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<RoleResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();
            return data.Select(x => MapToDTO(x)).ToList();
        }

        public async Task<RoleResponseDTO?> GetById(int id)
        {
            var data = await _repo.GetById(id);
            if (data == null) return null;

            return MapToDTO(data);
        }

        public async Task Create(CreateRoleDTO dto)
        {
            var entity = new Tblrole
            {
                RoleName = dto.RoleName,
                DepartmentId = dto.DepartmentId,
                RoleType = dto.RoleType,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            await _repo.Add(entity);
        }

        public async Task Update(UpdateRoleDTO dto)
        {
            var existing = await _repo.GetById(dto.Id);
            if (existing == null) return;

            existing.RoleName = dto.RoleName;
            existing.DepartmentId = dto.DepartmentId;
            existing.RoleType = dto.RoleType;
            existing.Description = dto.Description;
            existing.UpdatedBy = dto.UpdatedBy;
            existing.UpdatedAt = DateTime.Now;

            await _repo.Update(existing);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task SetActive(int id, bool isActive)
        {
            await _repo.SetActive(id, isActive);
        }

        public async Task<List<RoleResponseDTO>> Search(string keyword)
        {
            var data = await _repo.Search(keyword);
            return data.Select(x => MapToDTO(x)).ToList();
        }

        //  Mapper
        private RoleResponseDTO MapToDTO(Tblrole x)
        {
            return new RoleResponseDTO
            {
                Id = x.Id,
                RoleName = x.RoleName,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.DepartmentName ?? "",
                RoleType = x.RoleType,
                Description = x.Description,
                IsActive = x.IsActive == true
            };
        }
    }
}
