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

        public async Task<List<HolidayResponseDTO>> GetAll()
        {
            var data = await _repo.GetAll();

            return data.Select(x => new HolidayResponseDTO
            {
                Id = x.Id,
                Title = x.Title,
                Date = x.HolidayDate?.ToString("dd/MM/yyyy"),
                Description = x.Description,
                Status = x.IsActive == true ? "Active" : "Inactive"
            }).ToList();
        }

        public async Task Create(CreateHolidayDTO dto)
        {
            var entity = new Tblholiday
            {
                Title = dto.Title,
                HolidayDate = dto.HolidayDate,
                Description = dto.Description,

                IsActive = true,
                IsDelete = false,

                CreatedAt = DateTime.Now,
                CreatedBy = dto.CreatedBy ?? "Admin"
            };

            await _repo.Add(entity);
        }

        public async Task Update(UpdateHolidayDTO dto)
        {
            var existing = await _repo.GetById(dto.Id);
            if (existing == null) return;

            existing.Title = dto.Title;
            existing.HolidayDate = dto.HolidayDate;
            existing.Description = dto.Description;

            existing.UpdatedAt = DateTime.Now;
            existing.UpdatedBy = dto.UpdatedBy ?? "Admin";

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
    }
}
