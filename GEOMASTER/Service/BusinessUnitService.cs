using GEOMASTER.DTO.BusinessUnit;
using GEOMASTER.Interface.BusinessUnit;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class BusinessUnitService : IBusinessUnitService
    {
        private readonly IBusinessUnitRepository _repo;

        public BusinessUnitService(IBusinessUnitRepository repo)
        {
            _repo = repo;
        }

        // GET ALL

        public async Task<List<BusinessUnitResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();

            if (data == null)
                throw new Exception("No business unit data found.");

            return data.Select(MapToDTO).ToList();
        }

        // GET BY ID

        public async Task<BusinessUnitResponseDTO> GetById(int id)
        {
            var data = await _repo.GetById(id);

            if (data == null)
                throw new KeyNotFoundException(
                    $"Business Unit with ID {id} was not found."
                );

            return MapToDTO(data);
        }

        // CREATE

        public async Task Create(CreateBusinessUnitDTO dto)
        {
            ValidateCreate(dto);

            var entity = new Tblbusinessunit
            {
                UnitCode = dto.UnitCode.Trim(),
                UnitName = dto.UnitName.Trim(),

                IsDelete = false,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy ?? "Admin"
            };

            await _repo.Add(entity);
        }

        // =========================
        // UPDATE
        // =========================
        public async Task Update(UpdateBusinessUnitDTO dto)
        {
            ValidateUpdate(dto);

            var existing = await _repo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException(
                    $"Business Unit with ID {dto.Id} was not found."
                );

            existing.UnitCode = dto.UnitCode.Trim();
            existing.UnitName = dto.UnitName.Trim();

            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = dto.UpdatedBy ?? "Admin";

            await _repo.Update(existing);
        }

        // =========================
        // DELETE
        // =========================
        public async Task Delete(int id)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException(
                    $"Business Unit with ID {id} was not found."
                );

            await _repo.Delete(id);
        }

        // =========================
        // MAPPER
        // =========================
        private BusinessUnitResponseDTO MapToDTO(Tblbusinessunit x)
        {
            return new BusinessUnitResponseDTO
            {
                Id = x.Id,
                UnitCode = x.UnitCode,
                UnitName = x.UnitName,

                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,

                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy
            };
        }

        // =========================
        // VALIDATION
        // =========================
        private void ValidateCreate(CreateBusinessUnitDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.UnitCode))
                throw new ArgumentException("UnitCode is required.");

            if (string.IsNullOrWhiteSpace(dto.UnitName))
                throw new ArgumentException("UnitName is required.");
        }

        private void ValidateUpdate(UpdateBusinessUnitDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid Business Unit Id.");

            if (string.IsNullOrWhiteSpace(dto.UnitCode))
                throw new ArgumentException("UnitCode is required.");

            if (string.IsNullOrWhiteSpace(dto.UnitName))
                throw new ArgumentException("UnitName is required.");
        }
    }
}