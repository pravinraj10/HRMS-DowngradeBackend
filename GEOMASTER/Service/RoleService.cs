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
            return data.Select(MapToDTO).ToList();
        }

        public async Task<RoleResponseDTO> GetById(int id)
        {
            var data = await _repo.GetById(id);

            if (data == null)
                throw new KeyNotFoundException($"Role with ID {id} was not found.");

            return MapToDTO(data);
        }

        public async Task Create(CreateRoleDTO dto)
        {
            ValidateCreate(dto);

            var entity = new Tblrole
            {
                RoleName = dto.RoleName.Trim(),
                DepartmentId = dto.DepartmentId,
                RoleType = dto.RoleType,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };

            await _repo.Add(entity);
        }

        public async Task Update(UpdateRoleDTO dto)
        {
            var existing = await _repo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Role with ID {dto.Id} was not found.");

            ValidateUpdate(dto);

            existing.RoleName = dto.RoleName.Trim();
            existing.DepartmentId = dto.DepartmentId;
            existing.RoleType = dto.RoleType;
            existing.Description = dto.Description;
            existing.UpdatedBy = dto.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.Update(existing);
        }

        public async Task Delete(int id)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Role with ID {id} was not found.");

            await _repo.Delete(id);
        }

        public async Task SetActive(int id, bool isActive)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Role with ID {id} was not found.");

            await _repo.SetActive(id, isActive);
        }

        public async Task<List<RoleResponseDTO>> Search(string keyword)
        {
            var data = await _repo.Search(keyword ?? string.Empty);
            return data.Select(MapToDTO).ToList();
        }

        // Mapper

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

        // Validation

        private void ValidateCreate(CreateRoleDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.RoleName))
                throw new ArgumentException("RoleName is required.");

            if (dto.DepartmentId <= 0)
                throw new ArgumentException("Valid DepartmentId is required.");
        }

        private void ValidateUpdate(UpdateRoleDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid Role ID.");

            if (string.IsNullOrWhiteSpace(dto.RoleName))
                throw new ArgumentException("RoleName is required.");
        }
    }
}