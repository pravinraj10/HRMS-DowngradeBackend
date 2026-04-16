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

        public CityService(ICityRepository cityRepo,
                           IStateRepository stateRepo,
                           ICountryRepository countryRepo)
        {
            _cityRepo = cityRepo;
            _stateRepo = stateRepo;
            _countryRepo = countryRepo;
        }

        public List<CityResponseDTO> Search(string? searchTerm)
        {
            return _cityRepo.Search(searchTerm).Select(MapToDTO).ToList();
        }

        public List<CityResponseDTO> GetAllCities()
        {
            return _cityRepo.GetAll()
                            .Select(MapToDTO)
                            .ToList();
        }

        public CityResponseDTO? GetCityById(int id)
        {
            var data = _cityRepo.GetById(id);
            return data == null ? null : MapToDTO(data);
        }

        public List<CityResponseDTO> GetCityByCountryState(int countryId, int stateId)
        {
            return _cityRepo.GetByCountryState(countryId, stateId)
                            .Select(MapToDTO)
                            .ToList();
        }

        public void CreateCity(CityDTO dto)
        {
            // Validation
            var country = _countryRepo.GetById(dto.CountryId);
            var state = _stateRepo.GetById(dto.StateId);

            if (country == null)
                throw new Exception("Invalid Country");

            if (state == null)
                throw new Exception("Invalid State");

            if (state.CountryId != dto.CountryId)
                throw new Exception("State does not belong to Country");

            var city = new Tblcity
            {
                CountryId = dto.CountryId,
                StateId = dto.StateId,
                CityName = dto.CityName,

                IsActive = true,
                IsDelete = false,

                CreatedAt = DateTime.Now,
                CreatedBy = "Admin" // later replace with logged-in user
            };

            _cityRepo.Add(city);
            _cityRepo.Save();
        }

        public void UpdateCity(UpdateCityDTO dto)
        {
            var existing = _cityRepo.GetById(dto.Id);
            if (existing == null) return;

            existing.CountryId = dto.CountryId;
            existing.StateId = dto.StateId;
            existing.CityName = dto.CityName;

            existing.UpdatedAt = DateTime.Now;
            existing.UpdatedBy = "Admin";

            _cityRepo.Update(existing);
            _cityRepo.Save();
        }

        public void DeleteCity(int id)
        {
            _cityRepo.Delete(id);
            _cityRepo.Save();
        }

        public void SetCityActive(int id, bool isActive)
        {
            _cityRepo.SetActive(id, isActive);
            _cityRepo.Save();
        }

        //  MAPPER (VERY IMPORTANT)
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
