using GEOMASTER.DTO.Country;
using GEOMASTER.Models;

namespace GEOMASTER.Interface.Country
{
    public interface ICountryService
    {
        List<CountryDTO> Search(string? searchTerm);
        List<CountryDTO> GetAllCountries();
        CountryDTO GetCountryById(int id);

        void CreateCountry(CountryDTO dto);
        void UpdateCountry(UpdateCountryDTO dto);

        void DeleteCountry(int id);
        void DisableCountry(int id);
        bool SetCountryActive(int id, bool isActive);
    }
}
