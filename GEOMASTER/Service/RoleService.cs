using GEOMASTER.DTO.Roles;
using GEOMASTER.Interface.Role;

namespace GEOMASTER.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;

        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }

        // GET ALL

        public async Task<List<RoleResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();

            return data.Select(MapToDTO).ToList();
        }

        // GET BY ID

        public async Task<RoleResponseDTO> GetById(int id)
        {
            var data = await _repo.GetById(id);

            if (data == null)
                throw new KeyNotFoundException(
                    $"Role with ID {id} was not found.");

            return MapToDTO(data);
        }

        // CREATE

        public async Task Create(CreateRoleDTO dto)
        {
            ValidateCreate(dto);

            var entity = new Tblrole
            {
                RoleName = dto.RoleName.Trim(),

                Description = dto.Description,

                SideMenu = dto.SideMenu,

                CreatedBy = dto.CreatedBy,

                CreatedAt = DateTime.UtcNow,

                IsActive = true,

                IsDeleted = false
            };

            await _repo.Add(entity);
        }

        // UPDATE

        public async Task Update(UpdateRoleDTO dto)
        {
            ValidateUpdate(dto);

            var existing = await _repo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException(
                    $"Role with ID {dto.Id} was not found.");

            existing.RoleName = dto.RoleName.Trim();

            existing.Description = dto.Description;

            existing.SideMenu = dto.SideMenu;

            existing.UpdatedBy = dto.UpdatedBy;

            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.Update(existing);
        }

        // DELETE

        public async Task Delete(int id)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException(
                    $"Role with ID {id} was not found.");

            await _repo.Delete(id);
        }

        // SET ACTIVE

        public async Task SetActive(int id, bool isActive)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException(
                    $"Role with ID {id} was not found.");

            await _repo.SetActive(id, isActive);
        }

        // SEARCH

        public async Task<List<RoleResponseDTO>> Search(string keyword)
        {
            var data = await _repo.Search(keyword ?? string.Empty);

            return data.Select(MapToDTO).ToList();
        }

        // DTO MAPPER

        private RoleResponseDTO MapToDTO(Tblrole x)
        {
            return new RoleResponseDTO
            {
                Id = x.Id,

                RoleName = x.RoleName,

                Description = x.Description,

                SideMenu = x.SideMenu,

                IsActive = x.IsActive
            };
        }

        // VALIDATIONS

        private void ValidateCreate(CreateRoleDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.RoleName))
                throw new ArgumentException(
                    "Role Name is required.");
        }

        private void ValidateUpdate(UpdateRoleDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException(
                    "Invalid Role ID.");

            if (string.IsNullOrWhiteSpace(dto.RoleName))
                throw new ArgumentException(
                    "Role Name is required.");
        }
    }
}