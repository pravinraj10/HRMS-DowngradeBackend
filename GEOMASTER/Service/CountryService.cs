using GEOMASTER.DTO.Country;
using GEOMASTER.Interface.Country;
using GEOMASTER.Models;

namespace GEOMASTER
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repo;

        public CountryService(ICountryRepository repo)
        {
            _repo = repo;
        }

        public List<CountryDTO> Search(string? searchTerm)
        {
            var data = _repo.Search(searchTerm);
            return data.Select(x => new CountryDTO
            {
                Id = x.Id,
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy
            }).ToList();
        }

        public List<CountryDTO> GetAllCountries()
        {
            var data = _repo.GetAllCountries();

            return data.Select(x => new CountryDTO
            {
                Id = x.Id,
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy
            }).ToList();
        }

        public Tblcountry GetCountryById(int id)
        {
            return _repo.GetById(id);
        }

        public void CreateCountry(CountryDTO dto)
        {
            var entity = new Tblcountry
            {
                CountryCode = dto.CountryCode,
                CountryName = dto.CountryName,

                IsActive = true,
                IsDelete = false,

                CreatedBy = dto.CreatedBy ?? "Admin",
                CreatedAt = DateTime.Now
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void UpdateCountry(UpdateCountryDTO dto)
        {
            var existing = _repo.GetById(dto.Id);
            if (existing == null) return;

            existing.CountryCode = dto.CountryCode;
            existing.CountryName = dto.CountryName;
            existing.IsActive = dto.IsActive;

            existing.UpdatedBy = dto.UpdatedBy ?? "Admin";
            existing.UpdatedAt = DateTime.Now;

            _repo.Update(existing);
            _repo.Save();
        }

        public void DeleteCountry(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        public void DisableCountry(int id)
        {
            _repo.Disable(id);
            _repo.Save();
        }

        public bool SetCountryActive(int id, bool isActive)
        {
            var result = _repo.SetActive(id, isActive);
            if (result)
                _repo.Save();

            return result;
        }
    }
}
