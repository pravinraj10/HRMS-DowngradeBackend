using GEOMASTER.DTO.Designation;
using GEOMASTER.Interface.Designation;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _repo;

        public DesignationService(IDesignationRepository repo)
        {
            _repo = repo;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<DesignationResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();

            if (data == null)
                throw new Exception("No designation data found.");

            return data.Select(MapToDTO).ToList();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<DesignationResponseDTO> GetById(int id)
        {
            var data = await _repo.GetById(id);

            if (data == null)
                throw new KeyNotFoundException($"Designation with ID {id} was not found.");

            return MapToDTO(data);
        }

        // =========================
        // CREATE
        // =========================
        public async Task Create(CreateDesignationDTO dto)
        {
            Validate(dto);

            var entity = new Tbldesignation
            {
                DesignationName = dto.DesignationName.Trim(),
                DepartmentId = dto.DepartmentId,
                Description = dto.Description,

                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,

                IsActive = true,
                IsDeleted = false
            };

            await _repo.Add(entity);
        }

        // =========================
        // UPDATE
        // =========================
        public async Task Update(UpdateDesignationDTO dto)
        {
            ValidateUpdate(dto);

            var existing = await _repo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Designation with ID {dto.Id} was not found.");

            existing.DesignationName = dto.DesignationName.Trim();
            existing.DepartmentId = dto.DepartmentId;
            existing.Description = dto.Description;

            existing.UpdatedBy = dto.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.Update(existing);
        }

        // =========================
        // DELETE
        // =========================
        public async Task Delete(int id)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Designation with ID {id} was not found.");

            await _repo.Delete(id);
        }

        // =========================
        // ACTIVATE / DEACTIVATE
        // =========================
        public async Task SetActive(int id, bool isActive)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Designation with ID {id} was not found.");

            await _repo.SetActive(id, isActive);
        }

        // =========================
        // SEARCH
        // =========================
        public async Task<List<DesignationResponseDTO>> Search(string keyword)
        {
            var data = await _repo.Search(keyword ?? string.Empty);
            return data.Select(MapToDTO).ToList();
        }

        // =========================
        // MAPPER
        // =========================
        private DesignationResponseDTO MapToDTO(Tbldesignation x)
        {
            return new DesignationResponseDTO
            {
                Id = x.Id,
                DesignationName = x.DesignationName,
                DepartmentId = x.DepartmentId,
                Description = x.Description,
                IsActive = x.IsActive == true,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            };
        }

        // =========================
        // VALIDATION
        // =========================
        private void Validate(CreateDesignationDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.DesignationName))
                throw new ArgumentException("DesignationName is required.");

            if (dto.DepartmentId <= 0)
                throw new ArgumentException("DepartmentId is required.");
        }

        private void ValidateUpdate(UpdateDesignationDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid Designation Id.");

            if (string.IsNullOrWhiteSpace(dto.DesignationName))
                throw new ArgumentException("DesignationName is required.");

            if (dto.DepartmentId <= 0)
                throw new ArgumentException("DepartmentId is required.");
        }
    }
}