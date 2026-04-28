using GEOMASTER.DTO.Country;
using GEOMASTER.Interface.Country;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repo;

        public CountryService(ICountryRepository repo)
        {
            _repo = repo;
        }

        // =========================
        // SEARCH
        // =========================
        public List<CountryDTO> Search(string? searchTerm)
        {
            var data = _repo.Search(searchTerm ?? string.Empty);

            return data.Select(MapToDTO).ToList();
        }

        // =========================
        // GET ALL
        // =========================
        public List<CountryDTO> GetAllCountries()
        {
            var data = _repo.GetAllCountries();
            return data.Select(MapToDTO).ToList();
        }

        // =========================
        // GET BY ID (FIXED)
        // =========================
        public CountryDTO GetCountryById(int id)
        {
            var data = _repo.GetById(id);

            if (data == null)
                throw new KeyNotFoundException($"Country with ID {id} was not found.");

            return MapToDTO(data);
        }

        // =========================
        // CREATE
        // =========================
        public void CreateCountry(CountryDTO dto)
        {
            Validate(dto);

            var entity = new Tblcountry
            {
                CountryCode = dto.CountryCode.Trim(),
                CountryName = dto.CountryName.Trim(),

                IsActive = true,
                IsDelete = false,

                CreatedBy = dto.CreatedBy ?? "Admin",
                CreatedAt = DateTime.UtcNow
            };

            _repo.Add(entity);
            _repo.Save();
        }

        // =========================
        // UPDATE
        // =========================
        public void UpdateCountry(UpdateCountryDTO dto)
        {
            ValidateUpdate(dto);

            var existing = _repo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Country with ID {dto.Id} was not found.");

            existing.CountryCode = dto.CountryCode.Trim();
            existing.CountryName = dto.CountryName.Trim();
            existing.IsActive = dto.IsActive;

            existing.UpdatedBy = dto.UpdatedBy ?? "Admin";
            existing.UpdatedAt = DateTime.UtcNow;

            _repo.Update(existing);
            _repo.Save();
        }

        // =========================
        // DELETE
        // =========================
        public void DeleteCountry(int id)
        {
            var existing = _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Country with ID {id} was not found.");

            _repo.Delete(id);
            _repo.Save();
        }

        // =========================
        // DISABLE
        // =========================
        public void DisableCountry(int id)
        {
            var existing = _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Country with ID {id} was not found.");

            _repo.Disable(id);
            _repo.Save();
        }

        // =========================
        // SET ACTIVE
        // =========================
        public bool SetCountryActive(int id, bool isActive)
        {
            var existing = _repo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"Country with ID {id} was not found.");

            var result = _repo.SetActive(id, isActive);

            if (result)
                _repo.Save();

            return result;
        }

        // =========================
        // MAPPER
        // =========================
        private CountryDTO MapToDTO(Tblcountry x)
        {
            return new CountryDTO
            {
                Id = x.Id,
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy
            };
        }

        // =========================
        // VALIDATION
        // =========================
        private void Validate(CountryDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CountryName))
                throw new ArgumentException("CountryName is required.");

            if (string.IsNullOrWhiteSpace(dto.CountryCode))
                throw new ArgumentException("CountryCode is required.");
        }

        private void ValidateUpdate(UpdateCountryDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Id <= 0)
                throw new ArgumentException("Invalid Country Id.");

            if (string.IsNullOrWhiteSpace(dto.CountryName))
                throw new ArgumentException("CountryName is required.");

            if (string.IsNullOrWhiteSpace(dto.CountryCode))
                throw new ArgumentException("CountryCode is required.");
        }
    }
}