using GEOMASTER.DTO.City;

namespace GEOMASTER.Interface.City
{
    public interface ICityService
    {
        List<CityResponseDTO> Search(string? searchTerm);
        List<CityResponseDTO> GetAllCities();
        CityResponseDTO? GetCityById(int id);
        List<CityResponseDTO> GetCityByCountryState(int countryId, int stateId);

        void CreateCity(CityDTO dto);
        void UpdateCity(UpdateCityDTO dto);

        void DeleteCity(int id);
        void SetCityActive(int id, bool isActive);
    }
}
