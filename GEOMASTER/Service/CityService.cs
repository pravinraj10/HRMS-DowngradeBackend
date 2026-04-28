using GEOMASTER.DTO.City;
using GEOMASTER.Interface.City;
using GEOMASTER.Interface.Country;
using GEOMASTER.Interface.State;
using GEOMASTER.Models;

namespace GEOMASTER.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepo;
        private readonly IStateRepository _stateRepo;
        private readonly ICountryRepository _countryRepo;

        public CityService(
            ICityRepository cityRepo,
            IStateRepository stateRepo,
            ICountryRepository countryRepo)
        {
            _cityRepo = cityRepo;
            _stateRepo = stateRepo;
            _countryRepo = countryRepo;
        }

        // =========================
        // SEARCH
        // =========================
        public List<CityResponseDTO> Search(string? searchTerm)
        {
            return _cityRepo
                .Search(searchTerm ?? string.Empty)
                .Select(MapToDTO)
                .ToList();
        }

        // =========================
        // GET ALL
        // =========================
        public List<CityResponseDTO> GetAllCities()
        {
            return _cityRepo
                .GetAll()
                .Select(MapToDTO)
                .ToList();
        }

        // =========================
        // GET BY ID
        // =========================
        public CityResponseDTO GetCityById(int id)
        {
            var data = _cityRepo.GetById(id);

            if (data == null)
                throw new KeyNotFoundException($"City with ID {id} was not found.");

            return MapToDTO(data);
        }

        // =========================
        // GET BY COUNTRY + STATE
        // =========================
        public List<CityResponseDTO> GetCityByCountryState(int countryId, int stateId)
        {
            if (countryId <= 0)
                throw new ArgumentException("Invalid CountryId.");

            if (stateId <= 0)
                throw new ArgumentException("Invalid StateId.");

            return _cityRepo
                .GetByCountryState(countryId, stateId)
                .Select(MapToDTO)
                .ToList();
        }

        // =========================
        // CREATE
        // =========================
        public void CreateCity(CityDTO dto)
        {
            ValidateCityRelationship(dto.CountryId, dto.StateId);

            if (string.IsNullOrWhiteSpace(dto.CityName))
                throw new ArgumentException("CityName is required.");

            var city = new Tblcity
            {
                CountryId = dto.CountryId,
                StateId = dto.StateId,
                CityName = dto.CityName.Trim(),

                IsActive = true,
                IsDelete = false,

                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Admin"
            };

            _cityRepo.Add(city);
            _cityRepo.Save();
        }

        // =========================
        // UPDATE
        // =========================
        public void UpdateCity(UpdateCityDTO dto)
        {
            if (dto.Id <= 0)
                throw new ArgumentException("Invalid City Id.");

            if (string.IsNullOrWhiteSpace(dto.CityName))
                throw new ArgumentException("CityName is required.");

            var existing = _cityRepo.GetById(dto.Id);

            if (existing == null)
                throw new KeyNotFoundException($"City with ID {dto.Id} was not found.");

            ValidateCityRelationship(dto.CountryId, dto.StateId);

            existing.CountryId = dto.CountryId;
            existing.StateId = dto.StateId;
            existing.CityName = dto.CityName.Trim();

            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = "Admin";

            _cityRepo.Update(existing);
            _cityRepo.Save();
        }

        // =========================
        // DELETE
        // =========================
        public void DeleteCity(int id)
        {
            var existing = _cityRepo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"City with ID {id} was not found.");

            _cityRepo.Delete(id);
            _cityRepo.Save();
        }

        // =========================
        // ACTIVE STATUS
        // =========================
        public void SetCityActive(int id, bool isActive)
        {
            var existing = _cityRepo.GetById(id);

            if (existing == null)
                throw new KeyNotFoundException($"City with ID {id} was not found.");

            _cityRepo.SetActive(id, isActive);
            _cityRepo.Save();
        }

        // =========================
        // VALIDATE COUNTRY/STATE
        // =========================
        private void ValidateCityRelationship(int countryId, int stateId)
        {
            if (countryId <= 0)
                throw new ArgumentException("Invalid CountryId.");

            if (stateId <= 0)
                throw new ArgumentException("Invalid StateId.");

            var country = _countryRepo.GetById(countryId);
            if (country == null)
                throw new KeyNotFoundException("Country not found.");

            var state = _stateRepo.GetById(stateId);
            if (state == null)
                throw new KeyNotFoundException("State not found.");

            if (state.CountryId != countryId)
                throw new ArgumentException("State does not belong to selected country.");
        }

        // =========================
        // MAPPER
        // =========================
        private CityResponseDTO MapToDTO(Tblcity c)
        {
            return new CityResponseDTO
            {
                Id = c.Id,
                CountryId = c.CountryId,
                StateId = c.StateId,
                CityName = c.CityName,
                IsActive = c.IsActive,

                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                UpdatedAt = c.UpdatedAt,
                UpdatedBy = c.UpdatedBy
            };
        }
    }
}