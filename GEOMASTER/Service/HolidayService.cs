using GEOMASTER.DTO.Holiday;
using GEOMASTER.Interface.Holiday;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _repo;

        public HolidayService(IHolidayRepository repo)
        {
            _repo = repo;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<HolidayResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();

            if (data == null)
                throw new Exception("No holiday data found.");

            return data.Select(x => new HolidayResponseDTO
            {
                Id = x.Id,
                Title = x.Title,
                Date = x.HolidayDate?.ToString("dd/MM/yyyy"),
                Description = x.Description,
                Status = x.IsActive == true ? "Active" : "Inactive"
            }).ToList();
        }

        // =========================
        // CREATE
        // =========================
        public async Task Create(CreateHolidayDTO dto)
        {
            ValidateCreate(dto);

            var entity = new Tblholiday
            {
                Title = dto.Title.Trim(),
                HolidayDate = dto.HolidayDate,
                Description = dto.Description,

                IsActive = true,
                IsDelete = false,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy ?? "Admin"
            };

            await _repo.Add(entity);
        }

        // =========================
        // UPDATE
        // =========================
        public async Task Update(UpdateHolidayDTO dto)
        {
            ValidateUpdate(dto);

            var existing = await _repo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Holiday with ID {dto.Id} was not found.");

            existing.Title = dto.Title.Trim();
            existing.HolidayDate = dto.HolidayDate;
            existing.Description = dto.Description;

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
                throw new KeyNotFoundException($"Holiday with ID {id} was not found.");

            await _repo.Delete(id);
        }

        // =========================
        // ACTIVE STATUS
        // =========================
        public async Task SetActive(int id, bool isActive)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Holiday with ID {id} was not found.");

            await _repo.SetActive(id, isActive);
        }

        // =========================
        // VALIDATION
        // =========================
        private void ValidateCreate(CreateHolidayDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Holiday title is required.");

            if (dto.HolidayDate == null)
                throw new ArgumentException("Holiday date is required.");
        }

        private void ValidateUpdate(UpdateHolidayDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid Holiday Id.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Holiday title is required.");
        }
    }
}